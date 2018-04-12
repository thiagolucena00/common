using System;
using common.Dependency;

namespace common.Runtime.Sessions
{
    public abstract class SessionBase : ISession
    {
        public abstract long? UserId { get; }
        public abstract long? ImpersonatorUserId { get; }
        public IDisposable Use(long? userId)
        {
            throw new NotImplementedException();
        }
    }
}