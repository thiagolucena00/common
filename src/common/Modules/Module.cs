using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using common.Configuration;
using common.Dependency;
using common.Exceptions;
using Castle.Core.Logging;

namespace common.Modules
{
    /// <summary>
    ///     This class must be implemented by all module definition classes.
    /// </summary>
    /// <remarks>
    ///     A module definition class is generally located in it's own assembly
    ///     and implements some action in module events on application startup and shutdown.
    ///     It also defines depended modules.
    /// </remarks>
    public abstract class Module
    {
        public Module()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        ///     Gets a reference to the IOC manager.
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        ///     Gets a reference to the configuration.
        /// </summary>
        protected internal IStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     This is the first event called on application startup.
        ///     Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        public virtual void PreInitialize()
        {
        }

        /// <summary>
        ///     This method is used to register dependencies for this module.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     This method is called lastly on application startup.
        /// </summary>
        public virtual void PostInitialize()
        {
        }

        /// <summary>
        ///     This method is called when the application is being shutdown.
        /// </summary>
        public virtual void Shutdown()
        {
        }

        /// <summary>
        ///     Checks if given type is an module class.
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass &&
                   !typeInfo.IsAbstract &&
                   !typeInfo.IsGenericType &&
                   typeof(Module).IsAssignableFrom(type);
        }

        /// <summary>
        ///     Finds direct depended modules of a module (excluding given module).
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsModule(moduleType))
                throw new InitializationException("This type is not an ABP module: " +
                                                  moduleType.AssemblyQualifiedName);

            var list = new List<Type>();
            if (!moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true)) return list;

            var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true)
                .Cast<DependsOnAttribute>();
            list.AddRange(dependsOnAttributes.SelectMany(dependsOnAttribute => dependsOnAttribute.DependedModuleTypes));

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();

            AddModuleAndDependenciesRecursively(list, moduleType);
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type moduleType)
        {
            if (!IsModule(moduleType))
                throw new InitializationException("This type is not an common module: " +
                                                  moduleType.AssemblyQualifiedName);

            if (modules.Contains(moduleType))
                return;

            modules.Add(moduleType);
            var dependedModules = FindDependedModuleTypes(moduleType);
            foreach (var dependedModule in dependedModules)
                AddModuleAndDependenciesRecursively(modules, dependedModule);
        }
    }
}