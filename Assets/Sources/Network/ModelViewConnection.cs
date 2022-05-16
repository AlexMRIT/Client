using System.Net;
using UnityEngine;
using Client.Network;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Concurrent;

#pragma warning disable IDE0090

namespace Client
{
    public class ModelViewConnection : MonoBehaviour
    {
        private readonly TcpClient _client = new TcpClient();
        private ClientProcessor _clientProcessor;

        private async void Awake()
        {
            if (ClientProcessor.IsConnected)
                return;

            DontDestroyOnLoad(this);
            _clientProcessor = new ClientProcessor(_client);
            bool resultConnect = await Task.Run(TryClientConnect);

            if (!resultConnect)
            {
                Debug.LogError("Critical error connecting to the server.");
                return;
            }
        }

        private async Task<bool> TryClientConnect()
        {
            return await _clientProcessor.TryClientConnect(IPAddress.Any, ApplicationConfig.Port);
        }
    }
}