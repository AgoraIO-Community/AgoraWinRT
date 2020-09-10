#pragma once
#include "LiveTranscoding.g.h"

namespace winrt::AgoraWinRT::implementation
{
	struct LiveTranscoding : LiveTranscodingT<LiveTranscoding>
	{
		LiveTranscoding();

		~LiveTranscoding();

		void Close();

		int32_t width();
		void width(int32_t value);
		int32_t height();
		void height(int32_t value);
		int32_t videoBitrate();
		void videoBitrate(int32_t value);
		int32_t videoFramerate();
		void videoFramerate(int32_t value);
		bool lowLatency();
		void lowLatency(bool value);
		int32_t videoGop();
		void videoGop(int32_t value);
		AgoraWinRT::VIDEO_CODEC_PROFILE_TYPE videoCodecProfile();
		void videoCodecProfile(AgoraWinRT::VIDEO_CODEC_PROFILE_TYPE const& value);
		uint64_t backgroundColor();
		void backgroundColor(uint64_t value);
		com_array<AgoraWinRT::TranscodingUser> transcodingUsers();
		void transcodingUsers(array_view<AgoraWinRT::TranscodingUser const> value);
		hstring transcodingExtraInfo();
		void transcodingExtraInfo(hstring const& value);
		hstring metadata();
		void metadata(hstring const& value);
		AgoraWinRT::RtcImage watermark();
		void watermark(AgoraWinRT::RtcImage const& value);
		AgoraWinRT::RtcImage backgroundImage();
		void backgroundImage(AgoraWinRT::RtcImage const& value);
		AgoraWinRT::AUDIO_SAMPLE_RATE_TYPE audioSampleRate();
		void audioSampleRate(AgoraWinRT::AUDIO_SAMPLE_RATE_TYPE const& value);
		int32_t audioBitrate();
		void audioBitrate(int32_t value);
		int32_t audioChannels();
		void audioChannels(int32_t value);
		AgoraWinRT::AUDIO_CODEC_PROFILE_TYPE audioCodecProfile();
		void audioCodecProfile(AgoraWinRT::AUDIO_CODEC_PROFILE_TYPE const& value);
		com_array<AgoraWinRT::LiveStreamAdvancedFeature> advancedFeatures();
		void advancedFeatures(array_view<AgoraWinRT::LiveStreamAdvancedFeature const> value);
	private:
		int32_t m_Width;
		int32_t m_Height;
		int32_t m_VideoBitrate;
		int32_t m_VideoFramerate;
		bool m_LowLatency;
		int32_t m_VideoGop;
		AgoraWinRT::VIDEO_CODEC_PROFILE_TYPE m_VideoCodecProfile;
		uint64_t m_BackgroundColor;
		com_array<AgoraWinRT::TranscodingUser> m_TranscodingUsers;
		hstring m_TranscodingExtraInfo;
		hstring m_Metadata;
		AgoraWinRT::RtcImage m_Watermark;
		AgoraWinRT::RtcImage m_BackgroundImage;
		AgoraWinRT::AUDIO_SAMPLE_RATE_TYPE m_AudioSampleRate;
		int32_t m_AudioBitrate;
		int32_t m_AudioChannels;
		AgoraWinRT::AUDIO_CODEC_PROFILE_TYPE m_AudioCodecProfile;
		com_array<AgoraWinRT::LiveStreamAdvancedFeature> m_AdvancedFeatures;
	};
}
