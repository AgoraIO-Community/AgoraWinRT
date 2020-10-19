# AgoraWinRT开发说明手册

因为UWP的特殊性，原生Agora C++开发包无法在没有明确申请权限的基础上就得到摄像头和麦克风的使用权限，并且因为窗口句柄的取消，也无法缺省的实现对视频的渲染，另一方面，UWP开发明显对于C#的用户更加友好，特此对Agora C++进行WinRT封装，并在WinRT封装的基础上针对UWP的设备管理和音视频渲染再度封装，从而方便C#用户在UWP平台上进行Agora应用的开发，C++用户也可以参考或直接使用WinRT封装进行开发。

## 架构说明

整个架构分为两个主要部分，AgoraWinRT和AgoraUWP。

AgoraWinRT是对Agora C++的封装，而AgoraUWP则是对AgoraWinRT的封装。

### AgoraWinRT说明

AgoraWinRT针对Agora C++的API进行封装，封装时除了名字空间在AgoraWinRT名字空间下之外，其中的函数名称、类型名称都与Agora C++一致，使用者可直接参考[Agora C++ API Reference for All Platforms](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)中对应的类型、函数说明进行查找即可。

#### AgoraWinRT没有封装的部分

在AgoraWinRT中，因为各种原因有个别API没有进行封装，以下是列表及原因说明

