using Common.Network;
using Common.Network.Clients;

namespace SwitchApiServer
{
    public sealed class ApiServer : BaseServer
    {
        public ApiServer(string address, int port) : base(address, port) {}
        public override void OnConnect(IClient client)
        {
            base.OnConnect(client);
        }
        public override void OnDisconnect(IClient client)
        {
            base.OnDisconnect(client);
        }
        public override void OnMessage(IClient client, Header header, byte[] buff)
        {
            
        }

    }
}
