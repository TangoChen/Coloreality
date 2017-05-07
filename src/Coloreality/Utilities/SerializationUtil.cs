using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coloreality
{
    public static class SerializationUtil
    {
        private static BinaryFormatter _binaryFormatter = null;
        private static BinaryFormatter binaryFormatter
        {
            get
            {
                if (_binaryFormatter == null)
                {
                    _binaryFormatter = new BinaryFormatter()
                    {
                        AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                        Binder = new DefaultBinder()
                    };
                }
                return _binaryFormatter;
            }
        }

        public static byte[] Serialize(object obj)
        {
            byte[] result;
            using (MemoryStream memSerialize = new MemoryStream())
            {
                binaryFormatter.Serialize(memSerialize, obj);
                result = memSerialize.ToArray();
            }
            return result;
        }
        
        public static object Deserialize(byte[] bytes)
        {
            object result = null;
            using (MemoryStream memDeserialize = new MemoryStream(bytes))
            {
                try
                {
                    result = binaryFormatter.Deserialize(memDeserialize);
                }
                catch
                {
                }
            }
            return result;
        }

    }

}
