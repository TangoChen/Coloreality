using System;
using System.Text;

namespace Coloreality
{
    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }

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

    public class ReceiveEventArgs : EventArgs
    {
        /// <summary>
        /// Full raw bytes message.
        /// </summary>
        public byte[] RawMessage { get; private set; }

        /// <summary>
        /// Beginning byte as a DataType.
        /// </summary>
        public DataType MessageType { get; private set; }

        /// <summary>
        /// Rest bytes(excepts the beginning one.)
        /// </summary>
        public byte[] MessagePart { get; private set; }

        private object serializedObject = null;
        public object SerializedObject
        {
            get
            {
                return serializedObject;
            }
        }

        private string messageInString = string.Empty;

        /// <summary>
        /// Default is string.Empty.
        /// </summary>
        public string MessageInString
        {
            get
            {
                if (MessageType == DataType.String && messageInString == string.Empty)
                {
                    messageInString = Encoding.UTF8.GetString(MessagePart);
                }
                return messageInString;
            }
        }

        public ReceiveEventArgs(byte[] rawMessage)
        {

            RawMessage = rawMessage;
            MessagePart = ByteUtil.GetStartRemovedBytes(rawMessage);

            if (Enum.IsDefined(typeof(DataType), (int)rawMessage[0]))
            {
                MessageType = (DataType)rawMessage[0];
            }
            else
            {
                MessageType = DataType.Unknown;
            }

            if (MessageType == DataType.PreSerialization || MessageType == DataType.Serialization)
            {
                serializedObject = SerializationUtil.Deserialize(MessagePart);
            }

        }

    }

}
