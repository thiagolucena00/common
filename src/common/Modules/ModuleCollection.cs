using System;
using System.Collections.Generic;
using common.Collections.Extensions;

namespace common.Modules
{
    /// <summary>
    ///     Used to store ModuleInfo objects as a dictionary.
    /// </summary>
    internal class ModuleCollection : List<ModuleInfo>
    {
        public ModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        public Type StartupModuleType { get; }

        /// <summary>
        ///     Sorts modules according to dependencies.
        ///     If module A depends on module B, A comes after B in the returned List.
        /// </summary>
        /// <returns>Sorted list</returns>
        public List<ModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureCoreModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, StartupModuleType);
            return sortedModules;
        }

        public static void EnsureCoreModuleToBeFirst(List<ModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(CoreModule));
            if (kernelModuleIndex <= 0)
                return;

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        public static void EnsureStartupModuleToBeLast(List<ModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
                return;

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        internal void EnsureCoreModuleToBeFirst()
        {
            EnsureCoreModuleToBeFirst(this);
        }

        internal void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }
    }
}