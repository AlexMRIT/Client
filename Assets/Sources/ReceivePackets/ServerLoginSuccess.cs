using UnityEngine;
using Client.World;
using Client.Enums;
using Client.Utilite;
using Client.Network;
using System.Threading.Tasks;

namespace Client.ReceivePackets
{
    public sealed class ServerLoginSuccess : NetworkPacketBaseImplement
    {
        private readonly bool _autentication;
        private readonly bool _matchSearch;
        private readonly bool _gamePlaying;

        public ServerLoginSuccess(NetworkPacket packet)
        {
            _autentication = packet.InternalReadBool();
            _matchSearch = packet.InternalReadBool();
            _gamePlaying = packet.InternalReadBool();
        }

        public override Task ExecuteImplement()
        {
            GameObject client = DepedencyProvider.TryGetObjectByCode(DepedencyProvider.Code.ObjectClientProcessor);
            
            client.GetComponent<ModelViewConnection>().ChangeClientSession(_autentication, _matchSearch, _gamePlaying);
            client.GetComponent<AsyncLoadScene>().LoadScene((int)SceneId.SceneMatchSearch, null);

            return Task.CompletedTask;
        }
    }
}