using System;
using common.Dependency;
using common.Dependency.Installer;
using common.Modules;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;

namespace common.Bootstrappers
{
    /// <summary>
    ///     This is the main class that is responsible to start entire common system.
    ///     Prepares dependency injection and registers core components needed for startup.
    ///     It must be instantiated and initialized (see <see cref="Initialize" />) first in an application.
    /// </summary>
    public class Bootstrapper : IDisposable
    {
        private ILogger _logger;
        private IModuleManager _moduleManager;

        public Bootstrapper(Type startupModuleType, Action<BootstrapperOptions> optionAction = null)
        {
            var options = new BootstrapperOptions();
            optionAction?.Invoke(options);

            _logger = NullLogger.Instance;

            StartupModuleType = startupModuleType ?? throw new ArgumentNullException(nameof(startupModuleType));
            IocManager = options.IocManager;
            if (!options.DisableAllInterceptors)
                AddInterceptorRegisters();
        }

        /// <summary>
        ///     Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModuleType { get; }

        /// <summary>
        ///     Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        public void Dispose()
        {
            IocManager?.Dispose();
            _moduleManager.ShutdownModules();
        }

        /// <summary>
        ///     Creates a new <see cref="Bootstrapper" /> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">
        ///     Startup module of the application which depends on other used modules. Should be
        ///     derived from <see cref="Module" />.
        /// </typeparam>
        /// <param name="optionsAction">An action to set options</param>
        public static Bootstrapper Create<TStartupModule>(Action<BootstrapperOptions> optionsAction = null)
            where TStartupModule : Module
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));
            return new Bootstrapper(typeof(TStartupModule), optionsAction);
        }


        /// <summary>
        ///     Initializes the common system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();
            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new CoreInstaller());

                _moduleManager = IocManager.Resolve<IModuleManager>();
                _moduleManager.Initialize(StartupModuleType);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(Bootstrapper));
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<Bootstrapper>())
                IocManager.IocContainer.Register(Component.For<Bootstrapper>().Instance(this));
        }

        private void AddInterceptorRegisters()
        {
        }
    }
}