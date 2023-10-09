using Common.Threading;
using Common.Util;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Common.Network.Clients
{
    /// <summary>
    /// Implements the IClient interface to handle all logic within the class and easily deal with the methods as an IClient to prevent confusion 
    /// </summary>
    public class TcpClient : IClient
    {
        private readonly mSock _sock;
        private readonly TaskPool _pool = new(4); // Can be higher if required, but this is the amount of action it can do "at the same time".
        private readonly Addr addressInfo;
        public TcpClient(string address, int port)
        {
            addressInfo = new Addr(address, port);
            _sock = new mSock(SocketType.Stream, ProtocolType.Tcp);
            //_onReceive = onReceive;

            _sock.ReceiveTimeout = 4000;
            _sock.SendTimeout = 4000;
            _sock.Connect(address, port);
        }
        public TcpClient(Socket sock)
        {
            _sock = new mSock(sock.SafeHandle);
            EndPoint ep = sock.RemoteEndPoint!; // Is not empty as we know it is a remote connection
            string[] data = ep.ToString()!.Split(":");

            addressInfo = new Addr(data[0], int.Parse(data[1]));
        }

        public void Listen(Action<IClient, Header, byte[]> onReceive)
        {
            _pool.PendTask(() =>
            {
                while (_sock.Connected)
                {
                    try
                    {
                        byte[] headerBuff = new byte[Marshal.SizeOf(typeof(Header))];
                        _sock.Receive(headerBuff, SocketFlags.None);

                        Header header = MarshalHelper.BytesToStruct<Header>(headerBuff);

                        byte[] buff = new byte[header.length];

                        _sock.Receive(buff);

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
            => _sock.Connected;

        public Addr GetAddr() => addressInfo;

        public void Disconnect()
        {
            _sock.Disconnect(false);
            _sock.Dispose();
        }

        public void Send<T>(Header header, T message)
        {
            _pool.PendTask(() => _sock.Send(header, message));
        }


    }
}
