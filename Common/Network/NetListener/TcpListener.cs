namespace Common.Network.NetListener {
    public class TcpListener : IListener {
        TaskPool
        public void Start(Action<IClient, Header, byte[]> onRecv) {
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }
}
