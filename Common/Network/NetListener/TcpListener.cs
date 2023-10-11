namespace Common.Network.NetListener {
    public class TcpListener : IListener {
        public void Start(Action<IClient, Header, byte[]> onRecv) {
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }
}
