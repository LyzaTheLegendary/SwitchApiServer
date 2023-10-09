using Common.Network;
using Common.Network.Clients;
using Common.Network.NetListener;
using Common.Threading;

namespace SwitchApiServer
{
    public enum ServerType
    {
        TCP
    }
    public class BaseServer
    {
        IListener listener;
        TaskPool pool;
        public BaseServer(string address, int port, ServerType serverType = ServerType.TCP,int poolSize = 4) {
            pool = new(poolSize);
            if(serverType  == ServerType.TCP )
                listener = new TcpListener(address, port);

            listener?.Start(OnConnect);
        }
        public void PendTask(Action task)
           => pool.PendTask(task);
        
        public virtual void OnConnect(IClient client)
        {
            client.Listen(OnMessage);
        }
        public virtual void OnDisconnect(IClient client)
        {

        }
        public virtual void OnMessage(IClient client, Header header, byte[] buff)
        {

        }
    }
}
