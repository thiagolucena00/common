using System;
using common.Dependency.Scoped;

namespace common.Dependency
{
    /// <summary>
    ///     Extension methods to <see cref="IIocResolver" /> interface.
    /// </summary>
    public static class IocResolverExtensions
    {
        /// <summary>
        ///     Gets a <see cref="ScopedIocResolver" /> object that starts a scope to resolved objects to be Disposable.
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <returns>The instance object wrapped by <see cref="ScopedIocResolver" /></returns>
        public static IScopedIocResolver CreateScope(this IIocResolver iocResolver)
        {
            return new ScopedIocResolver(iocResolver);
        }

        /// <summary>
        ///     This method starts a scope to resolve and release all objects automatically.
        ///     You can use the <c>scope</c> in <see cref="action" />.
        /// </summary>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="action">An action that can use the resolved object</param>
        public static void UsingScope(this IIocResolver iocResolver, Action<IScopedIocResolver> action)
        {
            using (var scope = iocResolver.CreateScope())
            {
                action(scope);
            }
        }
    }
}