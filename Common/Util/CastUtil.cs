namespace Common.Util {
    public static class CastUtil {
        public static T Cast<T>(this byte[] buff) {
            return MarshalHelper.BytesToStruct<T>(buff);
        }
    }
}
