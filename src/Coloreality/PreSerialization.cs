using System;

namespace Coloreality
{
    [Serializable]
    public struct PreSerialization
    {
        public int DataIndex { get; private set; }
        public int DataLength { get; private set; }

        public PreSerialization(int dataIndex, int dataLength)
        {
            DataIndex = dataIndex;
            DataLength = dataLength;
        }

        public byte[] ToSerialization()
        {
            return SerializationUtil.Serialize(this);
        }
    }
}
