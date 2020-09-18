using AgoraWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Capture.Frames;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace AgoraUWP
{
    public abstract class VideoCanvas
    {
        public abstract object Target { get; set; }
        public AgoraWinRT.RENDER_MODE_TYPE RenderMode { get; set; } = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE;
        public String Channel { get; set; } = null;
        public UInt64 User { get; set; } = 0;
        public AgoraWinRT.VIDEO_MIRROR_MODE_TYPE MirrorMode { get; set; } = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_DISABLED;

        public virtual void Render(MediaFrameReference frame) { }

        public virtual void Render(VideoFrame frame) { }
    }

    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }

    public class ImageVideoCanvas : VideoCanvas
    {
        private Image target;
        private SoftwareBitmap backBuffer;
        private bool running;
        private SoftwareBitmapSource source;

        public override object Target { 
            get=> target; 
            set {
                target = value as Image;
                if (target != null)
                {
                    target.Source = new SoftwareBitmapSource();
                    this.source = (SoftwareBitmapSource)target.Source;
                }
            } }

        public override void Render(MediaFrameReference frame)
        {
            if (source == null) return;
            RenderBitmap(ConvertToImage(frame?.VideoMediaFrame));
        }

        private void RenderBitmap(SoftwareBitmap bitmap)
        {
            if (bitmap == null) return;
            bitmap = Interlocked.Exchange(ref this.backBuffer, bitmap);
            bitmap?.Dispose();

            _ = target.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                async () =>
                {
                    if (this.running) return;
                    this.running = true;

                    SoftwareBitmap tempBitmap;
                    while ((tempBitmap = Interlocked.Exchange(ref this.backBuffer, null)) != null)
                    {
                  
                        await source?.SetBitmapAsync(tempBitmap);
                        tempBitmap.Dispose();
                    }

                    this.running = false;
                });
        }

        private unsafe SoftwareBitmap ConvertToImage(VideoMediaFrame input)
        {
            using (var inputBitmap = input?.SoftwareBitmap)
            {
                if (inputBitmap == null) return null;
                return SoftwareBitmap.Convert(inputBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore);
            }
        }

        public override void Render(VideoFrame frame)
        {
            if (source == null) return;
            var nvBuffer = ConvertToNv12(frame);
            var image = ConvertToImage(nvBuffer, (int)frame.width, (int)frame.height);
            RenderBitmap(image);
        }

        private byte[] ConvertToNv12(VideoFrame frame)
        {
            var size = frame.width * frame.height;
            var totalLength = size * 3 / 2;
            var result = new byte[totalLength];
            var ybuffer = frame.yBuffer;
            Array.Copy(ybuffer, result, ybuffer.Length);
            byte[] ubuffer = frame.uBuffer, vbuffer = frame.vBuffer;
            for (int i = (int)size, j = 0; i < totalLength; j++)
            {
                result[i++] = ubuffer[j];
                result[i++] = vbuffer[j];
            }
            return result;
        }

        private unsafe SoftwareBitmap ConvertToImage(byte[] input, int width, int height)
        {
            using (var yuv = SoftwareBitmap.CreateCopyFromBuffer(input.AsBuffer(), BitmapPixelFormat.Nv12, width, height))
            {
                return SoftwareBitmap.Convert(yuv, BitmapPixelFormat.Bgra8);
            }

        }
    }
}
