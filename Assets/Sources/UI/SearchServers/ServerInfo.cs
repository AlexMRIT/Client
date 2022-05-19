using UnityEngine;
using UnityEngine.UI;

namespace Client.UI.SearchServers
{
    public sealed class ServerInfo : MonoBehaviour
    {
        [HideInInspector] public int ServerId;

        public Text ServerName;
        public Text ServerDescription;
        public Text CountPlayers;
        public Button SelectServer;
    }
}