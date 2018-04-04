using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using common.Bootstrappers;
using common.Modules;
using common.test.Dependency;
using common.test.Modules;
using Shouldly;
using Xunit;

namespace common.test.Bootstrappers
{
    public class BootstrapperTest : TestWithLocalIocManager
    {
        [Fact]
        public void Should_Load_All_Modules()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<WithSubDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();

            //Act
            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(5);

            modules.Any(m => m.Type == typeof(CoreModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(ModuleOne)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(ModuleTwo)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(WithDependencyModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(WithSubDependencyModule)).ShouldBeTrue();            
        }

        [Fact]
        public void Should_Load_Only_NoDependencyModule_And_CoreModule()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<NoDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();

            //Act
            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(2);

            modules.Any(m => m.Type == typeof(CoreModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(NoDependencyModule)).ShouldBeTrue();
        }

        [Fact]
        public void Should_Get_Startup_Module_As_Last_Module()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<WithDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();

            //Act
            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(4);

            modules.Any(m => m.Type == typeof(CoreModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(ModuleOne)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(ModuleTwo)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(WithDependencyModule)).ShouldBeTrue();

            var startupModule = modules.Last();

            startupModule.Type.ShouldBe(typeof(WithDependencyModule));
        }
        [Fact]
        public void Should_Get_Core_Module_As_First_Module()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<WithDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();

            //Act
            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(4);

            modules.Any(m => m.Type == typeof(CoreModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(ModuleOne)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(ModuleTwo)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(WithDependencyModule)).ShouldBeTrue();

            var startupModule = modules.First();

            startupModule.Type.ShouldBe(typeof(CoreModule));
        }

        

        public class ModuleOne : Module { }
        public class ModuleTwo : Module { }

        [DependsOn(typeof(ModuleOne), typeof(ModuleTwo))]
        public class WithDependencyModule : Module { }

        [DependsOn(typeof(WithDependencyModule), typeof(ModuleTwo))]
        public class WithSubDependencyModule : Module { }

        public class NoDependencyModule : Module { }
    }
}
