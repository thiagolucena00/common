using System;

namespace common.Dependency.Scoped
{
    /// <inheritdoc cref="IIocResolver" />
    /// <summary>
    ///     This interface is used to wrap a scope for batch resolvings in a single <c>using</c> statement.
    ///     It inherits <see cref="T:System.IDisposable" /> and <see cref="T:common.Dependency.IIocResolver" />, so resolved
    ///     objects can be easily and batch
    ///     manner released by IocResolver.
    ///     In <see cref="M:System.IDisposable.Dispose" /> method,
    ///     <see cref="M:common.Dependency.IIocResolver.Release(System.Object)" /> is called to dispose the object.
    /// </summary>
    public interface IScopedIocResolver : IIocResolver, IDisposable
    {
    }
}