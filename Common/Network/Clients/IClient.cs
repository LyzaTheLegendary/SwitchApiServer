namespace Common.Network.Clients
{
    /// <summary>
    /// Interface used for both remote and local communication to process messages
    /// </summary>
    public interface IClient
    {
        void Listen(Action<IClient, Header, byte[]> onReceive);
        void Send<T>(Header header, T message);
        void Disconnect();
        bool Connected();
        Addr GetAddr();
    }
    public struct Addr
    {
        public readonly string address;
        public readonly int port;
        public Addr(string address, int port)
        {
            this.address = address;
            this.port = port;
        }
        public override string ToString()
            => $"{address}:{port}";

    }
}
