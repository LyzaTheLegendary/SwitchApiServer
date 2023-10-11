namespace Common.Network {
    /// <summary>
    /// Interface used for both remote and local communication to process messages
    /// </summary>
    public interface IClient {
        void Send<T>(Header header, T message);
        void Disconnect();
        bool Connected();
    }
    public struct Addr {
        public readonly string address;
        public readonly int port;
        public Addr(string address, int port) {
            this.address = address;
            this.port = port;
        }
        public override string ToString()
            => $"{address}:{port}";
        
    }
}
