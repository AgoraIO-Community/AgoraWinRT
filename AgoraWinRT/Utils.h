#pragma once
#include <string>
#include "pch.h"
#include "AgoraRtc.g.h"

namespace Utils {

	std::string ToString(winrt::hstring const& value) {
		std::wstring wvalue(value.c_str());
		return std::string(wvalue.begin(), wvalue.end());
	}

	winrt::hstring ToString(const char* value) {
		std::string string = std::string(value);
		int size = MultiByteToWideChar(CP_UTF8, 0, &string[0], (int)string.size(), NULL, 0);
		std::wstring wstring(size, 0);
		MultiByteToWideChar(CP_UTF8, 0, &string[0], (int)string.size(), &wstring[0], size);
		return winrt::hstring(wstring);
	}

	winrt::AgoraWinRT::RtcStats FromRaw(agora::rtc::RtcStats const& raw) {
        winrt::AgoraWinRT::RtcStats stats;
        stats.duration = raw.duration;
        stats.txBytes = raw.txBytes;
        stats.rxBytes = raw.rxBytes;
        stats.txAudioBytes = raw.txAudioBytes;
        stats.txVideoBytes = raw.txVideoBytes;
        stats.rxAudioBytes = raw.rxAudioBytes;
        stats.rxVideoBytes = raw.rxVideoBytes;
        stats.txKBitRate = raw.txKBitRate;
        stats.rxKBitRate = raw.rxKBitRate;
        stats.rxAudioKBitRate = raw.rxAudioKBitRate;
        stats.txAudioKBitRate = raw.txAudioKBitRate;
        stats.rxVideoKBitRate = raw.rxVideoKBitRate;
        stats.txVideoKBitRate = raw.txVideoKBitRate;
        stats.lastmileDelay = raw.lastmileDelay;
        stats.txPacketLossRate = raw.txPacketLossRate;
        stats.rxPacketLossRate = raw.rxPacketLossRate;
        stats.userCount = raw.userCount;
        stats.cpuAppUsage = raw.cpuAppUsage;
        stats.cpuTotalUsage = raw.cpuTotalUsage;
        stats.gatewayRtt = raw.gatewayRtt;
        stats.memoryAppUsageRatio = raw.memoryAppUsageRatio;
        stats.memoryTotalUsageRatio = raw.memoryTotalUsageRatio;
        stats.memoryAppUsageInKbytes = raw.memoryAppUsageInKbytes;
        return stats;
	}

    winrt::AgoraWinRT::LocalAudioStats FromRaw(agora::rtc::LocalAudioStats const& raw) {
        winrt::AgoraWinRT::LocalAudioStats stats;
        stats.numChannels = raw.numChannels;
        stats.sentSampleRate = raw.sentSampleRate;
        stats.sentBitrate = raw.sentBitrate;
        stats.txPacketLossRate = raw.txPacketLossRate;
        return stats;
    }

    winrt::AgoraWinRT::LocalVideoStats FromRaw(agora::rtc::LocalVideoStats const& raw) {
        winrt::AgoraWinRT::LocalVideoStats stats;
        stats.sentBitrate = raw.sentBitrate;
        stats.sentFrameRate = raw.sentFrameRate;
        stats.encoderOutputFrameRate = raw.encoderOutputFrameRate;
        stats.rendererOutputFrameRate = raw.rendererOutputFrameRate;
        stats.targetBitrate = raw.targetBitrate;
        stats.targetFrameRate = raw.targetFrameRate;
        stats.qualityAdaptIndication = (winrt::AgoraWinRT::QUALITY_ADAPT_INDICATION)raw.qualityAdaptIndication;
        stats.encodedBitrate = raw.encodedBitrate;
        stats.encodedFrameWidth = raw.encodedFrameWidth;
        stats.encodedFrameHeight = raw.encodedFrameHeight;
        stats.encodedFrameCount = raw.encodedFrameCount;
        stats.codecType = (winrt::AgoraWinRT::VIDEO_CODEC_TYPE)raw.codecType;
        stats.txPacketLossRate = raw.txPacketLossRate;
        stats.captureFrameRate = raw.captureFrameRate;
        return stats;
    }

    winrt::AgoraWinRT::RemoteAudioStats FromRaw(agora::rtc::RemoteAudioStats const& raw) {
        winrt::AgoraWinRT::RemoteAudioStats stats;
        stats.uid = raw.uid;
        stats.quality = raw.quality;
        stats.networkTransportDelay = raw.networkTransportDelay;
        stats.jitterBufferDelay = raw.jitterBufferDelay;
        stats.audioLossRate = raw.audioLossRate;
        stats.numChannels = raw.numChannels;
        stats.receivedSampleRate = raw.receivedSampleRate;
        stats.receivedBitrate = raw.receivedBitrate;
        stats.totalFrozenTime = raw.totalFrozenTime;
        stats.frozenRate = raw.frozenRate;
        stats.totalActiveTime = raw.totalActiveTime;
        stats.publishDuration = raw.publishDuration;
        return stats;
    }

    winrt::AgoraWinRT::RemoteVideoStats FromRaw(agora::rtc::RemoteVideoStats const& raw) {
        winrt::AgoraWinRT::RemoteVideoStats stats;
        stats.uid = raw.uid;
        stats.delay = raw.delay;
        stats.width = raw.width;
        stats.height = raw.height;
        stats.receivedBitrate = raw.receivedBitrate;
        stats.decoderOutputFrameRate = raw.decoderOutputFrameRate;
        stats.rendererOutputFrameRate = raw.rendererOutputFrameRate;
        stats.packetLossRate = raw.packetLossRate;
        stats.rxStreamType = (winrt::AgoraWinRT::REMOTE_VIDEO_STREAM_TYPE)raw.rxStreamType;
        stats.totalFrozenTime = raw.totalFrozenTime;
        stats.frozenRate = raw.frozenRate;
        stats.totalActiveTime = raw.totalActiveTime;
        stats.publishDuration = raw.publishDuration;
        return stats;
    }

    agora::rtc::BeautyOptions ToRaw(winrt::AgoraWinRT::BeautyOptions const& value) {
        agora::rtc::BeautyOptions raw;
        raw.lighteningContrastLevel = (agora::rtc::BeautyOptions::LIGHTENING_CONTRAST_LEVEL)value.lighteningContrastLevel;
        raw.lighteningLevel = value.lighteningLevel;
        raw.smoothnessLevel = value.smoothnessLevel;
        raw.rednessLevel = value.rednessLevel;
        return raw;
    }

    agora::rtc::VideoEncoderConfiguration ToRaw(winrt::AgoraWinRT::VideoEncoderConfiguration const& value) {
        agora::rtc::VideoEncoderConfiguration raw;
        raw.dimensions.width = value.dimensions.width;
        raw.dimensions.height = value.dimensions.height;
        raw.frameRate = (agora::rtc::FRAME_RATE)value.frameRate;
        raw.minFrameRate = value.minFrameRate;
        raw.bitrate = value.bitrate;
        raw.minBitrate = value.minBitrate;
        raw.orientationMode = (agora::rtc::ORIENTATION_MODE)value.orientationMode;
        raw.degradationPreference = (agora::rtc::DEGRADATION_PREFERENCE)value.degradationPreference;
        raw.mirrorMode = (agora::rtc::VIDEO_MIRROR_MODE_TYPE)value.mirrorMode;
        return raw;
    }

}