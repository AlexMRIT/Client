using System.Net;
using UnityEngine;
using Client.Utilite;
using Client.Network;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Concurrent;

#pragma warning disable IDE0090
#pragma warning disable CS4014

namespace Client
{
    public class ModelViewConnection : MonoBehaviour
    {
        private readonly TcpClient _client = new TcpClient();
        private ClientProcessor _clientProcessor;
        private readonly ConcurrentQueue<byte[]> _receiveBufferQueue = new ConcurrentQueue<byte[]>();

        private async void Awake()
        {
            if (ClientProcessor.IsConnected)
                return;

            DontDestroyOnLoad(this);
            _clientProcessor = new ClientProcessor(_client, _receiveBufferQueue);
            bool resultConnect = await Task.Run(TryClientConnect);

            if (!resultConnect)
            {
                Debug.LogError("Critical error connecting to the server.");
                return;
            }

            Task.Factory.StartNew(_clientProcessor.ReadAsync, TaskCreationOptions.LongRunning);
        }

        private async Task<bool> TryClientConnect()
        {
            return await _clientProcessor.TryClientConnect(IPAddress.Any, ApplicationConfig.Port);
        }

        private void FixedUpdate()
        {
            if (_receiveBufferQueue.TryDequeue(out byte[] buffer))
                _clientProcessor.PacketHandler.HandlerPacket(buffer.ToPacket());
        }
    }
}