using System;
using System.Net;
using UnityEngine;
using Client.Utilite;
using Client.Exceptions;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

#pragma warning disable IDE0090

namespace Client.Network
{
    public sealed class ClientProcessor
    {
        public ClientProcessor(TcpClient client, ConcurrentQueue<byte[]> linqBuffer)
        {
            _client = client;
            PacketHandler = new GamePacketHandler();
            _bufferQueue = linqBuffer;
        }

        private readonly TcpClient _client;
        private NetworkStream _clientStream;
        private readonly ConcurrentQueue<byte[]> _bufferQueue;

        private const int OpCodeLength = 2;

        public static bool IsConnected { get; private set; }
        public readonly GamePacketHandler PacketHandler;

        public async Task<bool> TryClientConnect(IPAddress address, int port)
        {
            if (IsConnected)
                return false;

            try
            {
                await _client.ConnectAsync(address, port);
                _clientStream = _client.GetStream();
            }
            catch (SocketException exception)
            {
                ExceptionHandler.ExecuteSocketException(exception, nameof(ClientProcessor.TryClientConnect));
                return IsConnected = false;
            }

            return IsConnected = true;
        }

        public void ClientDisconnect()
        {
            Debug.Log("Call termination client.");

            IsConnected = false;
            _clientStream.Close();
            _client.Close();
        }

        public async void WriteAsync(NetworkPacket packet)
        {
            if (!IsConnected)
                return;

            byte[] buffer = packet.GetBuffer();
            List<byte> bytesBuild = new List<byte>();
            bytesBuild.BuildBufferWithOpcodePacket(buffer);

            try
            {
                await _clientStream.WriteAsync(bytesBuild.ToArray(), 0, bytesBuild.Count);
                await _clientStream.FlushAsync();
            }
            catch
            {
                ClientDisconnect();
            }
        }

        public async void ReadAsync()
        {
            try
            {
                while (true)
                {
                    if (!IsConnected)
                        return;

                    byte[] _buffer = new byte[OpCodeLength];
                    int bytesRead = await _clientStream.ReadAsync(_buffer, 0, OpCodeLength);

                    if (bytesRead == 0)
                    {
                        Debug.LogError("Client closed connection");
                        ClientDisconnect();
                        return;
                    }

                    if (bytesRead != OpCodeLength)
                        throw new NetworkPacketException("Wrong packet");

                    short length = BitConverter.ToInt16(_buffer, 0);
                    _buffer = new byte[length - OpCodeLength];

                    bytesRead = await _clientStream.ReadAsync(_buffer, 0, length - OpCodeLength);

                    if (bytesRead != length - OpCodeLength)
                        throw new NetworkPacketException("Wrong packet");

                    _bufferQueue.Enqueue(_buffer);
                }
            }
            catch (SocketException socketException)
            {
                ExceptionHandler.ExecuteSocketException(socketException, nameof(ClientProcessor.ReadAsync));
            }
            catch (Exception exception)
            {
                ExceptionHandler.Execute(exception, nameof(ClientProcessor.ReadAsync));
            }
            finally
            {
                ClientDisconnect();
            }
        }
    }
}