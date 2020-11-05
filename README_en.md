# AgoraWinRT Development Manual

In UWP, Agora C++ Native Development kit cannot get the camera and microphone permission because they should be explicitly applying,  and because window has not the handle property in UWP, Agora C++ Native also cannot render video data by default function like setupLocalVideo and setupRemoteVideo. On the other hand, UWP development is more friendly to C# users. 

Based on those reasons, this project encapsulates native c++ kit to WinRT kit, and based of WinRT kit, it encapsulates UWP kit to manage video and audio devices. If you are c++ user, you can also directly use WinRT kit for development.

## Architecture description

The whole architecture is divided into two main parts: AgoraWinRT and AgoraUWP.

AgoraWinRT encapsulated for Agora C++ Native.

AgoraUWP is based on AgoraWinRT , and add device managemet and video render on UWP.

### AgoraWinRT description

When AgoraWinRT encapsulated  the Agora C++ API, it use same function names and type names with Agora C++ expect for the namespace. Users can directly refer to [Agora C++ API Reference for All Platforms](https://docs.agora.io/en/Audio%20Broadcast/API%20Reference/cpp/index.html) to find the corresponding type and function description.

#### AgoraWinRT dose not encapsulated parts

In AgoraWinRT, some Agora C++ API are not encapsulated, the following list is those function's name and reason.

| Name                                | Reason                                                       |
| ----------------------------------- | ------------------------------------------------------------ |
| Screen Capture                      | not implemented temporarily                                  |
| Face Detection                      | Android and iOS                                              |
| Audio Routing Control               | Android and iOS                                              |
| In-ear Monitoring                   | Android and iOS                                              |
| Camera Control                      | Android and iOS                                              |
| setAudioSessionOperationRestriction | iOS                                                          |
| queryInterface                      | useless in WinRT                                             |
| IVideoDeviceManager                 | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| initialize                          | integrate into constructor                                   |
| release                             | integrate into dispose                                       |
| setupLocalVideo                     | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| setupRemoteVideo                    | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| setLocalRenderMode                  | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| setRemoteRenderMode                 | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| startPreview                        | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| stopPreview                         | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |
| enableLocalVideo                    | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description)#AgoraUWP说明) |
| onVideoDeviceStateChanged           | implemented in specific platform such as UWP platform. For more details, please refer to [AgoraUWP Description](#AgoraUWP description) |

#### AgoraWinRT project files description

AgoraWinRT project is a Windows Runtime Component (C++ WinRT) project, and it's interfaces are defined in serval idl files.

| File Name                        | Description                                                  |
| -------------------------------- | ------------------------------------------------------------ |
| AgoraWinRT.idl                   | the main interface file, which is a WinRT redefinition version of  the interface of AgoraRtc in Agora C++ |
| AgoraWinRTInterfaces.idl         | the event interfaces definition file, it includes  AgoraRtcEventHandler、MetadataObserver、AudioFrameObserver、VideoFrameObserver, PacketObserver. User can implement interface and call registration method to subscript events. |
| AgoraWinRTAudioDeviceManager.idl | include redefinition version of Agora C++ IAudioDeviceManager |
| AgoraWinRTChannel.idl            | include redefinition version of Agora C++ IChannel and IChannelEventHandler |
| AgoraWinRTTypes.idl              | include types redefiniton version of Agora C++ SDK           |
| AgoraWinRTEnums.idl              | include enums redefinition version of  Agora C++ SDK         |

#### AgoraWinRT::AgoraRtc event registration method description

AgoraWinRT::AgoraRtc is the core class of AgoraWinRT, which encapsulate AgoraRtc in Agora C++, and provides event registration methods that user can  those methods to subscript events.

| Event Registration Method     | Event Interface                                              |
| ----------------------------- | ------------------------------------------------------------ |
| RegisterRtcEngineEventHandler | AgoraRtcEventHandler                                         |
| RegisterMediaMetadataObserver | MetadataObserver                                             |
| RegisterPacketObserver        | PacketObserver，based on the internal implementation of Agora, only registration will not make this interface work. User must be call enableEncryption to make enable is true,  then PacketObserver's event can take effect. |
| RegisterAudioFrameObserver    | AudioFrameObserver                                           |
| RegisterVideoFrameObserver    | VideoFrameObserver                                           |

#### Special points of AgoraWinRT::AgoraRtc

AgoraWinRT::AgoraRtc removes initialize and release functions. Instead of that, it automatically initialize the RTC engine when AgoraWinRT::AgoraRtc class is created in constructor and automatically call the engine's release function when AgoraWinRT::AgoraRtc instance be destructed.

At the same time, AgoraWinRT::AgoraRtc implements the IDispose interface, so users can use Using or directly call the dispose method to release resources. 

**NOTE: user can not use AgoraWinRT::AgoraRtc instance again after calling Dispose method, instead of that , user must recreate a new instance of AgoraWinRT::AgoraRtc.**

#### AgoraWinRT::AgoraRtc's functions and event interfaces document

[As mentioned above](#AgoarWinRT description), the name of all funcitons, types, enums and interface definitions in AgoraWinRT::AgoraRtc are consistent with those in Agora C++ Naive SDK. Developers can use [the official documents](https://docs.agora.io/en/Audio%20Broadcast/API%20Reference/cpp/index.html) to find specific information of functions, types, enums and interfaces.

