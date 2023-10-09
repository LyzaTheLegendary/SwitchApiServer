using Common.Threading;
using Common.Util;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Common.Network {
    /// <summary>
    /// Implements the IClient interface to handle all logic within the class and easily deal with the methods as an IClient to prevent confusion 
    /// </summary>
    public class TcpClient : IClient {
        private readonly mSock sock;
        private readonly TaskPool _pool = new(4); // Can be higher if required, but this is the amount of action it can do "at the same time".
        public TcpClient(string address, int port) {
            sock = new mSock(SocketType.Stream, ProtocolType.Tcp);
            //_onReceive = onReceive;

            sock.ReceiveTimeout = 4000;
            sock.SendTimeout = 4000;
            sock.Connect(address, port);
        }
        public TcpClient(Socket sock)
        {
            sock = new mSock(sock.SafeHandle);
        }

        public void Listen(Action<IClient, Header, byte[]> onReceive) {
            _pool.PendTask(() =>
            {
                while (sock.Connected)
                {
                    try
                    {
                        byte[] headerBuff = new byte[Marshal.SizeOf(typeof(Header))];
                        sock.Receive(headerBuff, SocketFlags.None);

                        Header header = MarshalHelper.BytesToStruct<Header>(headerBuff);

                        byte[] buff = new byte[header.length];

                        sock.Receive(buff);

                        _pool.PendTask(() => { onReceive(this, header, buff); });
                    }
                    catch (SocketException e)
                    {
                        /* 10052 WSAENETRESET // windows
                         * 104 ECONNRESET // linux
                         * Network dropped connection on reset The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress. It can also be returned by setsockopt if an attempt is made to set SO_KEEPALIVE on a connection that has already failed. */
#if (DEBUG_WINDOWS)
                        if (e.NativeErrorCode == 10052)
                            return;
#endif
#if (DEBUG_LINUX)
                    if (e.NativeErrorCode == 104)
                        return;
#endif

                        //TODO: fix console to report error due to failed packet!
                    }
                }
            });
        }
        public bool Connected()
            => sock.Connected;
            
        

        public void Disconnect() {
            sock.Disconnect(false);
            sock.Dispose();
        }

        public void Send<T>(Header header, T message) {
            _pool.PendTask( () => sock.Send(header, message) );
        }
    }
}
