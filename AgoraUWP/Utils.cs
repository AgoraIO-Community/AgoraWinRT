using AgoraWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Capture.Frames;

namespace AgoraUWP
{
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }

    public static class Utils
    {
        public static unsafe SoftwareBitmap ConvertToImage(VideoMediaFrame input)
        {
            using (var inputBitmap = input?.SoftwareBitmap)
            {
                if (inputBitmap == null) return null;
                return SoftwareBitmap.Convert(inputBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore);
            }
        }

        public static byte[] ConvertToNv12(VideoFrame frame)
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

        public static unsafe SoftwareBitmap ConvertToImage(byte[] input, int width, int height)
        {
            using (var yuv = SoftwareBitmap.CreateCopyFromBuffer(input.AsBuffer(), BitmapPixelFormat.Nv12, width, height))
            {
                return SoftwareBitmap.Convert(yuv, BitmapPixelFormat.Bgra8);
            }

        }

    }

}
