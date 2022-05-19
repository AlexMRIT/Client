using Client.Network;
using Client.Utilite;

#pragma warning disable IDE0090

namespace Client.RequestPackets
{
    public sealed class SendPacketTryUpdateServerList
    {
        internal static NetworkPacket ToPacket(ClientSession session)
        {
            NetworkPacket packet = new NetworkPacket(OpcodeExtension.OpcodeGetRoomsWindow);

            packet.InternalWriteBool(session.SessionClientAuthorization);
            packet.InternalWriteBool(session.SessionClientMatchSearch);
            packet.InternalWriteBool(session.SessionClientGamePlaying);

            return packet;
        }
    }
}