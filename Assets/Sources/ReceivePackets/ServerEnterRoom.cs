using UnityEngine;
using Client.Enums;
using Client.World;
using Client.Models;
using Client.Utilite;
using Client.Network;
using Client.Contracts;
using System.Threading.Tasks;

namespace Client.ReceivePackets
{
    public sealed class ServerEnterRoom : NetworkPacketBaseImplement
    {
        private readonly AsyncLoadScene _loadScene;
        private readonly ModelViewConnection _network;

        private readonly int _characterId;
        private readonly int _score;
        private readonly string _name;
        private readonly float _attackSpeed;
        private readonly float _moveSpeed;
        private readonly int _physicsAttackMin;
        private readonly int _physicsAttackMax;

        private readonly bool _autentication;
        private readonly bool _matchSearch;
        private readonly bool _gamePlaying;

        public ServerEnterRoom(NetworkPacket packet, ModelViewConnection modelViewConnection)
        {
            GameObject client = DepedencyProvider.TryGetObjectByCode(DepedencyProvider.Code.ObjectClientProcessor);
            _loadScene = client.GetComponent<AsyncLoadScene>();
            _network = modelViewConnection;

            _characterId = packet.ReadInt();
            _score = packet.ReadInt();
            _name = packet.ReadString(packet.ReadInt());
            _attackSpeed = (float)packet.ReadDouble();
            _moveSpeed = (float)packet.ReadDouble();
            _physicsAttackMin = packet.ReadInt();
            _physicsAttackMax = packet.ReadInt();

            _autentication = packet.InternalReadBool();
            _matchSearch = packet.InternalReadBool();
            _gamePlaying = packet.InternalReadBool();
        }

        public override Task ExecuteImplement()
        {
            CharacterContract characterContract = new CharacterContract(_characterId, _score, _name, 
                _attackSpeed, _moveSpeed, _physicsAttackMin, _physicsAttackMax);
            CharacterSpecification characterSpecification = new CharacterSpecification(_characterId);

            _network.ChangeClientSession(_autentication, _matchSearch, _gamePlaying);
            _loadScene.LoadScene((int)SceneId.SceneGamePlaying, null, (model) =>
            {
                model.CreateNetworkCharacter(characterContract, characterSpecification);
            });

            return Task.CompletedTask;
        }
    }
}