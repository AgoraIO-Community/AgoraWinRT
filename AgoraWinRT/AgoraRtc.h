#pragma once
#include "pch.h"
#include "AgoraRtc.g.h"

namespace winrt::AgoraWinRT::implementation
{
    struct AgoraRtc : AgoraRtcT<AgoraRtc>,
        public agora::rtc::IRtcEngineEventHandler
    {
        AgoraRtc() = default;

        AgoraRtc(hstring const& vendorKey);
        //频道管理
        int16_t SetChannelProfile(AgoraWinRT::CHANNEL_PROFILE_TYPE const& type);
        int16_t SetClientRole(AgoraWinRT::CLIENT_ROLE_TYPE const& type);
        int16_t JoinChannel(hstring const& token, hstring const& channel, hstring const& info, uint64_t uid);
        int16_t SwitchChannel(hstring const& token, hstring const& channel);
        int16_t LeaveChannel();
        int16_t RenewToken(hstring const& token);
        AgoraWinRT::CONNECTION_STATE_TYPE GetConnectionState();
        void Close();
        //音频管理
        int16_t EnableAudio();
        int16_t DisableAudio();
        int16_t SetAudioProfile(AgoraWinRT::AUDIO_PROFILE_TYPE const& profile, AgoraWinRT::AUDIO_SCENARIO_TYPE const& scenario);
        int16_t AdjustRecordingSignalVolume(int16_t volume);
        int16_t AdjustUserPlaybackSignalVolume(uint64_t uid, int16_t volume);
        int16_t AdjustPlaybackSignalVolume(int16_t volume);
        int16_t EnableLocalAudio(bool enabled);
        int16_t MuteLocalAudioStream(bool muted);
        int16_t MuteRemoteAudioStream(uint64_t uid, bool muted);
        int16_t MuteAllRemoteAudioStreams(bool muted);
        int16_t SetDefaultMuteAllRemoteAudioStreams(bool muted);
        //视频管理
        int16_t EnableVideo();
        int16_t DisableVideo();
        int16_t SetVideoEncoderConfiguration(AgoraWinRT::VideoEncoderConfiguration const& config);
        int16_t MuteLocalVideoStream(bool muted);
        int16_t MuteRemoteVideoStream(uint64_t uid, bool muted);
        int16_t MuteAllRemoteVideoStream(bool muted);
        int16_t SetDefaultMuteAllRemoteVideoStreams(bool muted);
        //视频前处理及后处理
        int16_t SetBeautyEffectOptions(bool enabled, AgoraWinRT::BeautyOptions const& options);
    private:
        agora::rtc::IRtcEngine* m_rtcEngine{ nullptr };
        AgoraWinRT::AgoraRtcEventHandler m_handler{ nullptr };

    private:
        //频道管理事件
        void onConnectionStateChanged(agora::rtc::CONNECTION_STATE_TYPE type, agora::rtc::CONNECTION_CHANGED_REASON_TYPE reason) override;
        void onJoinChannelSuccess(const char* channel, agora::rtc::uid_t uid, int elapsed) override;
        void onRejoinChannelSuccess(const char* channel, agora::rtc::uid_t uid, int elapsed) override;
        void onLeaveChannel(const agora::rtc::RtcStats& value) override;
        void onClientRoleChanged(agora::rtc::CLIENT_ROLE_TYPE oldRole, agora::rtc::CLIENT_ROLE_TYPE newRole) override;
        void onUserJoined(agora::rtc::uid_t uid, int elapsed) override;
        void onUserOffline(agora::rtc::uid_t uid, agora::rtc::USER_OFFLINE_REASON_TYPE reason) override;
        void onNetworkTypeChanged(agora::rtc::NETWORK_TYPE type);
        void onConnectionLost() override;
        void onTokenPrivilegeWillExpire(const char* token) override;
        void onRequestToken() override;
        //本地媒体事件
        void onLocalAudioStateChanged(agora::rtc::LOCAL_AUDIO_STREAM_STATE state, agora::rtc::LOCAL_AUDIO_STREAM_ERROR error) override;
        void onLocalVideoStateChanged(agora::rtc::LOCAL_VIDEO_STREAM_STATE state, agora::rtc::LOCAL_VIDEO_STREAM_ERROR error) override;
        void onFirstLocalAudioFramePublished(int elapsed) override;
        void onFirstLocalVideoFramePublished(int elapsed) override;
        void onAudioPublishStateChanged(const char* channel, agora::rtc::STREAM_PUBLISH_STATE oldState, agora::rtc::STREAM_PUBLISH_STATE newState, int elapsed) override;
        void onVideoPublishStateChanged(const char* channel, agora::rtc::STREAM_PUBLISH_STATE oldState, agora::rtc::STREAM_PUBLISH_STATE newState, int elapsed) override;
        //远端媒体事件
        void onRemoteAudioStateChanged(agora::rtc::uid_t uid, agora::rtc::REMOTE_AUDIO_STATE state, agora::rtc::REMOTE_AUDIO_STATE_REASON reason, int elapsed);
        void onRemoteVideoStateChanged(agora::rtc::uid_t uid, agora::rtc::REMOTE_VIDEO_STATE state, agora::rtc::REMOTE_VIDEO_STATE_REASON reason, int elapsed);
        void onFirstRemoteVideoFrame(agora::rtc::uid_t uid, int width, int height, int elapsed);
        void onAudioSubscribeStateChanged(const char* channel, agora::rtc::uid_t uid, agora::rtc::STREAM_SUBSCRIBE_STATE oldState, agora::rtc::STREAM_SUBSCRIBE_STATE newState, int elapsed);
        void onVideoSubscribeStateChanged(const char* channel, agora::rtc::uid_t uid, agora::rtc::STREAM_SUBSCRIBE_STATE oldState, agora::rtc::STREAM_SUBSCRIBE_STATE newState, int elapsed);
        //数据统计事件
        void onRtcStats(const agora::rtc::RtcStats& stats) override;
        void onNetworkQuality(agora::rtc::uid_t uid, int txQuality, int rxQuality) override;
        void onLocalAudioStats(const agora::rtc::LocalAudioStats& stats) override;
        void onLocalVideoStats(const agora::rtc::LocalVideoStats& stats) override;
        void onRemoteAudioStats(const agora::rtc::RemoteAudioStats& stats) override;
        void onRemoteVideoStats(const agora::rtc::RemoteVideoStats& stats) override;
    };
}
namespace winrt::AgoraWinRT::factory_implementation
{
    struct AgoraRtc : AgoraRtcT<AgoraRtc, implementation::AgoraRtc>
    {
    };
}
