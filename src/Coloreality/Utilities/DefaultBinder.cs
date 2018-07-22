using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Coloreality
{
    internal class DefaultBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            try
            {
                string name = assemblyName.Split(',')[0];
                Assembly[] asmblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in asmblies)
                {
                    if (assembly.FullName.Split(',')[0] == name)
                    {
                        return assembly.GetType(typeName);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
    }

}
