using Client.Models;
using Client.Network;
using Client.Contracts;
using System.Threading.Tasks;

namespace Client.ReceivePackets
{
    public sealed class ServerAddMe : NetworkPacketBaseImplement
    {
        private readonly ModelViewConnection _network;

        private readonly int ObjId;

        public ServerAddMe(NetworkPacket packet, ModelViewConnection modelViewConnection)
        {
            _network = modelViewConnection;

            ObjId = packet.ReadInt();
        }

        public override Task ExecuteImplement()
        {
            CharacterContract characterContract = new CharacterContract(ObjId, 0, $"Bot {ObjId}", 1, 2, 0, 0);
            CharacterSpecification characterSpecification = new CharacterSpecification(ObjId);

            _network.CreateNetworkCharacter(characterContract, characterSpecification, true);

            return Task.CompletedTask;
        }
    }
}