using Common.Util;
using System.Runtime.InteropServices;

namespace Common.Network {
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public readonly struct Header {
        [MarshalAs(UnmanagedType.U4)]
        public readonly ushort length;
        [MarshalAs(UnmanagedType.U4)]
        public readonly ushort id;
        public Header(ushort _length, ushort _id) {
            length = _length;
            id = _id;
        }
        public static explicit operator byte[](Header header) 
            => MarshalHelper.StructToBytes(header);

        public static implicit operator Header(byte[] buff) {
            if (buff.Length != Marshal.SizeOf(typeof(Header)))
                throw new ArgumentException($"invalid array was not the same length as struct hex: {BitConverter.ToString(buff)}");

            return MarshalHelper.BytesToStruct<Header>(buff);
        }
        
    }
}
