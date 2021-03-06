using Client.Network;
using Client.Utilite;

#pragma warning disable IDE0090

namespace Client.RequestPackets
{
    public sealed class SendPacketTryAuthentication
    {
        internal static NetworkPacket ToPacket(ClientSession session, string login, string password)
        {
            NetworkPacket packet = new NetworkPacket(OpcodeExtension.OpcodeAuthentication);

            packet.InternalWriteBool(session.SessionClientAuthorization);
            packet.InternalWriteBool(session.SessionClientMatchSearch);
            packet.InternalWriteBool(session.SessionClientGamePlaying);

            packet.WriteString(login);
            packet.WriteString(password);

            return packet;
        }
    }
}