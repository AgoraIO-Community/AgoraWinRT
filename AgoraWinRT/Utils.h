#pragma once
#include <string>
#include "pch.h"
#include "Metadata.h"
#include "Packet.h"
#include "AudioFrame.h"
#include "VideoFrame.h"

namespace Utils {

	std::string ToString(winrt::hstring const& value);

	char* Copy(winrt::hstring const& value);

	winrt::hstring To(const char* value);

	winrt::hstring To(const char* value, size_t length);

	winrt::AgoraWinRT::RtcStats FromRaw(agora::rtc::RtcStats const& raw);

	winrt::AgoraWinRT::LocalAudioStats FromRaw(agora::rtc::LocalAudioStats const& raw);

	winrt::AgoraWinRT::LocalVideoStats FromRaw(agora::rtc::LocalVideoStats const& raw);

	winrt::AgoraWinRT::RemoteAudioStats FromRaw(agora::rtc::RemoteAudioStats const& raw);

	winrt::AgoraWinRT::RemoteVideoStats FromRaw(agora::rtc::RemoteVideoStats const& raw);

	winrt::com_array<winrt::AgoraWinRT::AudioVolumeInfo> FromRaw(const agora::rtc::AudioVolumeInfo* raw, int count);

	winrt::AgoraWinRT::LastmileProbeOneWayResult FromRaw(const agora::rtc::LastmileProbeOneWayResult& raw);

	winrt::AgoraWinRT::LastmileProbeResult FromRaw(const agora::rtc::LastmileProbeResult& raw);

	winrt::com_array<uint8_t> FromRaw(void* raw, int count);

	winrt::com_ptr<winrt::AgoraWinRT::implementation::Metadata> FromRaw(const agora::rtc::IMetadataObserver::Metadata& raw);

	winrt::com_ptr<winrt::AgoraWinRT::implementation::Packet> FromRaw(const agora::rtc::IPacketObserver::Packet& raw);

	winrt::com_ptr<winrt::AgoraWinRT::implementation::AudioFrame> FromRaw(const agora::media::IAudioFrameObserver::AudioFrame& raw);

	winrt::com_ptr<winrt::AgoraWinRT::implementation::VideoFrame> FromRaw(const agora::media::IVideoFrameObserver::VideoFrame& raw);

	void* ToRaw(winrt::com_array<uint8_t> const& value);

	void ToRaw(winrt::com_ptr<winrt::AgoraWinRT::implementation::VideoFrame> const& value, agora::media::IVideoFrameObserver::VideoFrame& raw);

	agora::rtc::BeautyOptions ToRaw(winrt::AgoraWinRT::BeautyOptions const& value);

	agora::rtc::VideoEncoderConfiguration ToRaw(winrt::AgoraWinRT::VideoEncoderConfiguration const& value);

	agora::rtc::TranscodingUser* ToRaw(winrt::com_array<winrt::AgoraWinRT::TranscodingUser> const& value);

	agora::rtc::RtcImage* ToRaw(winrt::AgoraWinRT::RtcImage const& value);

	agora::rtc::LiveStreamAdvancedFeature* ToRaw(winrt::com_array<winrt::AgoraWinRT::LiveStreamAdvancedFeature> const& value);

	agora::rtc::LiveTranscoding ToRaw(winrt::AgoraWinRT::LiveTranscoding const& value);

	agora::rtc::ChannelMediaInfo* ToRaw(winrt::AgoraWinRT::ChannelMediaInfo const& value);

	agora::rtc::ChannelMediaInfo* ToRaw(winrt::com_array<winrt::AgoraWinRT::ChannelMediaInfo> const& value);

	agora::rtc::ChannelMediaRelayConfiguration ToRaw(winrt::AgoraWinRT::ChannelMediaRelayConfiguration const& value);

	agora::rtc::LastmileProbeConfig ToRaw(winrt::AgoraWinRT::LastmileProbeConfig const& value);


	agora::media::ExternalVideoFrame* ToRaw(winrt::AgoraWinRT::ExternalVideoFrame const& value);

	void Free(agora::media::ExternalVideoFrame* value);

	agora::media::IAudioFrameObserver::AudioFrame* ToRaw(winrt::AgoraWinRT::AudioFrame const& value);

	void Free(agora::media::IAudioFrameObserver::AudioFrame* value);

	agora::rtc::Rectangle ToRaw(winrt::AgoraWinRT::Rectangle const& value);

	agora::rtc::WatermarkOptions ToRaw(winrt::AgoraWinRT::WatermarkOptions const& value);

	agora::rtc::EncryptionConfig ToRaw(winrt::AgoraWinRT::EncryptionConfig const& value);

	agora::rtc::InjectStreamConfig ToRaw(winrt::AgoraWinRT::InjectStreamConfig const& value);

	agora::rtc::CameraCapturerConfiguration ToRaw(winrt::AgoraWinRT::CameraCapturerConfiguration const& config);

	agora::rtc::ChannelMediaOptions ToRaw(winrt::AgoraWinRT::ChannelMediaOptions const& value);

}