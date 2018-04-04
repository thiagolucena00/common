using System;
using System.Collections.Generic;

namespace common.Modules
{
    public interface IModuleManager
    {
        ModuleInfo StartupModule { get; }

        IReadOnlyList<ModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}