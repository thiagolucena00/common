using System;

namespace common.Dependency
{
    public interface IIocRegister
    {
        /// <summary>
        ///     Registers a type as self registration.
        /// </summary>
        /// <typeparam name="T">Type of the class</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class;

        /// <summary>
        ///     Registers a type as self registration using a named key.
        /// </summary>
        /// <typeparam name="T">Type of the class</typeparam>
        /// <param name="key">The named key for the instance</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register<T>(string key, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class;

        /// <summary>
        ///     Registers a type as self registration.
        /// </summary>
        /// <param name="type">Type of the class</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        ///     Registers a type as self registration using a named key.
        /// </summary>
        /// <param name="type">Type of the class</param>
        /// <param name="key">The named key for the instance</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register(Type type, string key, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        ///     Registers a type with it's implementation.
        /// </summary>
        /// <typeparam name="TType">Registering type</typeparam>
        /// <typeparam name="TImplementation">The type that implements <see cref="TType" /></typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register<TType, TImplementation>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImplementation : class, TType;

        /// <summary>
        ///     Registers a type with it's implementation using a named key.
        /// </summary>
        /// <typeparam name="TType">Registering type</typeparam>
        /// <typeparam name="TImplementation">The type that implements <see cref="TType" /></typeparam>
        /// <param name="key">The named key for the instance of implementation</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register<TType, TImplementation>(string key, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImplementation : class, TType;

        /// <summary>
        ///     Registers a type with it's implementation.
        /// </summary>
        /// <param name="type">Type of the class</param>
        /// <param name="implementation">The type that implements <paramref name="type" /></param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register(Type type, Type implementation, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        ///     Registers a type with it's implementation using a named key.
        /// </summary>
        /// <param name="type">Type of the class</param>
        /// <param name="implementation">The type that implements <paramref name="type" /></param>
        /// <param name="key">The named key for the instance</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register(Type type, Type implementation, string key,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        ///     Checks whether given type is registered before.
        /// </summary>
        /// <param name="type">Type to check</param>
        bool IsRegistered(Type type);

        /// <summary>
        ///     Checks whether given type is registered before using a named key.
        /// </summary>
        /// <param name="key">The named key for the instance of implementation</param>
        bool IsRegistered(string key);

        /// <summary>
        ///     Checks whether given type is registered before.
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        bool IsRegistered<T>();
    }
}