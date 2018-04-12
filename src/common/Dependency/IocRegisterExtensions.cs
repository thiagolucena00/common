using System;

namespace common.Dependency
{
    /// <summary>
    ///     Extension methods for <see cref="IIocRegister" /> interface.
    /// </summary>
    public static class IocRegisterExtensions
    {
        /// <summary>
        ///     Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <typeparam name="T">Type of the class</typeparam>
        /// <param name="iocRegister">Register</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static bool RegisterIfNot<T>(this IIocRegister iocRegister,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class
        {
            if (iocRegister.IsRegistered<T>())
                return false;

            iocRegister.Register<T>(lifeStyle);
            return true;
        }

        /// <summary>
        ///     Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <param name="iocRegister">Registrar</param>
        /// <param name="type">Type of the class</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static bool RegisterIfNot(this IIocRegister iocRegister, Type type,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegister.IsRegistered(type))
                return false;

            iocRegister.Register(type, lifeStyle);
            return true;
        }

        /// <summary>
        ///     Registers a type with it's implementation if it's not registered before.
        /// </summary>
        /// <typeparam name="TType">Registering type</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType" /></typeparam>
        /// <param name="iocRegister">Registrar</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static bool RegisterIfNot<TType, TImpl>(this IIocRegister iocRegister,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            if (iocRegister.IsRegistered<TType>())
                return false;

            iocRegister.Register<TType, TImpl>(lifeStyle);
            return true;
        }


        /// <summary>
        ///     Registers a type with it's implementation if it's not registered before.
        /// </summary>
        /// <param name="iocRegister">Registrar</param>
        /// <param name="type">Type of the class</param>
        /// <param name="impl">The type that implements <paramref name="type" /></param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static bool RegisterIfNot(this IIocRegister iocRegister, Type type, Type impl,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegister.IsRegistered(type))
                return false;

            iocRegister.Register(type, impl, lifeStyle);
            return true;
        }
    }
}