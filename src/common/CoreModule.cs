using common.Configurations;
using common.Dependency;
using common.Dependency.Scoped;
using common.Modules;

namespace common
{
    /// <summary>
    ///     Kernel (core) module of the system.
    ///     No need to depend on this, it's automatically the first module always.
    /// </summary>
    public class CoreModule : Module
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            IocManager.Register<IScopedIocResolver, ScopedIocResolver>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var replaceAction in ((StartupConfiguration) Configuration).ServiceReplaceActions.Values)
                replaceAction();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }


        public override void Shutdown()
        {
            base.Shutdown();
        }
    }
}