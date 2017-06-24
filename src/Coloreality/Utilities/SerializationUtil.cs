using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coloreality
{
    public static class SerializationUtil
    {
        private static BinaryFormatter defaultBinaryFormatter = null;
        private static BinaryFormatter DefaultBinaryFormatter
        {
            get
            {
                if (defaultBinaryFormatter == null)
                {
                    defaultBinaryFormatter = new BinaryFormatter()
                    {
                        AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                        Binder = new DefaultBinder()
                    };
                }
                return defaultBinaryFormatter;
            }
        }

        public static byte[] Serialize(object obj)
        {
            byte[] result;
            using (MemoryStream memSerialize = new MemoryStream())
            {
                DefaultBinaryFormatter.Serialize(memSerialize, obj);
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
                    result = DefaultBinaryFormatter.Deserialize(memDeserialize);
                }
                catch
                {
                }
            }
            return result;
        }

    }

}
