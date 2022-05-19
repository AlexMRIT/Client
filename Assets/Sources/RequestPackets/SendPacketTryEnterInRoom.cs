using Client.Network;
using Client.Utilite;

#pragma warning disable IDE0090

namespace Client.RequestPackets
{
    public sealed class SendPacketTryEnterInRoom
    {
        internal static NetworkPacket ToPacket(int roomId, ClientSession session)
        {
            NetworkPacket packet = new NetworkPacket(OpcodeExtension.OpcodeEnterRoom);

            packet.WriteInt(roomId);

            packet.InternalWriteBool(session.SessionClientAuthorization);
            packet.InternalWriteBool(session.SessionClientMatchSearch);
            packet.InternalWriteBool(session.SessionClientGamePlaying);

            return packet;
        }
    }
}