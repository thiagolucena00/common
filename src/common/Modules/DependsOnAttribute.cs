using System;

namespace common.Modules
{
    /// <inheritdoc />
    /// <summary>
    ///     Used to define dependencies of an application module to other modules.
    ///     It should be used for a class derived from <see cref="T:common.Modules.Module" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Used to define dependencies of an ABP module to other modules.
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }

        /// <summary>
        ///     Types of depended modules.
        /// </summary>
        public Type[] DependedModuleTypes { get; }
    }
}