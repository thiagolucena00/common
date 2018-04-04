using System;
using common.Dependency;

namespace common.test.Dependency
{
    public abstract class TestWithLocalIocManager : IDisposable
    {
        protected IIocManager LocalIocManager;

        protected TestWithLocalIocManager()
        {
            LocalIocManager = new IocManager();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LocalIocManager.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}