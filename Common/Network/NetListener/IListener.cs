namespace Common.Network.NetListener {
    internal interface IListener {
        public void Start(Action<IClient> onConnect);
        public void Stop();
    }
}
