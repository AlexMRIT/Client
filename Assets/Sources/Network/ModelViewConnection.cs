using System.Net;
using UnityEngine;
using Client.World;
using Client.Models;
using Client.Utilite;
using Client.Network;
using Client.Contracts;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Concurrent;

#pragma warning disable IDE0090
#pragma warning disable CS4014

namespace Client
{
    [RequireComponent(typeof(AsyncLoadScene))]
    public class ModelViewConnection : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabCharacter;

        private readonly TcpClient _client = new TcpClient();
        private ClientProcessor _clientProcessor;
        private readonly ConcurrentQueue<byte[]> _receiveBufferQueue = new ConcurrentQueue<byte[]>();
        private readonly ClientSession _currentClientSession = new ClientSession(authorization: false, matchSearch: false, gamePlaying: false);

        public Character _currentCharacter { get; private set; }

        private void OnEnable()
        {
            DepedencyProvider.TryAddObject(DepedencyProvider.Code.ObjectClientProcessor, gameObject);
        }

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
            return await _clientProcessor.TryClientConnect(IPAddress.Parse("127.0.0.1"), ApplicationConfig.Port);
        }

        private void FixedUpdate()
        {
            if (_receiveBufferQueue.TryDequeue(out byte[] buffer))
                _clientProcessor.PacketHandler.HandlerPacket(buffer.ToPacket(), this);
        }

        private void OnDisable()
        {
            _clientProcessor.ClientDisconnect();
            Debug.Log("Close connecting");
        }

        public void SendPacket(NetworkPacket packet)
        {
            _clientProcessor.WriteAsync(packet);
        }

        public void ChangeClientSession(bool authorization, bool matchSearch, bool gamePlaying)
        {
            _currentClientSession.SessionClientAuthorization = authorization;
            _currentClientSession.SessionClientMatchSearch = matchSearch;
            _currentClientSession.SessionClientGamePlaying = gamePlaying;
        }

        public ClientSession GetClientSession() => _currentClientSession;

        public void CreateNetworkCharacter(CharacterContract characterContract, CharacterSpecification characterSpecification)
        {
            _currentCharacter = Instantiate(_prefabCharacter, new Vector3(0f, 0f, 0f), Quaternion.identity).GetComponent<Character>();
            _currentCharacter.Init(characterSpecification, characterContract);
        }
    }
}