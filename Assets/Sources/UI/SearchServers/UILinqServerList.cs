using UnityEngine;
using UnityEngine.UI;
using Client.Contracts;
using System.Collections.Generic;

#pragma warning disable IDE0090

namespace Client.UI.SearchServers
{
    public sealed class UILinqServerList : MonoBehaviour
    {
        public Button UpdateServerList;
        public Button CreateServer;
        public Button CreateServerEnd;
        public Button EnterServer;

        public InputField ServerName;
        public InputField ServerDescription;

        public Transform ContainerServers;
        public GameObject ServerElement;
        public GameObject WindowCreateServer;

        public List<KeyValuePair<int, ServerRoom>> ServerRooms = new List<KeyValuePair<int, ServerRoom>>(0);
        public List<KeyValuePair<int, ServerInfo>> ServersInfo = new List<KeyValuePair<int, ServerInfo>>(0);
    }
}