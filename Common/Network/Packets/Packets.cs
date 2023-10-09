using System.Runtime.InteropServices;
using System.Text;

namespace Common.Network.Packets {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TS_CS_LOGIN_REQUEST {
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 50)]
        private readonly byte[] username;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        private readonly byte[] password;

        public string GetUsername() => Encoding.UTF8.GetString(username);
        public string GetPassword() => Encoding.UTF8.GetString(password);
        
    }
}
