using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgoraUWP;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AgoraUWPDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AgoraRtc engine;

        public MainPage()
        {
            this.InitializeComponent();

            this.Init();
        }

        private void Init()
        {
            localVideo.Source = new SoftwareBitmapSource();
            remoteVideo.Source = new SoftwareBitmapSource();
            txtResult.TextChanged += TxtResult_TextChanged;
            btnStart.Click += StartEngineAndPreview;
        }

        private void TxtResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            var grid = VisualTreeHelper.GetChild(txtResult, 0);
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
            {
                var child = VisualTreeHelper.GetChild(grid, i);
                if (child is ScrollViewer)
                {
                    var scroll = (ScrollViewer)child;
                    scroll.ChangeView(0, scroll.ExtentHeight, 1);
                    break;
                }
            }
        }

        private void StartEngineAndPreview(object sender, RoutedEventArgs e)
        {
            if (this.engine != null) this.engine.Dispose();
            this.engine = new AgoraRtc(txtVendorKey.Text);
            this.log("set channel profile", this.engine.SetChannelProfile(AgoraWinRT.CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING));
            this.log("set client role", this.engine.SetClientRole(AgoraWinRT.CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER));
            this.engine.SetupLocalVideo(new ImageVideoCanvas{ Target = localVideo, RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE, MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_ENABLED });
            //this.engine.SetupRemoteVideo(new ImageVideoCanvas { Target = remoteVideo, RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_ADAPTIVE, MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_DISABLED });
            this.engine.SetupRemoteVideo(new ImageBrushVideoCanvas { Target = remoteVideoBrush, RenderMode = AgoraWinRT.RENDER_MODE_TYPE.RENDER_MODE_FIT, MirrorMode = AgoraWinRT.VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_DISABLED });
            this.log("enable video", this.engine.EnableVideo());
            this.engine.StartPreview();
            log("join channel", this.engine.JoinChannel(txtChannelToken.Text, txtChannelName.Text, "", 0));

        }

        private void log(String operation, int result)
        {
            _ = txtResult.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    txtResult.Text += String.Format("agora {0} result is {1}\n", operation, result);
                });
        }

    }
}
