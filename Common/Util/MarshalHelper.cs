using Common.Exceptions;
using System.Runtime.InteropServices;

namespace Common.Util {
    public static class MarshalHelper {
        public static byte[] StructToBytes<T>(T structure) {
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure!, ptr, false);

            byte[] memBuff = new byte[Marshal.SizeOf(structure)];
              
            Marshal.Copy(ptr,memBuff,0,Marshal.SizeOf(structure));
            Marshal.FreeCoTaskMem(ptr);

            if (memBuff.Length != Marshal.SizeOf(structure) || memBuff == null)
                throw new mException($"Invalid memory allocated tried to marshal struct: {typeof(T).Name}");

            return memBuff!;
        }

        public static T BytesToStruct<T>(byte[] buff) {

            if (buff.Length != Marshal.SizeOf(typeof(T)))
                throw new mException($"Invalid buffer given length: {buff.Length}");

            IntPtr ptr = Marshal.AllocCoTaskMem(buff.Length);
            Marshal.Copy(buff, 0, ptr, buff.Length);

            T? structure = (T?)Marshal.PtrToStructure(ptr, typeof(T));

#pragma warning disable IDE0270 // Use coalesce expression
            if (structure is null)
                throw new mException($"Failed to marshal structure buff in hex: {BitConverter.ToString(buff)}");
#pragma warning restore IDE0270 // Use coalesce expression

            return structure;
        }
    }
}
