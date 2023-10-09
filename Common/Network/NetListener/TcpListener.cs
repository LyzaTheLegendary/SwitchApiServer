using System.Net;
using System.Net.Sockets;

namespace Common.Network.NetListener {
    public class TcpListener : IListener {
        Socket listenerSock;
        CancellationTokenSource cancelToken = new();
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

                }
            },cancelToken.Token);
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }
}
