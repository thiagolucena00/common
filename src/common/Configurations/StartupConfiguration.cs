using System;
using System.Collections.Generic;
using common.Dependency;

namespace common.Configurations
{
    public class StartupConfiguration : IStartupConfiguration, IShouldInitialize
    {
        public StartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public Dictionary<Type, Action> ServiceReplaceActions { get; set; }

        public void Initialize()
        {
            ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public IIocManager IocManager { get; }
    }
}