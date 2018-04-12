using System;
using System.Collections.Generic;
using System.Text;

namespace common.Runtime.Sessions
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets current User Identification or null. It can be null if no user logged in.
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// User Identification of the impersonator. This is filled if a user is performing actions behalf of the <see cref="UserId"/>.
        /// </summary>
        long? ImpersonatorUserId { get; }

        /// <summary>
        /// Used to change <see cref="UserId"/> for a limited scope.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDisposable Use(long? userId);
    }
}
