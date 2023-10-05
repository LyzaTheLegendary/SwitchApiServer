using Common.Util;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("TcpClient")]
namespace Common.Network {
    /// <summary>
    /// Encapsulation of the socket class to implement certain methods to make life easier
    /// </summary>
#pragma warning disable IDE1006 // Naming Styles
    internal class mSock : Socket {
#pragma warning restore IDE1006 // Naming Styles
        public mSock(SocketType socketType, ProtocolType protocolType) : base(socketType, protocolType) {
        }

        public mSock(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType) : base(addressFamily, socketType, protocolType) {
        }

        public void Send<T>(Header header,T message) {
            List<byte> buff = new(Marshal.SizeOf(header) + Marshal.SizeOf(message));
            buff.AddRange(MarshalHelper.StructToBytes(header));
            buff.AddRange(MarshalHelper.StructToBytes(message));
            Send(buff.ToArray());
        }
            
        
    }
}
