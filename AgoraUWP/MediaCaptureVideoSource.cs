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

namespace AgoraUWP
{
    class MediaCaptureVideoSource : AgoraWinRT.VideoSource
    {
        private VideoSourceConsumer m_consumer;
        private MediaFrameReader videoFrameReader;

        public bool OnInitialize(VideoSourceConsumer consumer)
        {
            m_consumer = consumer;
            return this.InitCaptureAsync();
        }

        public void OnDispose()
        {
            videoFrameReader.Dispose();
        }

        public bool OnStart()
        {
            var status = videoFrameReader.StartAsync().AsTask().GetAwaiter().GetResult();
            return status == MediaFrameReaderStartStatus.Success;
        }

        public void OnStop()
        {
            videoFrameReader.StopAsync().AsTask().Wait();
        }

        public VIDEO_PIXEL_FORMAT GetBufferType()
        {
            return VIDEO_PIXEL_FORMAT.VIDEO_PIXEL_NV12;
        }

        public VIDEO_CAPTURE_TYPE GetVideoCaptureType()
        {
            return VIDEO_CAPTURE_TYPE.VIDEO_CAPTURE_CAMERA;
        }

        public VIDEO_CONTENT_HINT GetVideoContentHint()
        {
            return VIDEO_CONTENT_HINT.CONTENT_HINT_NONE;
        }

        private bool InitCaptureAsync()
        {
            var mediaCapture = new MediaCapture();
            var sourceGroup = MediaFrameSourceGroup.FindAllAsync().AsTask().GetAwaiter().GetResult();
            if (sourceGroup.Count == 0) return false;
            mediaCapture.InitializeAsync(
                new MediaCaptureInitializationSettings
                {
                    SourceGroup = sourceGroup[0],
                    SharingMode = MediaCaptureSharingMode.SharedReadOnly,
                    StreamingCaptureMode = StreamingCaptureMode.AudioAndVideo,
                    MemoryPreference = MediaCaptureMemoryPreference.Cpu
                }).AsTask().Wait();
            foreach (MediaFrameSource source in mediaCapture.FrameSources.Values)
            {
                if (source.Info.SourceKind == MediaFrameSourceKind.Color)
                {
                    videoFrameReader = mediaCapture.CreateFrameReaderAsync(source, MediaEncodingSubtypes.Nv12).AsTask().GetAwaiter().GetResult();
                    videoFrameReader.FrameArrived += VideoFrameArrivedEvent;
                    return true;
                }
            }
            return false;
        }

        private void VideoFrameArrivedEvent(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {
            using (var frame = sender.TryAcquireLatestFrame())
            {
                if (frame == null) return;
                var buffer = frame.BufferMediaFrame;
                if (buffer == null) return;
                var format = frame.VideoMediaFrame?.VideoFormat;
                if (format == null) return;
                m_consumer.ConsumeRawVideoFrame(
                    buffer.Buffer.ToArray(),
                    VIDEO_PIXEL_FORMAT.VIDEO_PIXEL_NV12,
                    format.Width, format.Height,
                    0,
                    (ulong)new DateTimeOffset().ToUnixTimeMilliseconds());
            }
        }
    }
}
