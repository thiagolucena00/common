using System;
using common.Dependency;

namespace common.Configuration
{
    /// <summary>
    ///     Used to configure application and modules on startup.
    /// </summary>
    public interface IStartupConfiguration
    {
        /// <summary>
        ///     Gets the IOC manager associated with this configuration.
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        ///     Used to replace a service type.
        ///     Given <see cref="replaceAction" /> should register an implementation for the <see cref="type" />.
        /// </summary>
        /// <param name="type">The type to be replaced.</param>
        /// <param name="replaceAction">Replace action.</param>
        void ReplaceService(Type type, Action replaceAction);
    }
}