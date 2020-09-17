using AgoraWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Windows.Media.MediaProperties;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml.Media;

namespace AgoraUWP
{
    public class AgoraRtc : AgoraWinRT.AgoraRtc
    {
        private VideoCanvas localVideo = null;

        public AgoraRtc(string vendorKey): base(vendorKey)
        {
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
                    MemoryPreference = MediaCaptureMemoryPreference.Auto,
                });
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

            }
        }

        public void SetupLocalVideo(VideoCanvas videoCanvas)
        {
            this.localVideo = videoCanvas;
        }

        public void StartPreview()
        {
            throw new NotImplementedException();
        }
    }
}
