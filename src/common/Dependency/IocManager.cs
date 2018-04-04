using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using common.Dependency.Registrars;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace common.Dependency
{
    public class IocManager : IIocManager
    {
        /// <summary>
        ///     List of all registered conventional registrars.
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            IocContainer = new WindsorContainer();
            IocContainer.Register(Component.For<IocManager, IIocManager, IIocRegister, IIocResolver>()
                .UsingFactoryMethod(() => this));

            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();
        }

        public static IocManager Instance { get; }
        public IWindsorContainer IocContainer { get; }

        public void Dispose()
        {
            IocContainer.Dispose();
        }

        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        public void Register<TType>(string key, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>().Named(key), lifeStyle));
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        public void Register(Type type, string key, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type).Named(key), lifeStyle));
        }

        public void Register<TType, TImplementation>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class where TImplementation : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>().ImplementedBy<TImplementation>(), lifeStyle));
        }

        public void Register<TType, TImplementation>(string key,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class where TImplementation : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>().ImplementedBy<TImplementation>().Named(key),
                lifeStyle));
        }

        public void Register(Type type, Type implementation,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type).ImplementedBy(implementation), lifeStyle));
        }

        public void Register(Type type, Type implementation, string key,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type).ImplementedBy(implementation).Named(key),
                lifeStyle));
        }

        public TType Resolve<TType>()
        {
            return IocContainer.Resolve<TType>();
        }

        public TType Resolve<TType>(Type type)
        {
            return (TType) IocContainer.Resolve(type);
        }

        public TType Resolve<TType>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<TType>(argumentsAsAnonymousType);
        }

        public T Resolve<T>(string key, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(key, argumentsAsAnonymousType);
        }

        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        public object Resolve(string key, Type type)
        {
            return IocContainer.Resolve(key, type);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        public object Resolve(string key, Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(key, type, argumentsAsAnonymousType);
        }

        public TType[] ResolveAll<TType>()
        {
            return IocContainer.ResolveAll<TType>();
        }

        public TType[] ResolveAll<TType>(object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll<TType>(argumentsAsAnonymousType);
        }

        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll(type, argumentsAsAnonymousType).Cast<object>().ToArray();
        }

        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered(string key)
        {
            return IocContainer.Kernel.HasComponent(key);
        }

        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            foreach (var conventionalDependencyRegistrar in _conventionalRegistrars)
                RegisterAssemblyByConvention(assembly, conventionalDependencyRegistrar);
        }

        public void RegisterAssemblyByConvention(Assembly assembly, IConventionalDependencyRegistrar registrar)
        {
            registrar.RegisterAssembly(new ConventionalRegistrationContext(assembly, this));
        }

        public bool IsRegistered<T>()
        {
            return IocContainer.Kernel.HasComponent(typeof(T));
        }

        public TType Resolve<TType>(string key, Type type)
        {
            return (TType) IocContainer.Resolve(key, type);
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration,
            DependencyLifeStyle lifeStyle) where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}