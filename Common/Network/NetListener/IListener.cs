namespace Common.Network.NetListener {
    internal interface IListener {
        public void Start(Action<IClient, Header, byte[]> onRecv);
        public void Stop();
    }
}
