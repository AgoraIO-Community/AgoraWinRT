#include "pch.h"
#include "AgoraRtc.h"
#include "AgoraRtc.g.cpp"
#include "Utils.h"

namespace winrt::AgoraWinRT::implementation
{
    AgoraRtc::AgoraRtc(hstring const& vendorKey)
    {
        if (vendorKey.empty())
            throw hresult_invalid_argument(L"vendorKey is empty");

        m_rtcEngine = createAgoraRtcEngine();
        if (m_rtcEngine == nullptr)
            throw L"createAgoraRtcEngine failed";

        agora::rtc::RtcEngineContext context;
        std::string tmp = Utils::ToString(vendorKey);
        context.appId = tmp.c_str();
        context.eventHandler = this;
        if (m_rtcEngine->initialize(context) == 0)
        {
            //ÉèÖÃIMediaEngine½Ó¿Ú
        }
        else
            throw L"rtcEngine initialize failed";
    }
    int16_t AgoraRtc::SetChannelProfile(AgoraWinRT::CHANNEL_PROFILE_TYPE const& type)
    {
        return m_rtcEngine->setChannelProfile((agora::rtc::CHANNEL_PROFILE_TYPE)type);
    }
    int16_t AgoraRtc::SetClientRole(AgoraWinRT::CLIENT_ROLE_TYPE const& type)
    {
        return m_rtcEngine->setClientRole((agora::rtc::CLIENT_ROLE_TYPE)type);
    }
    int16_t AgoraRtc::JoinChannel(hstring const& token, hstring const& channel, hstring const& info, uint64_t uid)
    {
        return m_rtcEngine->joinChannel(Utils::ToString(token).c_str(), Utils::ToString(channel).c_str(), Utils::ToString(info).c_str(), uid);
    }
    int16_t AgoraRtc::SwitchChannel(hstring const& token, hstring const& channel)
    {
        return m_rtcEngine->switchChannel(Utils::ToString(token).c_str(), Utils::ToString(channel).c_str());
    }
    int16_t AgoraRtc::LeaveChannel()
    {
        return m_rtcEngine->leaveChannel();
    }
    int16_t AgoraRtc::RenewToken(hstring const& token)
    {
        return m_rtcEngine->renewToken(Utils::ToString(token).c_str());
    }
    AgoraWinRT::CONNECTION_STATE_TYPE AgoraRtc::GetConnectionState()
    {
        return (AgoraWinRT::CONNECTION_STATE_TYPE)m_rtcEngine->getConnectionState();
    }
    void AgoraRtc::Close()
    {
        m_rtcEngine->release(true);
    }
    int16_t AgoraRtc::EnableAudio()
    {
        return m_rtcEngine->enableAudio();
    }
    int16_t AgoraRtc::DisableAudio()
    {
        return m_rtcEngine->disableAudio();
    }
    int16_t AgoraRtc::SetAudioProfile(AgoraWinRT::AUDIO_PROFILE_TYPE const& profile, AgoraWinRT::AUDIO_SCENARIO_TYPE const& scenario)
    {
        return m_rtcEngine->setAudioProfile((agora::rtc::AUDIO_PROFILE_TYPE)profile, (agora::rtc::AUDIO_SCENARIO_TYPE)scenario);
    }
    int16_t AgoraRtc::AdjustRecordingSignalVolume(int16_t volume)
    {
        return m_rtcEngine->adjustRecordingSignalVolume(volume);
    }
    int16_t AgoraRtc::AdjustUserPlaybackSignalVolume(uint64_t uid, int16_t volume)
    {
        return m_rtcEngine->adjustUserPlaybackSignalVolume(uid, volume);
    }
    int16_t AgoraRtc::AdjustPlaybackSignalVolume(int16_t volume)
    {
        return m_rtcEngine->adjustPlaybackSignalVolume(volume);
    }
    int16_t AgoraRtc::EnableLocalAudio(bool enabled)
    {
        return m_rtcEngine->enableLocalAudio(enabled);
    }
    int16_t AgoraRtc::MuteLocalAudioStream(bool muted)
    {
        return m_rtcEngine->muteLocalAudioStream(muted);
    }
    int16_t AgoraRtc::MuteRemoteAudioStream(uint64_t uid, bool muted)
    {
        return m_rtcEngine->muteRemoteAudioStream(uid, muted);
    }
    int16_t AgoraRtc::MuteAllRemoteAudioStreams(bool muted)
    {
        return m_rtcEngine->muteAllRemoteAudioStreams(muted);
    }
    int16_t AgoraRtc::SetDefaultMuteAllRemoteAudioStreams(bool muted)
    {
        return m_rtcEngine->setDefaultMuteAllRemoteAudioStreams(muted);
    }
    int16_t AgoraRtc::EnableVideo()
    {
        return m_rtcEngine->enableVideo();
    }
    int16_t AgoraRtc::DisableVideo()
    {
        return m_rtcEngine->disableVideo();
    }
    int16_t AgoraRtc::SetVideoEncoderConfiguration(AgoraWinRT::VideoEncoderConfiguration const& config)
    {
        return m_rtcEngine->setVideoEncoderConfiguration(Utils::ToRaw(config));
    }
    int16_t AgoraRtc::MuteLocalVideoStream(bool muted)
    {
        return m_rtcEngine->muteLocalVideoStream(muted);
    }
    int16_t AgoraRtc::MuteRemoteVideoStream(uint64_t uid, bool muted)
    {
        return m_rtcEngine->muteRemoteVideoStream(uid, muted);
    }
    int16_t AgoraRtc::MuteAllRemoteVideoStream(bool muted)
    {
        return m_rtcEngine->muteAllRemoteVideoStreams(muted);
    }
    int16_t AgoraRtc::SetDefaultMuteAllRemoteVideoStreams(bool muted)
    {
        return m_rtcEngine->setDefaultMuteAllRemoteVideoStreams(muted);
    }
    int16_t AgoraRtc::SetBeautyEffectOptions(bool enabled, AgoraWinRT::BeautyOptions const& options)
    {
        return m_rtcEngine->setBeautyEffectOptions(enabled, Utils::ToRaw(options));
    }
    void AgoraRtc::onConnectionStateChanged(agora::rtc::CONNECTION_STATE_TYPE type, agora::rtc::CONNECTION_CHANGED_REASON_TYPE reason)
    {
        if (m_handler) m_handler.OnConnectionStateChanged((CONNECTION_STATE_TYPE)type, (CONNECTION_CHANGED_REASON_TYPE)reason);
    }
    void AgoraRtc::onJoinChannelSuccess(const char* channel, agora::rtc::uid_t uid, int elapsed)
    {
        if (m_handler) m_handler.OnJoinChannelSuccess(Utils::ToString(channel), uid, elapsed);
    }
    void AgoraRtc::onRejoinChannelSuccess(const char* channel, agora::rtc::uid_t uid, int elapsed)
    {
        if (m_handler) m_handler.OnRejoinChannelSuccess(Utils::ToString(channel), uid, elapsed);
    }
    void AgoraRtc::onLeaveChannel(const agora::rtc::RtcStats& value)
    {
        if (m_handler) {
            m_handler.OnLeaveChannel(Utils::FromRaw(value));
        }
    }
    void AgoraRtc::onClientRoleChanged(agora::rtc::CLIENT_ROLE_TYPE oldRole, agora::rtc::CLIENT_ROLE_TYPE newRole)
    {
        if (m_handler) m_handler.OnClientRoleChanged((CLIENT_ROLE_TYPE)oldRole, (CLIENT_ROLE_TYPE)newRole);
    }
    void AgoraRtc::onUserJoined(agora::rtc::uid_t uid, int elapsed)
    {
        if (m_handler) m_handler.OnUserJoined(uid, elapsed);
    }
    void AgoraRtc::onUserOffline(agora::rtc::uid_t uid, agora::rtc::USER_OFFLINE_REASON_TYPE reason)
    {
        if (m_handler) m_handler.OnUserOffline(uid, (USER_OFFLINE_REASON_TYPE)reason);
    }
    void AgoraRtc::onNetworkTypeChanged(agora::rtc::NETWORK_TYPE type)
    {
        if (m_handler) m_handler.OnNetworkTypeChanged((NETWORK_TYPE)type);
    }
    void AgoraRtc::onConnectionLost()
    {
        if (m_handler) m_handler.OnConnectionLost();
    }
    void AgoraRtc::onTokenPrivilegeWillExpire(const char* token)
    {
        if (m_handler) m_handler.OnTokenPrivilegeWillExpire(Utils::ToString(token));
    }
    void AgoraRtc::onRequestToken()
    {
        if (m_handler) m_handler.OnRequestToken();
    }
    void AgoraRtc::onLocalAudioStateChanged(agora::rtc::LOCAL_AUDIO_STREAM_STATE state, agora::rtc::LOCAL_AUDIO_STREAM_ERROR error)
    {
        if (m_handler) m_handler.OnLocalAudioStateChanged((AgoraWinRT::LOCAL_AUDIO_STREAM_STATE)state, (AgoraWinRT::LOCAL_AUDIO_STREAM_ERROR)error);
    }
    void AgoraRtc::onLocalVideoStateChanged(agora::rtc::LOCAL_VIDEO_STREAM_STATE state, agora::rtc::LOCAL_VIDEO_STREAM_ERROR error)
    {
        if (m_handler) m_handler.OnLocalVideoStateChanged((AgoraWinRT::LOCAL_VIDEO_STREAM_STATE)state, (AgoraWinRT::LOCAL_VIDEO_STREAM_ERROR)error);
    }
    void AgoraRtc::onFirstLocalAudioFramePublished(int elapsed)
    {
        if (m_handler) m_handler.OnFirstLocalAudioFramePublished(elapsed);
    }
    void AgoraRtc::onFirstLocalVideoFramePublished(int elapsed)
    {
        if (m_handler) m_handler.OnFirstLocalVideoFramePublished(elapsed);
    }
    void AgoraRtc::onAudioPublishStateChanged(const char* channel, agora::rtc::STREAM_PUBLISH_STATE oldState, agora::rtc::STREAM_PUBLISH_STATE newState, int elapsed)
    {
        if (m_handler) m_handler.OnAudioPublishStateChanged(Utils::ToString(channel), (AgoraWinRT::STREAM_PUBLISH_STATE)oldState, (AgoraWinRT::STREAM_PUBLISH_STATE)newState, elapsed);
    }
    void AgoraRtc::onVideoPublishStateChanged(const char* channel, agora::rtc::STREAM_PUBLISH_STATE oldState, agora::rtc::STREAM_PUBLISH_STATE newState, int elapsed)
    {
        if (m_handler) m_handler.OnVideoPublishStateChanged(Utils::ToString(channel), (AgoraWinRT::STREAM_PUBLISH_STATE)oldState, (AgoraWinRT::STREAM_PUBLISH_STATE)newState, elapsed);
    }
    void AgoraRtc::onRemoteAudioStateChanged(agora::rtc::uid_t uid, agora::rtc::REMOTE_AUDIO_STATE state, agora::rtc::REMOTE_AUDIO_STATE_REASON reason, int elapsed)
    {
        if (m_handler) m_handler.OnRemoteAudioStateChanged(uid, (AgoraWinRT::REMOTE_AUDIO_STATE)state, (AgoraWinRT::REMOTE_AUDIO_STATE_REASON)reason, elapsed);
    }
    void AgoraRtc::onRemoteVideoStateChanged(agora::rtc::uid_t uid, agora::rtc::REMOTE_VIDEO_STATE state, agora::rtc::REMOTE_VIDEO_STATE_REASON reason, int elapsed)
    {
        if (m_handler) m_handler.OnRemoteVideoStateChanged(uid, (AgoraWinRT::REMOTE_VIDEO_STATE)state, (AgoraWinRT::REMOTE_VIDEO_STATE_REASON)reason, elapsed);
    }
    void AgoraRtc::onFirstRemoteVideoFrame(agora::rtc::uid_t uid, int width, int height, int elapsed)
    {
        if (m_handler) m_handler.OnFirstRemoteVideoFrame(uid, width, height, elapsed);
    }
    void AgoraRtc::onAudioSubscribeStateChanged(const char* channel, agora::rtc::uid_t uid, agora::rtc::STREAM_SUBSCRIBE_STATE oldState, agora::rtc::STREAM_SUBSCRIBE_STATE newState, int elapsed)
    {
        if (m_handler) m_handler.OnAudioSubscribeStateChanged(Utils::ToString(channel), uid, (AgoraWinRT::STREAM_SUBSCRIBE_STATE)oldState, (AgoraWinRT::STREAM_SUBSCRIBE_STATE)newState, elapsed);
    }
    void AgoraRtc::onVideoSubscribeStateChanged(const char* channel, agora::rtc::uid_t uid, agora::rtc::STREAM_SUBSCRIBE_STATE oldState, agora::rtc::STREAM_SUBSCRIBE_STATE newState, int elapsed)
    {
        if (m_handler) m_handler.OnVideoSubscribeStateChanged(Utils::ToString(channel), uid, (AgoraWinRT::STREAM_SUBSCRIBE_STATE)oldState, (AgoraWinRT::STREAM_SUBSCRIBE_STATE)newState, elapsed);
    }
    void AgoraRtc::onRtcStats(const agora::rtc::RtcStats& stats)
    {
        if (m_handler) m_handler.OnRtcStats(Utils::FromRaw(stats));
    }
    void AgoraRtc::onNetworkQuality(agora::rtc::uid_t uid, int txQuality, int rxQuality)
    {
        if (m_handler) m_handler.OnNetworkQuality(uid, (AgoraWinRT::QUALITY_TYPE)txQuality, (AgoraWinRT::QUALITY_TYPE)rxQuality);
    }
    void AgoraRtc::onLocalAudioStats(const agora::rtc::LocalAudioStats& stats)
    {
        if (m_handler) m_handler.OnLocalAudioStats(Utils::FromRaw(stats));
    }
    void AgoraRtc::onLocalVideoStats(const agora::rtc::LocalVideoStats& stats)
    {
        if (m_handler) m_handler.OnLocalVideoStats(Utils::FromRaw(stats));
    }
    void AgoraRtc::onRemoteAudioStats(const agora::rtc::RemoteAudioStats& stats)
    {
        if (m_handler) m_handler.OnRemoteAudioStats(Utils::FromRaw(stats));
    }
    void AgoraRtc::onRemoteVideoStats(const agora::rtc::RemoteVideoStats& stats)
    {
        if (m_handler) m_handler.OnRemoteVideoStats(Utils::FromRaw(stats));
    }
}
