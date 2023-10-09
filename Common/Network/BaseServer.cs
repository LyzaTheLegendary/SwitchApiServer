using Common.Network;
using Common.Network.Clients;
using Common.Network.NetListener;

namespace SwitchApiServer
{
    public class BaseServer
    {
        IListener listener;
        public BaseServer(string address, int port) { 
            listener = new TcpListener(address, port);
            listener.Start(OnConnect);
        }
        public virtual void OnConnect(IClient client)
        {
            ((TcpClient)client).Listen(OnMessage);
        }
        public virtual void OnDisconnect(IClient client)
        {

        }
        public virtual void OnMessage(IClient client, Header header, byte[] buff)
        {

        }
    }
}
