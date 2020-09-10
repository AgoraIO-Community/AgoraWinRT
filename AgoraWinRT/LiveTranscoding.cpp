#include "pch.h"
#include "LiveTranscoding.h"
#include "LiveTranscoding.g.cpp"

namespace winrt::AgoraWinRT::implementation
{
    LiveTranscoding::LiveTranscoding() :
        m_TranscodingUsers(winrt::com_array<AgoraWinRT::TranscodingUser>()),
        m_AdvancedFeatures(winrt::com_array<AgoraWinRT::LiveStreamAdvancedFeature>())
    {}

    LiveTranscoding::~LiveTranscoding()
    {
        Close();
    }
    void LiveTranscoding::Close()
    {
        m_TranscodingUsers.clear();
        m_AdvancedFeatures.clear();
    }
    int32_t LiveTranscoding::width()
    {
        return m_Width;
    }
    void LiveTranscoding::width(int32_t value)
    {
        m_Width = value;
    }
    int32_t LiveTranscoding::height()
    {
        return m_Height;
    }
    void LiveTranscoding::height(int32_t value)
    {
        m_Height = value;
    }
    int32_t LiveTranscoding::videoBitrate()
    {
        return m_VideoBitrate;
    }
    void LiveTranscoding::videoBitrate(int32_t value)
    {
        m_VideoBitrate = value;
    }
    int32_t LiveTranscoding::videoFramerate()
    {
        return m_VideoFramerate;
    }
    void LiveTranscoding::videoFramerate(int32_t value)
    {
        m_VideoFramerate = value;
    }
    bool LiveTranscoding::lowLatency()
    {
        return m_LowLatency;
    }
    void LiveTranscoding::lowLatency(bool value)
    {
        m_LowLatency = value;
    }
    int32_t LiveTranscoding::videoGop()
    {
        return m_VideoGop;
    }
    void LiveTranscoding::videoGop(int32_t value)
    {
        m_VideoGop = value;
    }
    AgoraWinRT::VIDEO_CODEC_PROFILE_TYPE LiveTranscoding::videoCodecProfile()
    {
        return m_VideoCodecProfile;
    }
    void LiveTranscoding::videoCodecProfile(AgoraWinRT::VIDEO_CODEC_PROFILE_TYPE const& value)
    {
        m_VideoCodecProfile = value;
    }
    uint64_t LiveTranscoding::backgroundColor()
    {
        return m_BackgroundColor;
    }
    void LiveTranscoding::backgroundColor(uint64_t value)
    {
        m_BackgroundColor = value;
    }
    com_array<AgoraWinRT::TranscodingUser> LiveTranscoding::transcodingUsers()
    {
        return com_array(m_TranscodingUsers.begin(), m_TranscodingUsers.end());
    }
    void LiveTranscoding::transcodingUsers(array_view<AgoraWinRT::TranscodingUser const> value)
    {
        m_TranscodingUsers.clear();
        m_TranscodingUsers = com_array(value.begin(), value.end());
    }
    hstring LiveTranscoding::transcodingExtraInfo()
    {
        return m_TranscodingExtraInfo;
    }
    void LiveTranscoding::transcodingExtraInfo(hstring const& value)
    {
        m_TranscodingExtraInfo = value;
    }
    hstring LiveTranscoding::metadata()
    {
        return m_Metadata;
    }
    void LiveTranscoding::metadata(hstring const& value)
    {
        m_Metadata = value;
    }
    AgoraWinRT::RtcImage LiveTranscoding::watermark()
    {
        return m_Watermark;
    }
    void LiveTranscoding::watermark(AgoraWinRT::RtcImage const& value)
    {
        m_Watermark = value;
    }
    AgoraWinRT::RtcImage LiveTranscoding::backgroundImage()
    {
        return m_BackgroundImage;
    }
    void LiveTranscoding::backgroundImage(AgoraWinRT::RtcImage const& value)
    {
        m_BackgroundImage = value;
    }
    AgoraWinRT::AUDIO_SAMPLE_RATE_TYPE LiveTranscoding::audioSampleRate()
    {
        return m_AudioSampleRate;
    }
    void LiveTranscoding::audioSampleRate(AgoraWinRT::AUDIO_SAMPLE_RATE_TYPE const& value)
    {
        m_AudioSampleRate = value;
    }
    int32_t LiveTranscoding::audioBitrate()
    {
        return m_AudioBitrate;
    }
    void LiveTranscoding::audioBitrate(int32_t value)
    {
        m_AudioBitrate = value;
    }
    int32_t LiveTranscoding::audioChannels()
    {
        return m_AudioChannels;
    }
    void LiveTranscoding::audioChannels(int32_t value)
    {
        m_AudioChannels = value;
    }
    AgoraWinRT::AUDIO_CODEC_PROFILE_TYPE LiveTranscoding::audioCodecProfile()
    {
        return m_AudioCodecProfile;
    }
    void LiveTranscoding::audioCodecProfile(AgoraWinRT::AUDIO_CODEC_PROFILE_TYPE const& value)
    {
        m_AudioCodecProfile = value;
    }
    com_array<AgoraWinRT::LiveStreamAdvancedFeature> LiveTranscoding::advancedFeatures()
    {
        return com_array(m_AdvancedFeatures.begin(), m_AdvancedFeatures.end());
    }
    void LiveTranscoding::advancedFeatures(array_view<AgoraWinRT::LiveStreamAdvancedFeature const> value)
    {
        m_AdvancedFeatures.clear();
        m_AdvancedFeatures = com_array(value.begin(), value.end());
    }
}
