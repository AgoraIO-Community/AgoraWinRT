using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace AgoraUWP
{
    public class VideoCanvas
    {
        public ImageSource Source { get; set; } = null;
        public AgoraWinRT.RENDER_MODE_TYPE RenderMode { get; set; } = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE;
        public String Channel { get; set; } = null;
        public UInt64 User { get; set; } = 0;
        public AgoraWinRT.VIDEO_MIRROR_MODE_TYPE MirrorMode { get; set; } = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_DISABLED;

    }
}
