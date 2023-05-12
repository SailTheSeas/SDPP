<<<<<<< Updated upstream
using System.Reflection;

namespace UnityEngine.TestTools.Utils
{
    internal class AssemblyLoadProxy : IAssemblyLoadProxy
    {
        public IAssemblyWrapper Load(string assemblyString)
        {
            return new AssemblyWrapper(Assembly.Load(assemblyString));
        }
    }
}
=======
using System.Reflection;

namespace UnityEngine.TestTools.Utils
{
    internal class AssemblyLoadProxy : IAssemblyLoadProxy
    {
        public IAssemblyWrapper Load(string assemblyString)
        {
            return new AssemblyWrapper(Assembly.Load(assemblyString));
        }
    }
}
>>>>>>> Stashed changes