| API名称                             | 原因说明                                                     |
| ----------------------------------- | ------------------------------------------------------------ |
| setVideoProfile                     | 2.3后废弃                                                    |
| 屏幕共享                            | 技术原因，暂时不封装                                         |
| 人脸检测                            | Android和iOS                                                 |
| 音频播放路由                        | Android和iOS                                                 |
| 耳返控制                            | Android和iOS                                                 |
| 摄像头控制                          | Android和iOS                                                 |
| setAudioSessionOperationRestriction | iOS                                                          |
| queryInterface                      | COM架构                                                      |
| IVideoDeviceManager                 | 视频设备管理部分因具体平台差异，转移至具体平台的封装中实现，比如UWP平台上，详情参考[AgoraUWP说明](#AgoraUWP说明) |
| initialize                          | 集成至类创建中                                               |
| release                             | 集成至dispose方法中                                          |

#### AgoraWinRT项目文件说明

AgoraWinRT项目为Windows Runtime Component（C++ WinRT）项目，其接口定义在几个idl文件中。

| 文件名                           | 用途说明                                                     |
| -------------------------------- | ------------------------------------------------------------ |
| AgoraWinRT.idl                   | 主接口文件，定义了AgoraRtc的接口，为Agora C++ SDK的WinRT重定义版本 |
| AgoraWinRTInterfaces.idl         | 事件接口定义文件，重定义了AgoraRtcEventHandler、MetadataObserver、AudioFrameObserver、VideoFrameObserver, PacketObserver，使用者在自己的类中可选择接口进行实现，并调用相应的注册方法，实现事件订阅 |
| AgoraWinRTAudioDeviceManager.idl | Agora C++ IAudioDeviceManager的WinRT重定义文件               |
| AgoraWinRTChannel.idl            | Agora C++ IChannel和IChannelEventHandler的WinRT重定义文件    |
| AgoraWinRTTypes.idl              | Agora C++ SDK中各种其他类型的WinRT重定义文件                 |
| AgoraWinRTEnums.idl              | Agora C++ SDK中各种枚举类型的WinRT重定义文件                 |

#### AgoraRtc的事件注册

AgoraRtc为AgoraWinRT的核心封装类，其中集中了对Agora C++ SDK的封装，并且通过下表提供Agora C++ RTC部分的事件注册支持

| 注册方法                      | 注册事件                                                     |
| ----------------------------- | ------------------------------------------------------------ |
| RegisterRtcEngineEventHandler | AgoraRtcEventHandler                                         |
| RegisterMediaMetadataObserver | MetadataObserver                                             |
| RegisterPacketObserver        | PacketObserver，基于Agora的内部机制，光注册并不会让这个接口生效，需要在调用EnableEncryption并且enable为true后，本接口中的函数事件才会调用 |
| RegisterAudioFrameObserver    | AudioFrameObserver                                           |
| RegisterVideoFrameObserver    | VideoFrameObserver                                           |

#### AgoraRtc的特殊实现点

AgoraRtc对比Agora C++实现，去除了initialize和release两个接口。因为C++ WinRT本身可以利用C++面对对象的特性，实现在AgoraRtc类创建时自动初始RTC引擎，并且在类析构时自动调用引擎的release方法。同时为了方便.NET平台的资源管理，AgoraRtc实现了IDispose接口，从而用户可以使用using或直接调用dispose方法进行资源的释放。**注意，因为这样子的实现，用户在调用dispose释放AgoraRtc之后，想要再次利用AgoraRtc，必须重新创建新的AgoraRtc类，并注册事件，而不是像Agora C++ SDK中那样子可以持有引用，并重新initialize。**

#### AgoraRtc的函数和事件接口文档

[如前所述](#AgoraWinRT说明)AgoraRtc中，除了名字空间为AgoraWinRT外，其下所有的函数、类型、枚举类型、接口定义的名称都与Agora C++ SDK中的一致。其意图就是方便开发者直接通过官方的[Agora C++ API Reference for All Platforms](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)文档查找具体的函数、类型、枚举类型、接口定义的具体说明。所以本处并不提供函数、类型、枚举类型、接口定义的说明文档，开发者可直接参考[官方文档](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)。

### AgoraUWP说明

AgoraUWP是针对UWP平台实现的对AgoraWinRT的C#封装，并在AgoraWinRT的基础上，提供了VideoDeviceManager、视频采集与渲染功能。同时，缺省的实现了AgoraRtcEventHandler、VideoFrameObserver、AudioFrameObserver三个接口，并将其中的函数转而实现为C#风格的事件。

其实现时的函数、类型、方法等名称原则与AgoraWinRT中一样，除了名字空间为AgoraUWP外，都与Agora C++ SDK中保持一致。

#### AgoraUWP项目文件说明

AgoraUWP项目为类库（通用Windows）项目类型，其中各个文件的说明为：

| 文件名                     | 用途说明                                                     |
| -------------------------- | ------------------------------------------------------------ |
| AgoraUWP.cs                | 本文件为AgoraUWP的主文件，其中包括了AgoraWinRT中AgoraRtc的同名UWP封装，封装中缺省实现AgoraRtcEventHandler、VideoFrameObserver、AudioFrameObserver三个AgoraWinRT接口，并将其转化为C#风格的对应事件。并在封装中提供了对UWP平台的视频采集与渲染的缺省实现，让使用者可以像使用Agora C++ SDK那样，不用考虑视频采集与渲染问题，直接调用SDK的功能即可完成正常的视频通讯与直播。 |
| AgoraUWPDelegate.cs        | AgoraRtcEventHandler、VideoFrameObserver、AudioFrameObserver三个接口对应的C#风格事件的定义文件 |
| IVideoDeviceCollection.cs  | Agora C++ SDK的IVideoDeviceCollection在C#上的重定义          |
| VideoDeviceManager.cs      | Agora C++ SDK的IVideoDevicemanger在C#上的重定义，并且提供IVideoDeviceCollection和IVideoDeviceManager在UWP平台上的C#实现 |
| VideoCanvas.cs             | 因为UWP平台上没有窗口的handle，传统的通过传入窗口handle，在窗口中进行视频渲染的做法无法实现。因此定义了VideoCanvas抽象类，统一UWP平台上视屏渲染的实现接口，具体说明参考[VideoCanvas](#VideoCanvas类说明) |
| ImageBrushVideoCanvas.cs   | 提供基于ImageBrush的VideoCanvas视频渲染实现，用户可以使用这个类在ImageBrush上实现视屏渲染，也可以参考这个类的实现，实现自己的视频渲染实现 |
| IMediaCapturer.cs          | AgoraUWP使用的媒体捕捉器的接口，用户可以通过实现自己的媒体捕捉器以替换AgoraUWP中缺省的媒体捕捉器 |
| GeneralMediaCapturer.cs    | IMediaCapturer接口的缺省实现，也是AgoraUWP缺省使用的媒体捕捉器，用户可以参考这个类的实现，实现自己的媒体捕捉器 |
| MediaCaptureVideoSource.cs | 基于GeneralMediaCapturer的一个VideoSource的实现              |



## 使用实例说明
