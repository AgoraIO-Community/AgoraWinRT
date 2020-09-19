using AgoraWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Windows.Media.MediaProperties;
using Windows.Security.Authentication.Identity.Core;
using Windows.Security.Cryptography.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media;

namespace AgoraUWP
{
    public class AgoraRtc : AgoraWinRT.AgoraRtc, 
        AgoraWinRT.AgoraRtcEventHandler, 
        AgoraWinRT.VideoFrameObserver, 
        AgoraWinRT.AudioFrameObserver
    {
        private VideoCanvas localVideo = null;
        private bool previewing;
        private bool joinChanneled;
        private VideoCanvas remoteVideo = null;

        public AgoraRtc(string vendorKey): base(vendorKey)
        {
            base.RegisterRtcEngineEventHandler(this);
            base.RegisterVideoFrameObserver(this);
            base.RegisterAudioFrameObserver(this);

            this.SetExternalVideoSource(true, false);            
            _ = this.InitCaptureAsync();
        }

        private async Task InitCaptureAsync()
        {
            var mediaCapture = new MediaCapture();
            var sourceGroup = await MediaFrameSourceGroup.FindAllAsync();
            if (sourceGroup.Count == 0) return;
            await mediaCapture.InitializeAsync(
                new MediaCaptureInitializationSettings
                {
                    SourceGroup = sourceGroup[0],
                    SharingMode = MediaCaptureSharingMode.SharedReadOnly,
                    StreamingCaptureMode = StreamingCaptureMode.AudioAndVideo,
                    MemoryPreference = MediaCaptureMemoryPreference.Cpu
                }) ;
            foreach (MediaFrameSource source in mediaCapture.FrameSources.Values)
            {
                if (source.Info.SourceKind == MediaFrameSourceKind.Color)
                {
                    MediaFrameReader reader = await mediaCapture.CreateFrameReaderAsync(source, MediaEncodingSubtypes.Nv12);
                    reader.FrameArrived += VideoFrameArrivedEvent;
                    await reader.StartAsync();
                }
            }
        }

        private void VideoFrameArrivedEvent(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {
            using(var frame = sender.TryAcquireLatestFrame())
            {
                if (frame == null) return;
                var buffer = frame.BufferMediaFrame;
                if (buffer == null) return;
                var format = frame.VideoMediaFrame?.VideoFormat;
                if (format == null) return;
                using (var externalFrame = new ExternalVideoFrame())
                {
                    externalFrame.format = VIDEO_PIXEL_FORMAT.VIDEO_PIXEL_NV12;
                    externalFrame.type = VIDEO_BUFFER_TYPE.VIDEO_BUFFER_RAW_DATA;
                    externalFrame.stride = format.Width;
                    externalFrame.height = format.Height;
                    externalFrame.buffer = buffer.Buffer.ToArray();
                    PushVideoFrame(externalFrame);
                }
                Preview(frame);
            }
        }

        public void SetupRemoteVideo(VideoCanvas videoCanvas)
        {
            this.remoteVideo = videoCanvas;
        }

        private void Preview(MediaFrameReference frame)
        {
            if (this.previewing && !this.joinChanneled)
            {
                this.localVideo.Render(frame);
            }
        }

        public void SetupLocalVideo(VideoCanvas videoCanvas)
        {
            this.localVideo = videoCanvas;
        }

        public void StartPreview()
        {
            this.previewing = true;
        }

        public void StopPreview()
        {
            this.previewing = false;
        }

        #region AgoraRtcEventHandler

        public event OnConnectionStateChangedDelegate OnConnectionStateChanged;
        public event OnJoinChannelSuccessDelegate OnJoinChannelSuccess;
        public event OnRejoinChannelSuccessDelegate OnRejoinChannelSuccess;
        public event OnLeaveChannelDelegate OnLeaveChannel;
        public event OnClientRoleChangedDelegate OnClientRoleChanged;
        public event OnUserJoinedDelegate OnUserJoined;
        public event OnUserOfflineDelegate OnUserOffline;
        public event OnNetworkTypeChangedDelegate OnNetworkTypeChanged;
        public event OnConnectionLostDelegate OnConnectionLost;
        public event OnTokenPrivilegeWillExpireDelegate OnTokenPrivilegeWillExpire;
        public event OnRequestTokenDelegate OnRequestToken;
        public event OnLocalAudioStateChangedDelegate OnLocalAudioStateChanged;
        public event OnLocalVideoStateChangedDelegate OnLocalVideoStateChanged;
        public event OnFirstLocalAudioFramePublishedDelegate OnFirstLocalAudioFramePublished;
        public event OnFirstLocalVideoFramePublishedDelegate OnFirstLocalVideoFramePublished;
        public event OnAudioPublishStateChangedDelegate OnAudioPublishStateChanged;
        public event OnVideoPublishStateChangedDelegate OnVideoPublishStateChanged;
        public event OnRemoteAudioStateChangedDelegate OnRemoteAudioStateChanged;
        public event OnRemoteVideoStateChangedDelegate OnRemoteVideoStateChanged;
        public event OnFirstRemoteVideoFrameDelegate OnFirstRemoteVideoFrame;
        public event OnAudioSubscribeStateChangedDelegate OnAudioSubscribeStateChanged;
        public event OnVideoSubscribeStateChangedDelegate OnVideoSubscribeStateChanged;
        public event OnRtcStatsDelegate OnRtcStats;
        public event OnNetworkQualityDelegate OnNetworkQuality;
        public event OnLocalAudioStatsDelegate OnLocalAudioStats;
        public event OnLocalVideoStatsDelegate OnLocalVideoStats;
        public event OnRemoteAudioStatsDelegate OnRemoteAudioStats;
        public event OnRemoteVideoStatsDelegate OnRemoteVideoStats;
        public event OnAudioMixingStateChangedDelegate OnAudioMixingStateChanged;
        public event OnRemoteAudioMixingBeginDelegate OnRemoteAudioMixingBegin;
        public event OnRemoteAudioMixingEndDelegate OnRemoteAudioMixingEnd;
        public event OnAudioEffectFinishedDelegate OnAudioEffectFinished;
        public event OnRtmpStreamingStateChangedDelegate OnRtmpStreamingStateChanged;
        public event OnRtmpStreamingEventDelegate OnRtmpStreamingEvent;
        public event OnTranscodingUpdatedDelegate OnTranscodingUpdated;
        public event OnChannelMediaRelayStateChangedDelegate OnChannelMediaRelayStateChanged;
        public event OnChannelMediaRelayEventDelegate OnChannelMediaRelayEvent;
        public event OnAudioVolumeIndicationDelegate OnAudioVolumeIndication;
        public event OnActiveSpeakerDelegate OnActiveSpeaker;
        public event OnLocalPublishFallbackToAudioOnlyDelegate OnLocalPublishFallbackToAudioOnly;
        public event OnRemoteSubscribeFallbackToAudioOnlyDelegate OnRemoteSubscribeFallbackToAudioOnly;
        public event OnLastmileQualityDelegate OnLastmileQuality;
        public event OnLastmileProbeResultDelegate OnLastmileProbeResult;
        public event OnStreamInjectedStatusDelegate OnStreamInjectedStatus;
        public event OnStreamMessageDelegate OnStreamMessage;
        public event OnStreamMessageErrorDelegate OnStreamMessageError;
        public event OnWarningDelegate OnWarning;
        public event OnErrorDelegate OnError;
        public event OnApiCallExecutedDelegate OnApiCallExecuted;

        void AgoraRtcEventHandler.OnConnectionStateChanged(CONNECTION_STATE_TYPE type, CONNECTION_CHANGED_REASON_TYPE reason)
        {
            OnConnectionStateChanged?.Invoke(type, reason);
        }

        void AgoraRtcEventHandler.OnJoinChannelSuccess(string channel, ulong uid, uint elapsed)
        {
            this.joinChanneled = true;
            OnJoinChannelSuccess?.Invoke(channel, uid, elapsed);
        }

        void AgoraRtcEventHandler.OnRejoinChannelSuccess(string channel, ulong uid, uint elapsed)
        {
            this.joinChanneled = true;
            OnRejoinChannelSuccess?.Invoke(channel, uid, elapsed);
        }

        void AgoraRtcEventHandler.OnLeaveChannel(RtcStats stats)
        {
            this.joinChanneled = false;
            OnLeaveChannel?.Invoke(stats);
        }

        void AgoraRtcEventHandler.OnClientRoleChanged(CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole)
        {
            OnClientRoleChanged?.Invoke(oldRole, newRole);
        }

        void AgoraRtcEventHandler.OnUserJoined(ulong uid, uint elapsed)
        {
            OnUserJoined?.Invoke(uid, elapsed);
        }

        void AgoraRtcEventHandler.OnUserOffline(ulong uid, USER_OFFLINE_REASON_TYPE reason)
        {
            OnUserOffline?.Invoke(uid, reason);
        }

        void AgoraRtcEventHandler.OnNetworkTypeChanged(NETWORK_TYPE type)
        {
            OnNetworkTypeChanged?.Invoke(type);
        }

        void AgoraRtcEventHandler.OnConnectionLost()
        {
            OnConnectionLost?.Invoke();
        }

        void AgoraRtcEventHandler.OnTokenPrivilegeWillExpire(string token)
        {
            OnTokenPrivilegeWillExpire?.Invoke(token);
        }

        void AgoraRtcEventHandler.OnRequestToken()
        {
            OnRequestToken?.Invoke();
        }

        void AgoraRtcEventHandler.OnLocalAudioStateChanged(LOCAL_AUDIO_STREAM_STATE state, LOCAL_AUDIO_STREAM_ERROR error)
        {
            OnLocalAudioStateChanged?.Invoke(state, error);
        }

        void AgoraRtcEventHandler.OnLocalVideoStateChanged(LOCAL_VIDEO_STREAM_STATE state, LOCAL_VIDEO_STREAM_ERROR error)
        {
            OnLocalVideoStateChanged?.Invoke(state, error);
        }

        void AgoraRtcEventHandler.OnFirstLocalAudioFramePublished(uint elapsed)
        {
            OnFirstLocalAudioFramePublished?.Invoke(elapsed);
        }

        void AgoraRtcEventHandler.OnFirstLocalVideoFramePublished(uint elapsed)
        {
            OnFirstLocalVideoFramePublished?.Invoke(elapsed);
        }

        void AgoraRtcEventHandler.OnAudioPublishStateChanged(string channel, STREAM_PUBLISH_STATE oldState, STREAM_PUBLISH_STATE newState, uint elapsed)
        {
            OnAudioPublishStateChanged?.Invoke(channel, oldState, newState, elapsed);
        }

        void AgoraRtcEventHandler.OnVideoPublishStateChanged(string channel, STREAM_PUBLISH_STATE oldState, STREAM_PUBLISH_STATE newState, uint elapsed)
        {
            OnVideoPublishStateChanged?.Invoke(channel, oldState, newState, elapsed);
        }

        void AgoraRtcEventHandler.OnRemoteAudioStateChanged(ulong uid, REMOTE_AUDIO_STATE state, REMOTE_AUDIO_STATE_REASON reason, uint elapsed)
        {
            OnRemoteAudioStateChanged?.Invoke(uid, state, reason, elapsed);
        }

        void AgoraRtcEventHandler.OnRemoteVideoStateChanged(ulong uid, REMOTE_VIDEO_STATE state, REMOTE_VIDEO_STATE_REASON reason, uint elapsed)
        {
            OnRemoteVideoStateChanged?.Invoke(uid, state, reason, elapsed);
        }

        void AgoraRtcEventHandler.OnFirstRemoteVideoFrame(ulong uid, uint width, uint height, uint elapsed)
        {
            OnFirstRemoteVideoFrame?.Invoke(uid, width, height, elapsed);
        }

        void AgoraRtcEventHandler.OnAudioSubscribeStateChanged(string channel, ulong uid, STREAM_SUBSCRIBE_STATE oldState, STREAM_SUBSCRIBE_STATE newState, uint elapsed)
        {
            OnAudioSubscribeStateChanged?.Invoke(channel, uid, oldState, newState, elapsed);
        }

        void AgoraRtcEventHandler.OnVideoSubscribeStateChanged(string channel, ulong uid, STREAM_SUBSCRIBE_STATE oldState, STREAM_SUBSCRIBE_STATE newState, uint elapsed)
        {
            OnVideoSubscribeStateChanged?.Invoke(channel, uid, oldState, newState, elapsed);
        }

        void AgoraRtcEventHandler.OnRtcStats(RtcStats stats)
        {
            OnRtcStats?.Invoke(stats);
        }

        void AgoraRtcEventHandler.OnNetworkQuality(ulong uid, QUALITY_TYPE txQuality, QUALITY_TYPE rxQuality)
        {
            OnNetworkQuality?.Invoke(uid, txQuality, rxQuality);
        }

        void AgoraRtcEventHandler.OnLocalAudioStats(LocalAudioStats stats)
        {
            OnLocalAudioStats?.Invoke(stats);
        }

        void AgoraRtcEventHandler.OnLocalVideoStats(LocalVideoStats stats)
        {
            OnLocalVideoStats?.Invoke(stats);
        }

        void AgoraRtcEventHandler.OnRemoteAudioStats(RemoteAudioStats stats)
        {
            OnRemoteAudioStats?.Invoke(stats);
        }

        void AgoraRtcEventHandler.OnRemoteVideoStats(RemoteVideoStats stats)
        {
            OnRemoteVideoStats?.Invoke(stats);
        }

        void AgoraRtcEventHandler.OnAudioMixingStateChanged(AUDIO_MIXING_STATE_TYPE state, AUDIO_MIXING_ERROR_TYPE error)
        {
            OnAudioMixingStateChanged?.Invoke(state, error);
        }

        void AgoraRtcEventHandler.OnRemoteAudioMixingBegin()
        {
            OnRemoteAudioMixingBegin?.Invoke();
        }

        void AgoraRtcEventHandler.OnRemoteAudioMixingEnd()
        {
            OnRemoteAudioMixingEnd?.Invoke();
        }

        void AgoraRtcEventHandler.OnAudioEffectFinished(ulong soundId)
        {
            OnAudioEffectFinished?.Invoke(soundId);
        }

        void AgoraRtcEventHandler.OnRtmpStreamingStateChanged(string url, RTMP_STREAM_PUBLISH_STATE state, RTMP_STREAM_PUBLISH_ERROR error)
        {
            OnRtmpStreamingStateChanged?.Invoke(url, state, error);
        }

        void AgoraRtcEventHandler.OnRtmpStreamingEvent(string url, RTMP_STREAMING_EVENT code)
        {
            OnRtmpStreamingEvent?.Invoke(url, code);
        }

        void AgoraRtcEventHandler.OnTranscodingUpdated()
        {
            OnTranscodingUpdated?.Invoke();
        }

        void AgoraRtcEventHandler.OnChannelMediaRelayStateChanged(CHANNEL_MEDIA_RELAY_STATE state, CHANNEL_MEDIA_RELAY_ERROR error)
        {
            OnChannelMediaRelayStateChanged?.Invoke(state, error);
        }

        void AgoraRtcEventHandler.OnChannelMediaRelayEvent(CHANNEL_MEDIA_RELAY_EVENT code)
        {
            OnChannelMediaRelayEvent?.Invoke(code);
        }

        void AgoraRtcEventHandler.OnAudioVolumeIndication(AudioVolumeInfo[] speakers, byte totalVolume)
        {
            OnAudioVolumeIndication?.Invoke(speakers, totalVolume);
        }

        void AgoraRtcEventHandler.OnActiveSpeaker(ulong uid)
        {
            OnActiveSpeaker?.Invoke(uid);
        }

        void AgoraRtcEventHandler.OnLocalPublishFallbackToAudioOnly(bool isFallbackOrRecover)
        {
            OnLocalPublishFallbackToAudioOnly?.Invoke(isFallbackOrRecover);
        }

        void AgoraRtcEventHandler.OnRemoteSubscribeFallbackToAudioOnly(ulong uid, bool isFallbackOrRecover)
        {
            OnRemoteSubscribeFallbackToAudioOnly?.Invoke(uid, isFallbackOrRecover);
        }

        void AgoraRtcEventHandler.OnLastmileQuality(QUALITY_TYPE quality)
        {
            OnLastmileQuality?.Invoke(quality);
        }

        void AgoraRtcEventHandler.OnLastmileProbeResult(LastmileProbeResult result)
        {
            OnLastmileProbeResult?.Invoke(result);
        }

        void AgoraRtcEventHandler.OnStreamInjectedStatus(string url, ulong uid, INJECT_STREAM_STATUS status)
        {
            OnStreamInjectedStatus?.Invoke(url, uid, status);
        }

        void AgoraRtcEventHandler.OnStreamMessage(ulong uid, long streamId, string data)
        {
            OnStreamMessage?.Invoke(uid, streamId, data);
        }

        void AgoraRtcEventHandler.OnStreamMessageError(ulong uid, long streamId, uint error, ushort missed, ushort cached)
        {
            OnStreamMessageError?.Invoke(uid, streamId, error, missed, cached);
        }

        void AgoraRtcEventHandler.OnWarning(long code, string msg)
        {
            OnWarning?.Invoke(code, msg);
        }

        void AgoraRtcEventHandler.OnError(long code, string msg)
        {
            OnError?.Invoke(code, msg);
        }

        void AgoraRtcEventHandler.OnApiCallExecuted(long code, string api, string result)
        {
            OnApiCallExecuted?.Invoke(code, api, result);
        }
        #endregion AgoraRtcEventHandler

        #region VideoFrameObserver
        public event OnCaptureVideoFrameDelegate OnCaptureVideoFrame;
        public event OnPreEncodeVideFrameDelegate OnPreEncodeVideFrame;
        public event OnRenderVideoFrameDelegate OnRenderVideoFrame;
        public event GetVideoFramePreferenceDelegate GetVideoFramePreference;
        public event GetRotationAppliedDelegate GetRotationApplied;
        public event GetMirrorAppliedDelegate GetMirrorApplied;
        public event GetSmoothRenderingEnabledDelegate GetSmoothRenderingEnabled;
        public event GetObservedFramePositionDelegate GetObservedFramePosition;
        public event IsMultipleChannelVideoFrameWantedDelegate IsMultipleChannelVideoFrameWanted;
        public event OnRenderVideoFrameExDelegate OnRenderVideoFrameEx;

        bool VideoFrameObserver.OnCaptureVideoFrame(VideoFrame frame)
        {
          return  OnCaptureVideoFrame == null ? true : OnCaptureVideoFrame(frame);
        }

        bool VideoFrameObserver.OnPreEncodeVideFrame(VideoFrame frame)
        {
            var result = OnPreEncodeVideFrame == null ? true : OnPreEncodeVideFrame(frame);
            this.localVideo?.Render(frame);
            return result;
        }

        bool VideoFrameObserver.OnRenderVideoFrame(ulong uid, VideoFrame frame)
        {
            var result = OnRenderVideoFrame == null ? true : OnRenderVideoFrame(uid, frame);
            this.remoteVideo?.Render(frame);
            return result;
        }

        VIDEO_FRAME_TYPE VideoFrameObserver.GetVideoFramePreference()
        {
            return GetVideoFramePreference == null ? VIDEO_FRAME_TYPE.FRAME_TYPE_YUV420 : GetVideoFramePreference();   
        }

        bool VideoFrameObserver.GetRotationApplied()
        {
            return GetRotationApplied == null ? false : GetRotationApplied();
        }

        bool VideoFrameObserver.GetMirrorApplied()
        {
            return GetMirrorApplied == null ? false : GetMirrorApplied();
        }

        bool VideoFrameObserver.GetSmoothRenderingEnabled()
        {
            return GetSmoothRenderingEnabled == null ? false : GetSmoothRenderingEnabled();
        }

        uint VideoFrameObserver.GetObservedFramePosition()
        {
            return GetObservedFramePosition == null ? Convert.ToUInt16(VIDEO_OBSERVER_POSITION.POSITION_POST_CAPTURER | VIDEO_OBSERVER_POSITION.POSITION_PRE_RENDERER) : GetObservedFramePosition();
        }

        bool VideoFrameObserver.IsMultipleChannelVideoFrameWanted()
        {
            return IsMultipleChannelVideoFrameWanted == null ? false : IsMultipleChannelVideoFrameWanted();
        }

        bool VideoFrameObserver.OnRenderVideoFrameEx(string channel, ulong uid, VideoFrame frame)
        {
            return OnRenderVideoFrameEx == null ? true : OnRenderVideoFrameEx(channel, uid, frame);
        }

        #endregion VideoFrameObserver

        #region AudioFrameObserver
        public event OnRecordAudioFrameDelegate OnRecordAudioFrame;
        public event OnPlaybackAudioFrameDelegate OnPlaybackAudioFrame;
        public event OnMixedAudioFrameDelegate OnMixedAudioFrame;
        public event OnPlaybackAudioFrameBeforeMixingDelegate OnPlaybackAudioFrameBeforeMixing;
        public event IsMultipleChannelAudioFrameWantedDelegate IsMultipleChannelAudioFrameWanted;
        public event OnPlaybackAudioFrameBeforeMixingExDelegate OnPlaybackAudioFrameBeforeMixingEx;

        bool AudioFrameObserver.OnRecordAudioFrame(AudioFrame frame)
        {
            return OnRecordAudioFrame == null ? true : OnRecordAudioFrame(frame);
        }

        bool AudioFrameObserver.OnPlaybackAudioFrame(AudioFrame frame)
        {
            return OnPlaybackAudioFrame == null ? true : OnPlaybackAudioFrame(frame);
        }

        bool AudioFrameObserver.OnMixedAudioFrame(AudioFrame frame)
        {
            return OnMixedAudioFrame == null ? true : OnMixedAudioFrame(frame);
        }

        bool AudioFrameObserver.OnPlaybackAudioFrameBeforeMixing(ulong uid, AudioFrame frame)
        {
            return OnPlaybackAudioFrameBeforeMixing == null ? true : OnPlaybackAudioFrameBeforeMixing(uid, frame);
        }

        bool AudioFrameObserver.IsMultipleChannelAudioFrameWanted()
        {
            return IsMultipleChannelAudioFrameWanted == null ? false : IsMultipleChannelAudioFrameWanted();
        }

        bool AudioFrameObserver.OnPlaybackAudioFrameBeforeMixingEx(string channel, ulong uid, AudioFrame frame)
        {
            return OnPlaybackAudioFrameBeforeMixingEx == null ? true : OnPlaybackAudioFrameBeforeMixingEx(channel, uid, frame);
        }
        #endregion AudioFrameObserver

    }
}
