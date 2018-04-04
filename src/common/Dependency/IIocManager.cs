using System;
using System.Reflection;
using common.Dependency.Registrars;
using Castle.Windsor;

namespace common.Dependency
{
    public interface IIocManager : IIocRegister, IIocResolver, IDisposable
    {
        /// <summary>
        ///     Reference to the Castle Windsor Container.
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        ///     Checks whether given type is registered before.
        /// </summary>
        /// <param name="type">Type to check</param>
        new bool IsRegistered(Type type);

        /// <summary>
        ///     Checks whether given type is registered before.
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        new bool IsRegistered<T>();

        /// <summary>
        ///     Checks whether given type is registered before using a named key.
        /// </summary>
        /// <param name="key">The named key for the instance of implementation</param>
        new bool IsRegistered(string key);

        /// <summary>
        ///     Adds a dependency registrar for conventional registration.
        /// </summary>
        /// <param name="registrar">dependency registrar</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        ///     Registers types of given assembly by all conventional registrars. See
        ///     <see cref="IocManager.AddConventionalRegistrar" /> method.
        /// </summary>
        /// <param name="assembly">Assembly to register</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        ///     Registers types of given assembly by conventional registrar.
        /// </summary>
        /// <param name="assembly">Assembly to register</param>
        /// <param name="registrar">Dependency registrar</param>
        void RegisterAssemblyByConvention(Assembly assembly, IConventionalDependencyRegistrar registrar);
    }
}