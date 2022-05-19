using UnityEngine;
using Client.Utilite;
using Client.Network;
using Client.Contracts;
using System.Threading.Tasks;
using Client.UI.SearchServers;

namespace Client.ReceivePackets
{
    public sealed class ServerSendRoom : NetworkPacketBaseImplement
    {
        private readonly ServerListCoreAPI _serverListCoreAPI;
        private readonly ModelViewConnection _network;

        private readonly ServerRoom[] _serverRooms;
        private readonly bool _authentication;
        private readonly bool _matchSearch;
        private readonly bool _gamePlaying;

        public ServerSendRoom(NetworkPacket packet, ModelViewConnection modelViewConnection)
        {
            _serverListCoreAPI = DepedencyProvider.TryGetObjectByCode(DepedencyProvider.Code.ObjectServerList).GetComponent<ServerListCoreAPI>();
            _network = modelViewConnection;

            _serverRooms = new ServerRoom[packet.ReadInt()];
            for (int iterator = 0; iterator < _serverRooms.Length; iterator++)
                _serverRooms[iterator] = new ServerRoom(id: packet.ReadInt(), name: packet.ReadString
                    (packet.ReadInt()), description: packet.ReadString(packet.ReadInt()), currPlayer: packet.ReadInt(), maxPlayer: packet.ReadInt());

            _authentication = packet.InternalReadBool();
            _matchSearch = packet.InternalReadBool();
            _gamePlaying = packet.InternalReadBool();
        }

        public override Task ExecuteImplement()
        {
            foreach (ServerRoom server in _serverRooms)
                _serverListCoreAPI.AddServer(server);

            _network.ChangeClientSession(_authentication, _matchSearch, _gamePlaying);

            Debug.Log($"Added: {_serverRooms.Length} count.");
            return Task.CompletedTask;
        }
    }
}