using common.Dependency;

namespace common.Bootstrappers
{
    public class BootstrapperOptions
    {
        public BootstrapperOptions()
        {
            IocManager = Dependency.IocManager.Instance;
        }

        /// <summary>
        ///     Used to disable all interceptors added by ABP.
        /// </summary>
        public bool DisableAllInterceptors { get; set; }

        /// <summary>
        ///     IIocManager that is used to bootstrap the ABP system. If set to null, uses global
        ///     <see cref="common.Dependency.IocManager.Instance" />
        /// </summary>
        public IIocManager IocManager { get; set; }
    }
}