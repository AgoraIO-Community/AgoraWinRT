using AgoraWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraUWP
{
    public class AgoraUWP: AgoraWinRT.AgoraRtcEventHandler
    {
        public void OnConnectionStateChanged(CONNECTION_STATE_TYPE type, CONNECTION_CHANGED_REASON_TYPE reason)
        {
            throw new NotImplementedException();
        }

        public void OnJoinChannelSuccess(string channel, ulong uid, uint elapsed)
        {
            throw new NotImplementedException();
        }

        public void OnRejoinChannelSuccess(string channel, ulong uid, uint elapsed)
        {
            throw new NotImplementedException();
        }

        public void OnLeaveChannel(RtcStats stats)
        {
            throw new NotImplementedException();
        }

        public void OnClientRoleChanged(CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole)
        {
            throw new NotImplementedException();
        }

        public void OnUserJoined(ulong uid, uint elapsed)
        {
            throw new NotImplementedException();
        }

        public void OnUserOffline(ulong uid, USER_OFFLINE_REASON_TYPE reason)
        {
            throw new NotImplementedException();
        }

        public void OnNetworkTypeChanged(NETWORK_TYPE type)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionLost()
        {
            throw new NotImplementedException();
        }

        public void OnTokenPrivilegeWillExpire(string token)
        {
            throw new NotImplementedException();
        }

        public void OnRequestToken()
        {
            throw new NotImplementedException();
        }
    }
}
