using Common.Network.Clients;
using Common.Threading;
using System.Net;
using System.Net.Sockets;
using TcpClient = Common.Network.Clients.TcpClient;

namespace Common.Network.NetListener
{
    public class TcpListener : IListener {
        Socket listenerSock;
        CancellationTokenSource cancelToken = new();
        TaskPool pool = new(2);
        public TcpListener(string address, int port)
        {
            IPEndPoint ep = new(IPAddress.Parse(address), port);
            listenerSock = new(ep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenerSock.Bind(ep);
            listenerSock.Listen();
        }
        public void Start(Action<IClient> onConnect) {
            Task.Run(() =>
            {
            while (true) // we don't need a is alive variable as we cancel it using a cancel token
            {
                Socket remoteSock = listenerSock.Accept();
                pool.PendTask(() => { onConnect(new TcpClient(remoteSock)); });

                }
            },cancelToken.Token);
        }
        /// <summary>
        /// Closes the listener, However the socket is still active, Meaning you can restart it by calling the start method again!
        /// </summary>
        public void Stop() {
            cancelToken.Cancel();
        }
    }
}
