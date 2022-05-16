using System;
using Client.Network;
using System.Collections.Generic;

namespace Client.Utilite
{
    public static class NetworkPacketHelper
    {
        public static void BuildBufferWithOpcodePacket(this List<byte> bytes, byte[] buffer)
        {
            bytes.AddRange(BitConverter.GetBytes((short)(buffer.Length + 2)));
            bytes.AddRange(buffer);
        }

        public static NetworkPacket ToPacket(this byte[] buffer, int extraBytes = 0)
        {
            return new NetworkPacket(1 + extraBytes, buffer);
        }
    }
}