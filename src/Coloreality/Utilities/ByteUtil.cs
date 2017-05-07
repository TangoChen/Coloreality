using System;

namespace Coloreality
{
    public static class ByteUtil
    {
        public static byte[] GetStartDefinedBytes(byte firstByte, byte[] raw)
        {
            byte[] newBytes = new byte[raw.Length + 1];
            newBytes[0] = firstByte;
            Buffer.BlockCopy(raw, 0, newBytes, 1, raw.Length);
            return newBytes;
        }

        public static byte[] GetStartRemovedBytes(byte[] value)
        {
            if (value.Length > 1)
            {
                byte[] newBytes = new byte[value.Length - 1];
                Buffer.BlockCopy(value, 1, newBytes, 0, newBytes.Length);
                return newBytes;
            }
            else
            {
                return value;
            }
        }

    }
}
