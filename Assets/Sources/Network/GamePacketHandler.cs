using System;
using UnityEngine;
using Client.Enums;
using Client.ReceivePackets;
using System.Collections.Concurrent;

namespace Client.Network
{
    public sealed class GamePacketHandler
    {
        private readonly ConcurrentDictionary<byte, Type> ClientPackets = new ConcurrentDictionary<byte, Type>();

        public GamePacketHandler()
        {
            ClientPackets.TryAdd((byte)Opcodes.ServerLoginFail, typeof(ServerLoginFail));
            ClientPackets.TryAdd((byte)Opcodes.ServerLoginSuccess, typeof(ServerLoginSuccess));
            ClientPackets.TryAdd((byte)Opcodes.ServerSendRoom, typeof(ServerSendRoom));
            ClientPackets.TryAdd((byte)Opcodes.ServerDamageResult, typeof(ServerDamageResult));
            ClientPackets.TryAdd((byte)Opcodes.ServerCharacterStopMove, typeof(ServerCharacterStopMove));
            ClientPackets.TryAdd((byte)Opcodes.ServerCharacterMoveToLocation, typeof(ServerCharacterMoveToLocation));
            ClientPackets.TryAdd((byte)Opcodes.ServerCharacterUpdate, typeof(ServerCharacterUpdate));
            ClientPackets.TryAdd((byte)Opcodes.ServerCharacterDeath, typeof(ServerCharacterDeath));
            ClientPackets.TryAdd((byte)Opcodes.ServerExitAllCharacterForRoom, typeof(ServerExitAllCharacterForRoom));
            ClientPackets.TryAdd((byte)Opcodes.ServerDeleteMe, typeof(ServerDeleteMe));
            ClientPackets.TryAdd((byte)Opcodes.ServerAddMe, typeof(ServerAddMe));
        }

        public void HandlerPacket(NetworkPacket packet)
        {
            Debug.Log($"Received packet: {packet.FirstOpcode:X2}:{packet.SecondOpcode:X2}");

            NetworkPacketBaseImplement networkPacket = null;

            if (ClientPackets.ContainsKey(packet.FirstOpcode))
            {
                Debug.Log($"Received packet of type: {ClientPackets[packet.FirstOpcode].Name}");
                networkPacket = (NetworkPacketBaseImplement)Activator.CreateInstance(ClientPackets[packet.FirstOpcode], args: packet);
            }

            if (networkPacket is null)
                throw new ArgumentNullException(nameof(NetworkPacketBaseImplement), $"Packet with opcode: {packet.FirstOpcode:X2} doesn't exist in the dictionary.");

            networkPacket.ExecuteImplement();
        }
    }
}