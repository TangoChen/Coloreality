using System;

namespace Coloreality
{
    [Serializable]
    public struct PreSerialization
    {
        public int dataIndex;
        public int dataLength;

        public PreSerialization(int dataIndex, int dataLength)
        {
            this.dataIndex = dataIndex;
            this.dataLength = dataLength;
        }

        public byte[] ToSerialization()
        {
            return SerializationUtil.Serialize(this);
        }
    }
}
