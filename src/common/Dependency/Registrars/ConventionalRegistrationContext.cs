using System.Reflection;

namespace common.Dependency.Registrars
{
    public class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager)
        {
            Assembly = assembly;
            IocManager = iocManager;
        }

        public Assembly Assembly { get; }
        public IIocManager IocManager { get; }
    }
}