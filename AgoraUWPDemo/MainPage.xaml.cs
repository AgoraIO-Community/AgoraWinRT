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
        private AgoraUWPRTC engine;
        private ulong remoteUser;
        private bool localVideoEnabled = true;
        private GeneralMediaCapturer m_audioCapture;

        public MainPage()
        {
            this.InitializeComponent();

            this.Init();

            /// UWP需要权限，这个权限应当在主线程以异步的形式进行申请。这个部分需要用户自己解决
            AgoraUWPRTC.RequestCameraAccess();
        }

        private void Init()
        {
            localVideo.Source = new SoftwareBitmapSource();
            remoteVideo.Source = new SoftwareBitmapSource();
            txtResult.TextChanged += TxtResult_TextChanged;
            btnStart.Click += StartEngineAndPreview;
            btnStartSelfAudio.Click += StartEngineAndSelfAudioProcess;
            btnTest.Click += TestCode;
        }
        /// <summary>
        /// 演示平常情况下打开视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEngineAndPreview(object sender, RoutedEventArgs e)
        {
            InitEngine();
            this.log("set channel profile", this.engine.SetChannelProfile(AgoraWinRT.CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING));
            this.log("set client role", this.engine.SetClientRole(AgoraWinRT.CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER));
            this.engine.SetupLocalVideo(new ImageVideoCanvas { Target = localVideo, RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE, MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_ENABLED });
            this.log("enable video", this.engine.EnableVideo());
            this.engine.StartPreview();
            log("join channel", this.engine.JoinChannel(txtChannelToken.Text, txtChannelName.Text, "", 0));
        }
        /// <summary>
        /// 演示如何进行音频自采集和自渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEngineAndSelfAudioProcess(object sender, RoutedEventArgs e)
        {
            InitEngine();
            InitAudioCapture();

            this.log("set channel profile", this.engine.SetChannelProfile(AgoraWinRT.CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING));
            this.log("set client role", this.engine.SetClientRole(AgoraWinRT.CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER));
            this.engine.SetupLocalVideo(new ImageVideoCanvas { Target = localVideo, RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE, MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_ENABLED });
            this.engine.SetExternalAudioSource(true, m_audioCapture.AudioFormat.AudioEncodingProperties.SampleRate, (byte)m_audioCapture.AudioFormat.AudioEncodingProperties.ChannelCount);
            this.log("enable video", this.engine.EnableVideo());
            this.engine.StartPreview();
            log("join channel", this.engine.JoinChannel(txtChannelToken.Text, txtChannelName.Text, "", 0));
        }

        private void InitAudioCapture()
        {
            var sourceGroup = MediaFrameSourceGroup.FindAllAsync().AsTask().GetAwaiter().GetResult();
            if (sourceGroup.Count == 0) return;
            m_audioCapture = new GeneralMediaCapturer(sourceGroup[0], StreamingCaptureMode.Audio);
            m_audioCapture.OnAudioFrameArrived += M_audioCapture_OnAudioFrameArrived;
        }

        private void M_audioCapture_OnAudioFrameArrived(AudioMediaFrame frame)
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
            this.engine.LeaveChannel();
            this.engine.Dispose();
            this.engine = null;
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
            if (this.engine != null) this.engine.Dispose();
            this.engine = new AgoraUWPRTC(txtVendorKey.Text);
            this.engine.OnUserJoined += Engine_OnUserJoined;
            this.engine.OnFirstLocalVideoFrame += Engine_OnFirstLocalVideoFrame;
            this.engine.OnFirstRemoteVideoFrame += Engine_OnFirstRemoteVideoFrame;
            this.engine.OnJoinChannelSuccess += Engine_OnJoinChannelSuccess;
        }

        private void Engine_OnJoinChannelSuccess(string channel, ulong uid, uint elapsed)
        {
            m_audioCapture?.EnableAudio(true);
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
