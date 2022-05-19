using Client.Network;
using Client.Utilite;

#pragma warning disable IDE0090

namespace Client.RequestPackets
{
    public sealed class SendPacketTryCreateServer
    {
        internal static NetworkPacket ToPacket(string name, string description)
        {
            NetworkPacket packet = new NetworkPacket(OpcodeExtension.OpcodeCreateServer);

            packet.WriteString(name);
            packet.WriteString(description);

            return packet;
        }
    }
}