using UnityEngine;
using Client.Enums;
using Client.Network;
using System.Threading.Tasks;

namespace Client.ReceivePackets
{
    public sealed class ServerLoginFail : NetworkPacketBaseImplement
    {
        private readonly ErrorCodeAuthetication _code;

        public ServerLoginFail(NetworkPacket packet, ModelViewConnection modelViewConnection)
        {
            _code = (ErrorCodeAuthetication)packet.ReadByte();
        }

        public override Task ExecuteImplement()
        {
            switch (_code)
            {
                case ErrorCodeAuthetication.ReasonUserOrPassWrong:
                    Debug.LogWarning("The username or password you entered is incorrect. Try again. Make sure you don't have CAPS LOCK enabled.");
                    break;
                case ErrorCodeAuthetication.ReasonAccountInUse:
                    Debug.Log("Your account is already active. Try to come back later.");
                    break;
                default:
                    Debug.Log("Unknown error, contact technical support.");
                    break;
            }

            return Task.CompletedTask;
        }
    }
}