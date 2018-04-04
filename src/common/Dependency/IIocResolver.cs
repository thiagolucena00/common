using System;

namespace common.Dependency
{
    public interface IIocResolver
    {
        /// <summary>
        ///     Gets an object from IOC container.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <returns>The object instance</returns>
        T Resolve<T>();

        /// <summary>
        ///     Gets an object from IOC container.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <typeparam name="T">Type of the object to cast</typeparam>
        /// <param name="type">Type of the object to resolve</param>
        /// <returns>The object instance</returns>
        T Resolve<T>(Type type);

        /// <summary>
        ///     Gets an object from IOC container.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        ///     Gets an object from IOC container using a named key.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="key">The named key for the instance of implementation</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        T Resolve<T>(string key, object argumentsAsAnonymousType);

        /// <summary>
        ///     Gets an object from IOC container.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <returns>The object instance</returns>
        object Resolve(Type type);

        /// <summary>
        ///     Gets an object from IOC container using a named key.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <param name="key">The named key for the instance of implementation</param>
        /// <param name="type">Type of the object to get</param>
        /// <returns>The object instance</returns>
        object Resolve(string key, Type type);

        /// <summary>
        ///     Gets an object from IOC container.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        ///     Gets an object from IOC container using a named key.
        ///     Returning object must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <param name="key">The named key for the instance of implementation</param>
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        object Resolve(string key, Type type, object argumentsAsAnonymousType);


        /// <summary>
        ///     Gets all implementations for given type.
        ///     Returning objects must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <typeparam name="T">Type of the objects to resolve</typeparam>
        /// <returns>Object instances</returns>
        T[] ResolveAll<T>();

        /// <summary>
        ///     Gets all implementations for given type.
        ///     Returning objects must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <typeparam name="T">Type of the objects to resolve</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>Object instances</returns>
        T[] ResolveAll<T>(object argumentsAsAnonymousType);

        /// <summary>
        ///     Gets all implementations for given type.
        ///     Returning objects must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <param name="type">Type of the objects to resolve</param>
        /// <returns>Object instances</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        ///     Gets all implementations for given type.
        ///     Returning objects must be Released (see <see cref="Release" />) after usage.
        /// </summary>
        /// <param name="type">Type of the objects to resolve</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>Object instances</returns>
        object[] ResolveAll(Type type, object argumentsAsAnonymousType);

        /// <summary>
        ///     Releases a pre-resolved object. See Resolve methods.
        /// </summary>
        /// <param name="obj">Object to be released</param>
        void Release(object obj);

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