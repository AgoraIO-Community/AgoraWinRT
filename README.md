# AgoraWinRT����˵���ֲ�

��ΪUWP�������ԣ�ԭ��Agora C++�������޷���û����ȷ����Ȩ�޵Ļ����Ͼ͵õ�����ͷ����˷��ʹ��Ȩ�ޣ�������Ϊ���ھ����ȡ����Ҳ�޷�ȱʡ��ʵ�ֶ���Ƶ����Ⱦ����һ���棬UWP�������Զ���C#���û������Ѻã��ش˶�Agora C++����WinRT��װ������WinRT��װ�Ļ��������UWP���豸���������Ƶ��Ⱦ�ٶȷ�װ���Ӷ�����C#�û���UWPƽ̨�Ͻ���AgoraӦ�õĿ�����C++�û�Ҳ���Բο���ֱ��ʹ��WinRT��װ���п�����

## �ܹ�˵��

�����ܹ���Ϊ������Ҫ���֣�AgoraWinRT��AgoraUWP��

AgoraWinRT�Ƕ�Agora C++�ķ�װ����AgoraUWP���Ƕ�AgoraWinRT�ķ�װ��

### AgoraWinRT˵��

AgoraWinRT���Agora C++��API���з�װ����װʱ�������ֿռ���AgoraWinRT���ֿռ���֮�⣬���еĺ������ơ��������ƶ���Agora C++һ�£�ʹ���߿�ֱ�Ӳο�[Agora C++ API Reference for All Platforms](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)�ж�Ӧ�����͡�����˵�����в��Ҽ��ɡ�

#### AgoraWinRTû�з�װ�Ĳ���

��AgoraWinRT�У���Ϊ����ԭ���и���APIû�н��з�װ���������б�ԭ��˵��

