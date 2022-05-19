using UnityEngine;
using Client.Network;
using System.Threading.Tasks;

namespace Client.ReceivePackets
{
    public sealed class ServerCharacterMoveToLocation : NetworkPacketBaseImplement
    {
        private readonly Vector3 _direction;
        private readonly ModelViewConnection _network;

        public ServerCharacterMoveToLocation(NetworkPacket packet, ModelViewConnection modelViewConnection)
        {
            _direction = new Vector3((float)packet.ReadDouble(), (float)packet.ReadDouble(), (float)packet.ReadDouble());
            _network = modelViewConnection;
        }

        public override Task ExecuteImplement()
        {
            if (Vector3.Distance(_network._currentCharacter.gameObject.transform.position, _direction) > 1.0f)
                _network._currentCharacter.gameObject.transform.position = _direction;

            return Task.CompletedTask;
        }
    }
}