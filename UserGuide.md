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

#### AgoraRtc���¼�ע��

AgoraRtcΪAgoraWinRT�ĺ��ķ�װ�࣬���м����˶�Agora C++ SDK�ķ�װ������ͨ���±��ṩAgora C++ RTC���ֵ��¼�ע��֧��

| ע�᷽��                      | ע���¼�                                                     |
| ----------------------------- | ------------------------------------------------------------ |
| RegisterRtcEngineEventHandler | AgoraRtcEventHandler                                         |
| RegisterMediaMetadataObserver | MetadataObserver                                             |
| RegisterPacketObserver        | PacketObserver������Agora���ڲ����ƣ���ע�Ტ����������ӿ���Ч����Ҫ�ڵ���EnableEncryption����enableΪtrue�󣬱��ӿ��еĺ����¼��Ż���� |
| RegisterAudioFrameObserver    | AudioFrameObserver                                           |
| RegisterVideoFrameObserver    | VideoFrameObserver                                           |

#### AgoraRtc������ʵ�ֵ�

AgoraRtc�Ա�Agora C++ʵ�֣�ȥ����initialize��release�����ӿڡ���ΪC++ WinRT�����������C++��Զ�������ԣ�ʵ����AgoraRtc�ഴ��ʱ�Զ���ʼRTC���棬������������ʱ�Զ����������release������ͬʱΪ�˷���.NETƽ̨����Դ����AgoraRtcʵ����IDispose�ӿڣ��Ӷ��û�����ʹ��using��ֱ�ӵ���dispose����������Դ���ͷš�**ע�⣬��Ϊ�����ӵ�ʵ�֣��û��ڵ���dispose�ͷ�AgoraRtc֮����Ҫ�ٴ�����AgoraRtc���������´����µ�AgoraRtc�࣬��ע���¼�����������Agora C++ SDK�������ӿ��Գ������ã�������initialize��**

#### AgoraRtc�ĺ������¼��ӿ��ĵ�

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
| MediaCaptureVideoSource.cs | ����GeneralMediaCapturer��һ��VideoSource��ʵ��              |



## ʹ��ʵ��˵��
