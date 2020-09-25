using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgoraUWP;
using Windows.UI.Xaml.Media.Imaging;
using AgoraWinRT;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using System.Runtime.InteropServices;
using Windows.Media;
using Windows.Storage.Streams;
using System.Reflection;
using Windows.Media.Audio;
using Windows.Media.Render;
using Windows.Media.MediaProperties;
using System.Runtime.ConstrainedExecution;
using System.Timers;
using System.Threading;

namespace AgoraUWPDemo
{
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private static readonly uint DEFAULT_BITS_PER_SAMPLE = 16;
        private static readonly uint DEFAULT_CHANNEL_COUNT = 1;
        private static readonly uint DEFAULT_SAMPLE_RATE = 48000;

        private AgoraUWPRTC engine;
        private GeneralMediaCapturer m_audioCapture;
        private AudioGraph m_audioGraph;
        private AudioFrameInputNode m_audioInput;
        private Dictionary<int, Action> m_modes;
        private bool isAudioMuted = false;
        private bool AudioMuted
        {
            get => isAudioMuted;
            set
            {
                isAudioMuted = value;
                btnMuteAudio.Content = isAudioMuted ? "关闭静音" : "开启静音";

            }
        }
        private bool isVideoMuted = false;
        private bool VideoMuted
        {
            get => isVideoMuted;
            set
            {
                isVideoMuted = value;
                btnMuteVideo.Content = isVideoMuted ? "发送视频" : "关闭视频";
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.Init();

            /// UWP需要权限，这个权限应当在主线程以异步的形式进行申请。这个部分需要用户自己解决
            AgoraUWPRTC.RequestCameraAccess();
        }

        private void Init()
        {
            m_modes = new Dictionary<int, Action>();
            m_modes.Add(0, () => { StartEngineAndPreview(); });
            m_modes.Add(1, () => { StartEngineAndSelfAudioProcess(); });
            m_modes.Add(2, () => { StartEngineAndPullAudioProcess(); });

            txtResult.TextChanged += TxtResult_TextChanged;

            btnJoinChannel.Click += JoinChannel;
            btnLeaveChannel.Click += LeaveChannel;
            btnMuteAudio.Click += MuteAudio;
            btnMuteVideo.Click += MuteVideo;
        }

        private void MuteVideo(object sender, RoutedEventArgs e)
        {
            this.VideoMuted = !this.VideoMuted;
            engine.MuteLocalVideoStream(isVideoMuted);
        }

        private void MuteAudio(object sender, RoutedEventArgs e)
        {
            this.AudioMuted = !this.AudioMuted;
            engine.MuteLocalAudioStream(this.AudioMuted);
        }

        private void LeaveChannel(object sender, RoutedEventArgs e)
        {
            //Clean();
            engine.LeaveChannel();
        }

        private void JoinChannel(object sender, RoutedEventArgs e)
        {
            var action = m_modes[cbMediaMode.SelectedIndex];
            action?.Invoke();
            this.log("set channel profile", this.engine.SetChannelProfile(AgoraWinRT.CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING));
            this.log("set client role", this.engine.SetClientRole(AgoraWinRT.CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER));
            this.engine.SetupLocalVideo(new ImageBrushVideoCanvas { Target = localVideoBrush, RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE, MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_ENABLED });
            this.log("enable video", this.engine.EnableVideo());
            this.engine.StartPreview();
            log("join channel", this.engine.JoinChannel(txtChannelToken.Text, txtChannelName.Text, "", 0));
        }

        private void Clean()
        {
            this.AudioMuted = false;
            this.VideoMuted = false;
            if (m_audioInput != null) { m_audioInput.Stop(); m_audioInput.Dispose(); m_audioInput = null; }
            if (m_audioGraph != null) { m_audioGraph.Stop(); m_audioGraph.Dispose(); m_audioGraph = null; }
            if (m_audioCapture != null) { m_audioCapture.Dispose(); m_audioCapture = null; }
            if (this.engine != null) { engine.LeaveChannel(); this.engine.Dispose(); this.engine = null; }
        }
        /// <summary>
        /// 演示平常情况下打开视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEngineAndPreview()
        {
            InitEngine();

        }
        /// <summary>
        /// 演示如何进行音频自采集和自渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEngineAndSelfAudioProcess()
        {
            InitEngine();
            InitAudioCapture();
            this.engine.SetExternalAudioSource(true, m_audioCapture.AudioFormat.AudioEncodingProperties.SampleRate, (byte)m_audioCapture.AudioFormat.AudioEncodingProperties.ChannelCount);
        }
        /// <summary>
        /// 演示音频自渲染功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEngineAndPullAudioProcess()
        {
            InitEngine();
            InitAudioGraph();
            log("Set External Audio Sink", this.engine.SetExternalAudioSink(true, DEFAULT_SAMPLE_RATE, (byte)DEFAULT_CHANNEL_COUNT));
        }

        private void InitAudioGraph()
        {
            var settings = new AudioGraphSettings(AudioRenderCategory.Media);
            var createGraphResult = AudioGraph.CreateAsync(settings).AsTask().GetAwaiter().GetResult();
            m_audioGraph = createGraphResult.Graph;
            var outputResult = m_audioGraph.CreateDeviceOutputNodeAsync().AsTask().GetAwaiter().GetResult();

            m_audioInput = m_audioGraph.CreateFrameInputNode(
                new AudioEncodingProperties
                {
                    BitsPerSample = DEFAULT_BITS_PER_SAMPLE,
                    ChannelCount = DEFAULT_CHANNEL_COUNT,
                    SampleRate = DEFAULT_SAMPLE_RATE,
                    Subtype = MediaEncodingSubtypes.Pcm,
                });
            m_audioInput.QuantumStarted += QuantumStartedEvent;
            m_audioInput.AddOutgoingConnection(outputResult.DeviceOutputNode);
            m_audioInput.Stop();
        }

        private void QuantumStartedEvent(AudioFrameInputNode sender, FrameInputNodeQuantumStartedEventArgs args)
        {
            using (var frame = new AgoraWinRT.AudioFrame())
            {
                frame.bytesPerSample = 2;
                frame.channels = (byte)DEFAULT_CHANNEL_COUNT;
                frame.samplesPerSec = DEFAULT_SAMPLE_RATE;
                frame.type = AUDIO_FRAME_TYPE.FRAME_TYPE_PCM16;
                frame.renderTimeMs = 0;
                frame.samples = DEFAULT_SAMPLE_RATE / 100;// (uint)args.RequiredSamples;
                var result = engine.PullAudioFrame(frame);
                if (result == 0) PlayAudioFrame(frame);
            }

        }

        private unsafe void PlayAudioFrame(AgoraWinRT.AudioFrame frame)
        {
            using (var audioFrame = new Windows.Media.AudioFrame((uint)frame.buffer.Length))
            {
                using (var buffer = audioFrame.LockBuffer(AudioBufferAccessMode.Write))
                using (var reference = buffer.CreateReference())
                {
                    byte* data;
                    uint size;
                    ((IMemoryBufferByteAccess)reference).GetBuffer(out data, out size);
                    fixed (byte* raw = frame.buffer)
                    {
                        System.Buffer.MemoryCopy(raw, data, size, size);
                    }
                }
                m_audioInput?.AddFrame(audioFrame);
            }
        }

        private void InitAudioCapture()
        {
            var sourceGroup = MediaFrameSourceGroup.FindAllAsync().AsTask().GetAwaiter().GetResult();
            if (sourceGroup.Count == 0) return;
            m_audioCapture = new GeneralMediaCapturer(sourceGroup[0], StreamingCaptureMode.Audio);
            m_audioCapture.OnAudioFrameArrived += AudioFrameArrivedEvent;
        }

        private void AudioFrameArrivedEvent(AudioMediaFrame frame)
        {
            using (Windows.Media.AudioFrame rawAudioFrame = frame.GetAudioFrame())
            using (AudioBuffer audioBuffer = rawAudioFrame.LockBuffer(AudioBufferAccessMode.Read))
            using (AgoraWinRT.AudioFrame audioFrame = new AgoraWinRT.AudioFrame())
            {
                var buffer = Windows.Storage.Streams.Buffer.CreateCopyFromMemoryBuffer(audioBuffer);
                buffer.Length = audioBuffer.Length;
                using (var dataReader = DataReader.FromBuffer(buffer))
                {
                    dataReader.ByteOrder = ByteOrder.LittleEndian;
                    byte[] tmp = new byte[buffer.Length / 2];
                    int pos = 0;
                    while (dataReader.UnconsumedBufferLength > 0)
                    {
                        var int16Data = (Int16)(dataReader.ReadSingle() * Int16.MaxValue);
                        byte[] chunck = BitConverter.GetBytes(int16Data);
                        tmp[pos++] = chunck[0];
                        tmp[pos++] = chunck[1];
                    }
                    var format = frame.AudioEncodingProperties;
                    audioFrame.bytesPerSample = 2;
                    audioFrame.channels = (byte)format.ChannelCount;
                    audioFrame.samplesPerSec = format.SampleRate;
                    audioFrame.samples = (uint)(tmp.Length / 4);
                    audioFrame.type = AUDIO_FRAME_TYPE.FRAME_TYPE_PCM16;
                    audioFrame.buffer = tmp;
                    engine.PushAudioFrame(audioFrame);
                }
            }
        }

        private void TestCode(object sender, RoutedEventArgs e)
        {
            //engine.SetRemoteRenderMode(remoteUser, RENDER_MODE_TYPE.RENDER_MODE_FILL, VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_DISABLED);
            //localVideoEnabled = !localVideoEnabled;
            //engine.EnableLocalVideo(localVideoEnabled);    
            //log("Set External Video Source", engine.SetExternalVideoSource(localVideoEnabled, false));
            Clean();
        }

        private void TxtResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            var grid = VisualTreeHelper.GetChild(txtResult, 0);
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
            {
                var child = VisualTreeHelper.GetChild(grid, i);
                if (child is ScrollViewer)
                {
                    var scroll = (ScrollViewer)child;
                    scroll.ChangeView(0, scroll.ExtentHeight, 1);
                    break;
                }
            }
        }
        private void InitEngine()
        {
            Clean();
            engine = new AgoraUWPRTC(txtVendorKey.Text);
            engine.OnUserJoined += Engine_OnUserJoined;
            engine.OnFirstLocalVideoFrame += Engine_OnFirstLocalVideoFrame;
            engine.OnFirstRemoteVideoFrame += Engine_OnFirstRemoteVideoFrame;
            engine.OnJoinChannelSuccess += Engine_OnJoinChannelSuccess;
            engine.OnPlaybackAudioFrame += Engine_OnPlaybackAudioFrame;
        }

        private bool Engine_OnPlaybackAudioFrame(AgoraWinRT.AudioFrame frame)
        {
            PlayAudioFrame(frame);
            return false;
        }

        private void Engine_OnJoinChannelSuccess(string channel, ulong uid, uint elapsed)
        {
            m_audioCapture?.EnableAudio(true);
            m_audioGraph?.Start();
            m_audioInput?.Start();
        }

        private void Engine_OnFirstRemoteVideoFrame(ulong uid, uint width, uint height, uint elapsed)
        {
            log("onFirstRemoteVideoFrame", elapsed);
        }

        private void Engine_OnFirstLocalVideoFrame(uint width, uint height, uint elapsed)
        {
            log("onFirstLocalVideoFrame", elapsed);
        }

        private void Engine_OnUserJoined(ulong uid, uint elapsed)
        {
            _ = Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    this.engine.SetupRemoteVideo(new ImageBrushVideoCanvas
                    {
                        User = uid,
                        Target = remoteVideoBrush,
                        RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_FIT,
                        MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_ENABLED
                    });

                });
        }

        private void log(String operation, long result)
        {
            _ = txtResult.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    txtResult.Text += String.Format("agora {0} result is {1}\n", operation, result);
                });
        }

    }
}
