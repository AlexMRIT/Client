using System.Net;
using Client.Exceptions;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client.Network
{
    public sealed class ClientProcessor
    {
        public ClientProcessor(TcpClient client)
        {
            Client = client;
        }

        private readonly TcpClient Client;
        private readonly NetworkStream ClientStream;

        public static bool IsConnected { get; private set; }

        public async Task<bool> TryClientConnect(IPAddress address, int port)
        {
            try
            {
                await Client.ConnectAsync(address, port);
            }
            catch (SocketException exception)
            {
                ExceptionHandler.ExecuteSocketException(exception, nameof(ClientProcessor.TryClientConnect));
                return IsConnected = false;
            }

            return IsConnected = true;
        }

        
    }
}