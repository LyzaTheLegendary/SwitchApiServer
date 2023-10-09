using Common.Network.Clients;

namespace Common.Network.NetListener
{
    public interface IListener {
        public void Start(Action<IClient> onConnect);
        public void Stop();
    }
}
