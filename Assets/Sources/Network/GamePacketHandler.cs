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

        }
    }
}