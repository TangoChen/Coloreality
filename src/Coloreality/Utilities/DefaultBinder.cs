using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Coloreality
{
    internal class DefaultBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type = null;
            try
            {
                string name = assemblyName.Split(',')[0];
                Assembly[] asmblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in asmblies)
                {
                    if (assembly.FullName.Split(',')[0] == name)
                    {
                        type = assembly.GetType(typeName);
                        break;
                    }
                }
                return type;
            }
            catch
            {
                return null;
            }

        }
    }

}