| API����                             | ԭ��˵��                                                     |
| ----------------------------------- | ------------------------------------------------------------ |
| setVideoProfile                     | 2.3�����                                                    |
| ��Ļ����                            | ����ԭ����ʱ����װ                                         |
| �������                            | Android��iOS                                                 |
| ��Ƶ����·��                        | Android��iOS                                                 |
| ��������                            | Android��iOS                                                 |
| ����ͷ����                          | Android��iOS                                                 |
| setAudioSessionOperationRestriction | iOS                                                          |
| queryInterface                      | COM�ܹ�                                                      |
| IVideoDeviceManager                 | ��Ƶ�豸�����������ƽ̨���죬ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| initialize                          | �������ഴ����                                               |
| release                             | ������dispose������                                          |
| setupLocalVideo                     | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| setupRemoteVideo                    | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| setLocalRenderMode                  | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| setRemoteRenderMode                 | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| startPreview                        | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| stopPreview                         | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| enableLocalVideo                    | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |
| onVideoDeviceStateChanged           | ת��������ƽ̨�ķ�װ��ʵ�֣�����UWPƽ̨�ϣ�����ο�[AgoraUWP˵��](#AgoraUWP˵��) |

#### AgoraWinRT��Ŀ�ļ�˵��

AgoraWinRT��ĿΪWindows Runtime Component��C++ WinRT����Ŀ����ӿڶ����ڼ���idl�ļ��С�

| �ļ���                           | ��;˵��                                                     |
| -------------------------------- | ------------------------------------------------------------ |
| AgoraWinRT.idl                   | ���ӿ��ļ���������AgoraRtc�Ľӿڣ�ΪAgora C++ SDK��WinRT�ض���汾 |
| AgoraWinRTInterfaces.idl         | �¼��ӿڶ����ļ����ض�����AgoraRtcEventHandler��MetadataObserver��AudioFrameObserver��VideoFrameObserver, PacketObserver��ʹ�������Լ������п�ѡ��ӿڽ���ʵ�֣���������Ӧ��ע�᷽����ʵ���¼����� |
| AgoraWinRTAudioDeviceManager.idl | Agora C++ IAudioDeviceManager��WinRT�ض����ļ�               |
| AgoraWinRTChannel.idl            | Agora C++ IChannel��IChannelEventHandler��WinRT�ض����ļ�    |
| AgoraWinRTTypes.idl              | Agora C++ SDK�и����������͵�WinRT�ض����ļ�                 |
| AgoraWinRTEnums.idl              | Agora C++ SDK�и���ö�����͵�WinRT�ض����ļ�                 |

#### AgoraWinRT::AgoraRtc���¼�ע��

AgoraRtcΪAgoraWinRT�ĺ��ķ�װ�࣬���м����˶�Agora C++ SDK�ķ�װ������ͨ���±��ṩAgora C++ RTC���ֵ��¼�ע��֧��

| ע�᷽��                      | ע���¼�                                                     |
| ----------------------------- | ------------------------------------------------------------ |
| RegisterRtcEngineEventHandler | AgoraRtcEventHandler                                         |
| RegisterMediaMetadataObserver | MetadataObserver                                             |
| RegisterPacketObserver        | PacketObserver������Agora���ڲ����ƣ���ע�Ტ����������ӿ���Ч����Ҫ�ڵ���EnableEncryption����enableΪtrue�󣬱��ӿ��еĺ����¼��Ż���� |
| RegisterAudioFrameObserver    | AudioFrameObserver                                           |
| RegisterVideoFrameObserver    | VideoFrameObserver                                           |

#### AgoraWinRT::AgoraRtc������ʵ�ֵ�

AgoraRtc�Ա�Agora C++ʵ�֣�ȥ����initialize��release�����ӿڡ���ΪC++ WinRT�����������C++��Զ�������ԣ�ʵ����AgoraRtc�ഴ��ʱ�Զ���ʼRTC���棬������������ʱ�Զ����������release������ͬʱΪ�˷���.NETƽ̨����Դ����AgoraRtcʵ����IDispose�ӿڣ��Ӷ��û�����ʹ��using��ֱ�ӵ���dispose����������Դ���ͷš�**ע�⣬��Ϊ����ʵ�֣��û��ڵ���dispose�ͷ�AgoraRtc֮����Ҫ�ٴ�����AgoraRtc���������´����µ�AgoraRtc�࣬��ע���¼�����������Agora C++ SDK�������ӿ��Գ������ã�������initialize��**

#### AgoraWinRT::AgoraRtc�ĺ������¼��ӿ��ĵ�

[��ǰ����](#AgoraWinRT˵��)AgoraRtc�У��������ֿռ�ΪAgoraWinRT�⣬�������еĺ��������͡�ö�����͡��ӿڶ�������ƶ���Agora C++ SDK�е�һ�¡�����ͼ���Ƿ��㿪����ֱ��ͨ���ٷ���[Agora C++ API Reference for All Platforms](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)�ĵ����Ҿ���ĺ��������͡�ö�����͡��ӿڶ���ľ���˵�������Ա��������ṩ���������͡�ö�����͡��ӿڶ����˵���ĵ��������߿�ֱ�Ӳο�[�ٷ��ĵ�](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)��

### AgoraUWP˵��

AgoraUWP�����UWPƽ̨ʵ�ֵĶ�AgoraWinRT��C#��װ������AgoraWinRT�Ļ����ϣ��ṩ��VideoDeviceManager����Ƶ�ɼ�����Ⱦ���ܡ�ͬʱ��ȱʡ��ʵ����AgoraRtcEventHandler��VideoFrameObserver��AudioFrameObserver�����ӿڣ��������еĺ���ת��ʵ��ΪC#�����¼���

��ʵ��ʱ�ĺ��������͡�����������ԭ����AgoraWinRT��һ�����������ֿռ�ΪAgoraUWP�⣬����Agora C++ SDK�б���һ�¡�

#### AgoraUWP��Ŀ�ļ�˵��

AgoraUWP��ĿΪ��⣨ͨ��Windows����Ŀ���ͣ����и����ļ���˵��Ϊ��

| �ļ���                     | ��;˵��                                                     |
| -------------------------- | ------------------------------------------------------------ |
| AgoraUWP.cs                | ���ļ�ΪAgoraUWP�����ļ������а�����AgoraWinRT��AgoraRtc��ͬ��UWP��װ����װ��ȱʡʵ��AgoraRtcEventHandler��VideoFrameObserver��AudioFrameObserver����AgoraWinRT�ӿڣ�������ת��ΪC#���Ķ�Ӧ�¼������ڷ�װ���ṩ�˶�UWPƽ̨����Ƶ�ɼ�����Ⱦ��ȱʡʵ�֣���ʹ���߿�����ʹ��Agora C++ SDK���������ÿ�����Ƶ�ɼ�����Ⱦ���⣬ֱ�ӵ���SDK�Ĺ��ܼ��������������ƵͨѶ��ֱ���� |
| AgoraUWPDelegate.cs        | AgoraRtcEventHandler��VideoFrameObserver��AudioFrameObserver�����ӿڶ�Ӧ��C#����¼��Ķ����ļ� |
| IVideoDeviceCollection.cs  | Agora C++ SDK��IVideoDeviceCollection��C#�ϵ��ض���          |
| VideoDeviceManager.cs      | Agora C++ SDK��IVideoDevicemanger��C#�ϵ��ض��壬�����ṩIVideoDeviceCollection��IVideoDeviceManager��UWPƽ̨�ϵ�C#ʵ�� |
| VideoCanvas.cs             | ��ΪUWPƽ̨��û�д��ڵ�handle����ͳ��ͨ�����봰��handle���ڴ����н�����Ƶ��Ⱦ�������޷�ʵ�֡���˶�����VideoCanvas�����࣬ͳһUWPƽ̨��������Ⱦ��ʵ�ֽӿڣ�����˵���ο�[VideoCanvas](#VideoCanvas��˵��) |
| ImageBrushVideoCanvas.cs   | �ṩ����ImageBrush��VideoCanvas��Ƶ��Ⱦʵ�֣��û�����ʹ���������ImageBrush��ʵ��������Ⱦ��Ҳ���Բο�������ʵ�֣�ʵ���Լ�����Ƶ��Ⱦʵ�� |
| IMediaCapturer.cs          | AgoraUWPʹ�õ�ý�岶׽���Ľӿڣ��û�����ͨ��ʵ���Լ���ý�岶׽�����滻AgoraUWP��ȱʡ��ý�岶׽�� |
| GeneralMediaCapturer.cs    | IMediaCapturer�ӿڵ�ȱʡʵ�֣�Ҳ��AgoraUWPȱʡʹ�õ�ý�岶׽�����û����Բο�������ʵ�֣�ʵ���Լ���ý�岶׽�� |
| MediaCaptureVideoSource.cs | ����GeneralMediaCapturer��һ��Agora VideoSource��ʵ��        |

#### AgoraUWP.AgoraRtc

������Agora RTC��UWPƽ̨�ϵĺ��ķ�װ�࣬��̳���AgoraWinRT::AgoraRtc�࣬��ʵ����AgoraRtcEventHandler��VideoFrameObserver��AudioFrameObserver����AgoraWinRT�ӿڣ�ͬʱ����ת��ΪC#���Ķ�Ӧ�¼�����������AgoraWinRT��û��ʵ�ֵ����UWPƽ̨����Ƶ��׽����Ⱦ����Ƶ�豸�����ܣ�ʵ����setupLocalVideo��setupRemoteVideo��setLocalRenderMode��setRemoteRenderMode��startPreview��stopPreview��enableLocalVideo���������幦����ο�[Agora C++ API Reference for All Platforms](https://docs.agora.io/cn/Audio%20Broadcast/API%20Reference/cpp/index.html)��ͬ������˵����

��ʹ��AgoraUWP.AgoraRtc��ʱ��ʹ��Agora C++ԭ��ʵ��ʱ�м�����ͬ�㣺

1. ��ΪUWPƽ̨��Ȩ�޹���ԭ��ʹ������ͷ����˷���Ҫ����Ȩ�ޡ��������ԣ�����Ȩ�޲�����Ϊ�е���ȷ�ϵ����̣��޷�ʹ��ͬ�����룬�ر����RequestCameraAccess�첽��̬������ʹ������ʹ��֮ǰ��Ҫ�첽���ñ�����������Ȩ�ޡ�
2. ��ΪUWPƽ̨���޴���handle��setupLocalVideo��setupRemoteVideo����������Ϊ����VideoCanvas�������κμ̳в�ʵ����VideoCanvas���඼������ȷ�õ���Ҫ��Ⱦ����Ƶ֡���ݣ��û���Ҫ�Լ�ʵ������UWP�����ϵ���Ƶ��Ⱦ������ʹ��AgoraUWP.ImageBrushVideoCanvas�Ӷ�ֱ����ImageBrush��ʵ����Ƶ��Ⱦ��

#### AgoraUWP.IMediaCapturer��AgoraUWP.GeneralMediaCapturer

IMediaCapturer������һ����UWP�ϵ�����Ƶ֡��׽������GeneralMediaCapturer��������ȱʡʵ���ˡ�

GeneralMediaCapturer��������AgoraUWP.AgoraRtc��Ϊ�ڲ���Ƶ֡��׽��ʹ�ã�ͬʱҲ������Ϊ����Ƶ�Բɼ�ʱ������Ƶ֡��׽������ʹ�á�

IMediaCapturer����˵�����£�

1. VideoDevice

   �õ���ǰ��׽����ʹ�õ���Ƶ�豸������Ϊnull

2. AudioDevice

   �õ���ǰ��׽����ʹ�õ���Ƶ�豸������Ϊnull

3. AudioFormat

   ��ǰʹ�õ���Ƶ��ʽ���ݣ�������ʡ��������ȣ�����ɲο�msdn�й���[MediaFrameFormat](https://docs.microsoft.com/en-us/uwp/api/Windows.Media.Capture.Frames.MediaFrameFormat?redirectedfrom=MSDN&view=winrt-19041)����

4. VideoFormat

   ��ǰʹ�õ���Ƶ��ʽ����

5. VideoBufferType

   ��ǰ��������Ƶ����buffer�ĸ�ʽ��ȱʡΪ VIDEO_BUFFER_RAW_DATA

6. OnVideoFrameArrived

   ���ɼ�����Ƶ֡����ʱ����

7. EnableVideo

   �Ƿ�����Ƶ֡�ɼ���ȱʡΪtrue���������Ϊfalse����OnVideoFrameArrived���ᴥ��

8. OnAudioFrameArrived 

   ���ɼ�����Ƶ֡����ʱ����

9. EnableAudio

   �Ƿ����ɼ���Ƶ֡��ȱʡΪtrue���������Ϊfalse����OnAudioFrameArrived���ᴥ��

#### VideoCanvas��ImageBrushVideoCanvas

VideoCanvas�����˿ɱ�AgoraUWP.AgoraRtc����Ϊ����SetupLocalVideo��Զ��SetupRemoteVideo��Ƶ��Ⱦ��������ʵ�ֵķ��������ԡ�

ImageBrushVideoCanvas���ǻ���ImageBrush֮�ϵ�VideoCanvasʵ���ࡣ

VideoCanvas����˵�����£�

1. Target

   ��Ҫ�����Ͻ�����Ⱦ�Ķ��󣬱���ImageBrushVideoCanvas�е�ImageBrush����

2. RenderMode

   ��Ƶ����Ƶ��Ⱦ���ڵ���䷽ʽ

3. Channel

   ��Ƶ��Ӧ��Դ��Ƶ��

4. User

   ��Ƶ��Ӧ���û�id

5. MirrorMode

   ��Ƶ�Ƿ���Ҫ������ʾ

6. Render(MediaFrameReference)

   ���ص���ƵԤ��ʱ��ʹ�õ���Ⱦ����

7. Render(VideoFrame)

   AgoraUWP.AgoraRtc���б��غ�Զ����Ƶ��Ⱦʱ���õķ���

VideoCanvas��Ϊ�����࣬��������Render��ʵ��Ϊ�鷽���������û�������Ҫ����������������ѡ��ĵ���ʵ�֣�����VideoDeviceManager��StartDeviceTest�е�VideoCanvas�Ϳ���ֻʵ��Render(MediaFrameReference)����Ϊ���������VideoCanvasֻ�ڱ�����Ƶ������ʹ�á�

#### MediaCaptureVideoSource

Agora C++���ṩ��һ��setVideoSource�ķ��������û������ṩ�Լ���VideoSource��������Agora��ʾ���У��Զ������Ƶ�ɼ�������Ƶ�Բɼ�setExternalVideoSource��pushVideoFrame�����������еġ�����ֻ���ṩ��һ������GeneralMeidaCapturer�����Զ����VideoSource�����ӣ��û����Բο���ʵ���Լ���VideoSource��

## ʹ��ʵ��˵��

AgoraUWPDemoΪ����AgoraUWP��ʾ����

��������ʾ������ģʽ��

1. ʹ��SDK�ڲ��ɼ�ģʽ

   ֱ��ʹ��SDK�ڲ�������Ƶ�ɼ����û����ù��Ĳɼ����֣�ֻ�ù���ҵ�񲿷֡�

2. ʹ����Ƶ�Բɼ�

   ��ʾ����ν���ʹ��GeneralMediaCapturer������Ƶ�Բɼ�����Ϊʾ����ʹ���߿ɲο���ʵ���Լ�����Ƶ�Բɼ�������

3. ʹ����Ƶ����Ⱦ

   ��ʾ�����ʹ��[AudioGraph](https://docs.microsoft.com/en-us/uwp/api/windows.media.audio.audiograph?view=winrt-19041)���[PullAudioFrame](https://docs.agora.io/cn/Video/API%20Reference/cpp/classagora_1_1media_1_1_i_media_engine.html#aaf43fc265eb4707bb59f1bf0cbe01940)���ⲿ��Ƶ������Ⱦ��

ͬʱʾ���л���ʾ����ε����ֱ��ʡ�֡�ʡ����ʵȡ�

### ʹ��SDK�ڲ��ɼ�ģʽ����

�����̶�Ӧ��[��ο��ٿ�ʼ](https://docs.agora.io/cn/Video/start_call_windows?platform=Windows)һ��

```mermaid
sequenceDiagram
participant UWP as UWP App
participant SDK as Agora UWP SDK
participant Agora as SD-RTN
Note left of UWP: ��������ͷ����˷�Ȩ��
UWP->>SDK: AgoraUWP.AgoraRtc.RequestCameraAccess
Note left of UWP: ��ʼ��
UWP->>SDK: new AgoraUWP.AgoraRtc
Note left of UWP: ���ñ�����ͼ
UWP->>SDK: SetupLocalVideo(new ImageBrushVideoCanvas)
UWP->>SDK: EnableVideo
UWP->>SDK: StartPreview ��ѡ
Note left Of UWP: ����Ƶ��
UWP->>SDK: JoinChannel
SDK->>Agora: �������Ƶ��
SDK-->>UWP: OnJoinChannelSuccess
Note left of UWP: ����Զ����ͼ
Note right of Agora: Զ���û�����
Agora-->>UWP: OnFirstRemoteVideoDecoded ���� OnUserJoined
UWP->>SDK: SetupRemoteVideo(new ImageBrushVideoCanvas)
Note left of UWP: �뿪Ƶ��
UWP->>SDK: LeaveChannel
SDK->>Agora: �����뿪Ƶ��
Note left of UWP: �ͷ�
UWP->>SDK: Dispose
```

![���ٿ�ʼ����](images/image-20201020161921981.png)

��ע�⣬��ΪUWPƽ̨���ƣ����������ͷ����˷�Ȩ�޽�������ſ���ʹ�ã������ڳ�ʼ��֮ǰ���û���Ҫ�ȵ���AgoraUWP.AgoraRtc.RequestCameraAccess�첽��������Ȩ�����룬��Ϊ���첽����������Ƽ��ڳ���һ��ʼ���������ʾʱ���У�����������ʼ��AgoraRtc���������������ʼ��Ҫ������Ƶͨ����ʱ���ٽ��У���������AgoraRtc�޷�����豸Ȩ�ޡ�

### ʹ����Ƶ�Բɼ�

�����̶�Ӧ[�Զ�����Ƶ�ɼ�����Ⱦ](https://docs.agora.io/cn/Video/custom_audio_windows?platform=Windows)һ���е�ǰ�벿���Զ�����Ƶ�ɼ�

```mermaid
sequenceDiagram
participant UWP as UWP App
participant SDK as Agora UWP SDK
participant Agora as SD-RTN
Note left of UWP: ��������ͷ����˷�Ȩ��
UWP->>SDK: AgoraUWP.AgoraRtc.RequestCameraAccess
Note left of UWP: ��ʼ��
UWP->>SDK: new AgoraUWP.AgoraRtc
Note left of UWP: ����ʹ���ⲿ��ƵԴ
UWP->>SDK: SetExternalAudioSource
Note left of UWP: ����Ƶ��
UWP->>SDK: JoinChannel
SDK->>Agora: �������Ƶ��
SDK-->>UWP: OnJoinChannelSuccess
Note left of UWP: ����������Ƶ���ݷ��͸�SDK
UWP->>SDK: PushAudioFrame
Note left of UWP: �뿪Ƶ��
UWP->>SDK: LeaveChannel
SDK->>Agora: �����뿪Ƶ��
Note left of UWP: �ͷ�
UWP->>SDK: Dispose
```

![�Բɼ���Ƶ����](images/image-20201020162134496.png)

����ʾ��

```c#
/// <summary>
/// ��ʾ��ν�����Ƶ�Բɼ�������Ⱦ
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void StartEngineAndSelfAudioProcess()
{
    InitEngine();
    InitAudioCapture();
    this.engine.SetExternalAudioSource(true, m_audioCapture.AudioFormat.AudioEncodingProperties.SampleRate, (byte)m_audioCapture.AudioFormat.AudioEncodingProperties.ChannelCount);
}
```

���˳�ʼ�������⣬����ʼ����һ������GeneralMediaCapturerʵ�ֵ���Ƶ�ɼ���

```c#
private void InitAudioCapture()
{
    var sourceGroup = MediaFrameSourceGroup.FindAllAsync().AsTask().GetAwaiter().GetResult();
    if (sourceGroup.Count == 0) return;
    m_audioCapture = new GeneralMediaCapturer(sourceGroup[0], StreamingCaptureMode.Audio);
    m_audioCapture.OnAudioFrameArrived += AudioFrameArrivedEvent;
}
```

�������Ƶ�ɼ����У����ɼ�����Ƶ����ʱ��AudioFrameArrivedEvent�����������ɼ�����PCM32����Ƶ����ת��ΪAgora�ܴ����PCM16����Ƶ���ݺ�ͨ��PushAudioFrame���з���

```c#
private void AudioFrameArrivedEvent(AudioMediaFrame frame)
{
    using (Windows.Media.AudioFrame rawAudioFrame = frame.GetAudioFrame())
        using (AudioBuffer audioBuffer = rawAudioFrame.LockBuffer(AudioBufferAccessMode.Read))
        using (AgoraWinRT.AudioFrame audioFrame = new AgoraWinRT.AudioFrame())
    {
        var buffer = Windows.Storage.Streams.Buffer.CreateCopyFromMemoryBuffer(audioBuffer);
        buffer.Length = audioBuffer.Length;
        using (var dataReader = DataReader.FromBuffer(buffer))
        {
            dataReader.ByteOrder = ByteOrder.LittleEndian;
            byte[] tmp = new byte[buffer.Length / 2];
            int pos = 0;
            while (dataReader.UnconsumedBufferLength > 0)
            {
                var int16Data = (Int16)(dataReader.ReadSingle() * Int16.MaxValue);
                byte[] chunck = BitConverter.GetBytes(int16Data);
                tmp[pos++] = chunck[0];
                tmp[pos++] = chunck[1];
            }
            var format = frame.AudioEncodingProperties;
            audioFrame.bytesPerSample = 2;
            audioFrame.channels = (byte)format.ChannelCount;
            audioFrame.samplesPerSec = format.SampleRate;
            audioFrame.samples = (uint)(tmp.Length / 4);
            audioFrame.type = AUDIO_FRAME_TYPE.FRAME_TYPE_PCM16;
            audioFrame.buffer = tmp;
            engine.PushAudioFrame(audioFrame);
        }
    }
}
```

��ʾ���͹ٷ���ʾ�����в��ģ��ٷ�ʹ����һ�����У�ȷ���������ݲ������ʱ���������ʧ����ʾ��û�н����ⷽ��Ĵ���ʹ���߿��Ը����Լ�Ӧ�õ���Ҫ�ο�[�ٷ�ʾ��](https://docs.agora.io/cn/Video/custom_audio_windows?platform=Windows)ʵ�֡�

### ʹ����Ƶ����Ⱦ

�����̶�Ӧ[�Զ�����Ƶ�ɼ�����Ⱦ](https://docs.agora.io/cn/Video/custom_audio_windows?platform=Windows)һ���еĺ�벿�֣��Զ�����Ƶ��Ⱦ

```mermaid
sequenceDiagram
participant UWP as UWP App
participant SDK as Agora UWP SDK
participant Agora as SD-RTN
Note left of UWP: ��������ͷ����˷�Ȩ��
UWP->>SDK: AgoraUWP.AgoraRtc.RequestCameraAccess
Note left of UWP: ��ʼ��
UWP->>SDK: new AgoraUWP.AgoraRtc
Note left of UWP: ����ʹ���ⲿ��Ƶ��Ⱦ
UWP->>SDK: SetExternalAudioSink
Note left of UWP: ����Ƶ��
UWP->>SDK: JoinChannel
SDK->>Agora: �������Ƶ��
SDK-->>UWP: OnJoinChannelSuccess
Note left of UWP: ��ȡԶ����Ƶ���ݣ����д������в���
UWP->>SDK: PullAudioFrame
Note left of UWP: �뿪Ƶ��
UWP->>SDK: LeaveChannel
SDK->>Agora: �����뿪Ƶ��
Note left of UWP: �ͷ�
UWP->>SDK: Dispose
```
![��Ƶ����Ⱦ����](images/image-20201020162339492.png)

����ʾ��

```c#
private void StartEngineAndPullAudioProcess()
{
    InitEngine();
    InitAudioGraph();
    log("Set External Audio Sink", this.engine.SetExternalAudioSink(true, DEFAULT_SAMPLE_RATE, (byte)DEFAULT_CHANNEL_COUNT));
}
```

���˳�ʼ��AgoraRtc�����⣬����ʼ����һ��[AudioGraph](https://docs.microsoft.com/en-us/uwp/api/windows.media.audio.audiograph?view=winrt-19041)��Ϊ��Ƶ��������

```c#
private void InitAudioGraph()
{
    var settings = new AudioGraphSettings(AudioRenderCategory.Media);
    var createGraphResult = AudioGraph.CreateAsync(settings).AsTask().GetAwaiter().GetResult();
    m_audioGraph = createGraphResult.Graph;
    var outputResult = m_audioGraph.CreateDeviceOutputNodeAsync().AsTask().GetAwaiter().GetResult();

    m_audioInput = m_audioGraph.CreateFrameInputNode(
        new AudioEncodingProperties
        {
            BitsPerSample = DEFAULT_BITS_PER_SAMPLE,
            ChannelCount = DEFAULT_CHANNEL_COUNT,
            SampleRate = DEFAULT_SAMPLE_RATE,
            Subtype = MediaEncodingSubtypes.Pcm,
        });
    m_audioInput.QuantumStarted += QuantumStartedEvent;
    m_audioInput.AddOutgoingConnection(outputResult.DeviceOutputNode);
    m_audioInput.Stop();
}
```

����m_audioInput��ΪPCM���ݵ�����ˣ�������ȱʡ��ϵͳ��Ƶ����ˣ������������������ÿ��m_audioInput׼���ÿ��Դ����µ���Ƶ����ʱ���ᴥ��QuantumStarted�¼���������¼��оͿ��Ե���Agora��PullAudioFrame�õ���Ƶ���ݲ����д���󲥷š�

```c#
private void QuantumStartedEvent(AudioFrameInputNode sender, FrameInputNodeQuantumStartedEventArgs args)
{
    using (var frame = new AgoraWinRT.AudioFrame())
    {
        frame.bytesPerSample = 2;
        frame.channels = (byte)DEFAULT_CHANNEL_COUNT;
        frame.samplesPerSec = DEFAULT_SAMPLE_RATE;
        frame.type = AUDIO_FRAME_TYPE.FRAME_TYPE_PCM16;
        frame.renderTimeMs = 0;
        frame.samples = DEFAULT_SAMPLE_RATE / 100;
        var result = engine.PullAudioFrame(frame);
        if (result == 0) PlayAudioFrame(frame);
    }
}

 private unsafe void PlayAudioFrame(AgoraWinRT.AudioFrame frame)
 {
     using (var audioFrame = new Windows.Media.AudioFrame((uint)frame.buffer.Length))
     {
         using (var buffer = audioFrame.LockBuffer(AudioBufferAccessMode.Write))
             using (var reference = buffer.CreateReference())
         {
             byte* data;
             uint size;
             ((IMemoryBufferByteAccess)reference).GetBuffer(out data, out size);
             fixed (byte* raw = frame.buffer)
             {
                 System.Buffer.MemoryCopy(raw, data, size, size);
             }
         }
         m_audioInput?.AddFrame(audioFrame);
     }
 }
```

