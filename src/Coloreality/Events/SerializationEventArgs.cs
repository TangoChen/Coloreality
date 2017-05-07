using System;

namespace Coloreality
{
    public delegate void SerializationReadyEventHandler(object sender, SerializationEventArgs e);

    public class SerializationEventArgs : EventArgs
    {
        public int DataIndex { get; private set; }
        public byte[] Data { get; private set; }
        public bool CanSkip { get; private set; }
        public SerializationEventArgs(int dataIndex, byte[] data = null, bool canSkip = false)
        {
            DataIndex = dataIndex;
            Data = data;
            CanSkip = canSkip;
        }
    }
}