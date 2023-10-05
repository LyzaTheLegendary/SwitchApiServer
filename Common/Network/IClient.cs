namespace Common.Network {
    /// <summary>
    /// Interface used for both remote and local communication to process messages
    /// </summary>
    public interface IClient {
        void Send<T>(Header header, T message);
        void Disconnect();
        bool Connected();
    }
}
