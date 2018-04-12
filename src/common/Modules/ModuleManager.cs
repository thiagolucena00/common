using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using common.Collections.Extensions;
using common.Configurations;
using common.Dependency;
using common.Exceptions;
using Castle.Core.Logging;

namespace common.Modules
{
    public class ModuleManager : IModuleManager
    {
        private readonly IIocManager _iocManager;
        private ModuleCollection _modules;

        public ModuleManager(IIocManager iocManager)
        {
            _iocManager = iocManager;
            Logger = NullLogger.Instance;
        }

        public NullLogger Logger { get; set; }

        public ModuleInfo StartupModule { get; private set; }
        public IReadOnlyList<ModuleInfo> Modules => _modules.ToImmutableList();

        public void Initialize(Type startupModule)
        {
            _modules = new ModuleCollection(startupModule);
            LoadAllModules();
        }

        public void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(module => module.Instance.Shutdown());

            Logger.Debug("Shutting down completed.");
        }

        private void LoadAllModules()
        {
            Logger.Debug("Loading modules...");

            var moduleTypes = new List<Type>();
            moduleTypes.AddIfNotContains(typeof(CoreModule));
            moduleTypes.AddRange(Module.FindDependedModuleTypesRecursivelyIncludingGivenModule(_modules.StartupModuleType).Distinct());

            Logger.Debug("Found " + moduleTypes.Count + " modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            _modules.EnsureCoreModuleToBeFirst();
            _modules.EnsureStartupModuleToBeLast();

            SetDependencies();
            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                foreach (var dependedModuleType in Module.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                        throw new InitializationException("Could not find a depended module " +
                                                          dependedModuleType.AssemblyQualifiedName + " for " +
                                                          moduleInfo.Type.AssemblyQualifiedName);

                    if (moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null)
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                }
            }
        }

        private void CreateModules(List<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                if (!(_iocManager.Resolve(moduleType) is Module module))
                    throw new InitializationException("This type is not a module: " + moduleType.AssemblyQualifiedName);

                module.IocManager = _iocManager;
                module.Configuration = _iocManager.Resolve<IStartupConfiguration>();

                var moduleInfo = new ModuleInfo(moduleType, module);

                _modules.Add(moduleInfo);
                if (moduleType == _modules.StartupModuleType)
                    StartupModule = moduleInfo;

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(List<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
                _iocManager.RegisterIfNot(moduleType);
        }
    }
}