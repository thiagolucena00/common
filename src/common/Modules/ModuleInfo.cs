using System;
using System.Collections.Generic;

namespace common.Modules
{
    public class ModuleInfo
    {
        public ModuleInfo(Type type, Module instance)
        {
            Type = type;
            Instance = instance;

            Dependencies = new List<ModuleInfo>();
        }

        /// <summary>
        ///     All dependent modules of this module.
        /// </summary>
        public List<ModuleInfo> Dependencies { get; }

        /// <summary>
        ///     Type of this module.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        ///     Instance of the module.
        /// </summary>
        public Module Instance { get; set; }
    }
}