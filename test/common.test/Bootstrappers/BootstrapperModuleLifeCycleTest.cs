using System.Collections.Generic;
using System.Linq;
using common.Bootstrappers;
using common.Modules;
using common.test.Dependency;
using Shouldly;
using Xunit;

namespace common.test.Bootstrappers
{
    public class BootstrapperModuleLifeCycleTest : TestWithLocalIocManager
    {
        [Fact]
        public void Should_Call_The_Module_Initialize_Lifecycle_Methods_When_Bootstrapper_Are_Initialized()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<TestDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();
            //Act
            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;

            //Assert 
            var testDependencyModuleInfo = modules.FirstOrDefault(m => m.Type == typeof(TestDependencyModule));
            testDependencyModuleInfo.ShouldNotBeNull();

            var instance = testDependencyModuleInfo.Instance as TestDependencyModule;
            instance.ShouldNotBeNull();

            instance.PreInitializeCount.ShouldBe(1);
            instance.InitializeCount.ShouldBe(1);
            instance.PostInitializeCount.ShouldBe(1);
        }
        [Fact]
        public void Should_Call_The_Module_Shutdown_When_Bootstrapper_Are_Disposed()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<TestDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();
            //Act
            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;
            bootstrapper.Dispose();

            //Assert 
            var testDependencyModuleInfo = modules.FirstOrDefault(m => m.Type == typeof(TestDependencyModule));
            testDependencyModuleInfo.ShouldNotBeNull();

            var instance = testDependencyModuleInfo.Instance as TestDependencyModule;
            instance.ShouldNotBeNull();

            instance.PreInitializeCount.ShouldBe(1);
            instance.InitializeCount.ShouldBe(1);
            instance.PostInitializeCount.ShouldBe(1);
        }
        [Fact]
        public void Should_Calls_Initialize_Methods_Ordered_By_Dependecies()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<TestDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });

            //Act
            bootstrapper.Initialize();

            //Assert            
            Counter.InitializeCalls.ElementAtOrDefault(0).ShouldBe(nameof(Test1Module));
            Counter.InitializeCalls.ElementAtOrDefault(1).ShouldBe(nameof(Test2Module));
            Counter.InitializeCalls.ElementAtOrDefault(2).ShouldBe(nameof(TestDependencyModule));
        }

        [Fact]
        public void Should_Calls_Shutdown_Methods_Ordered_By_Dependecies_When_Bootstrapper_Are_Disposed()
        {
            //Arrange
            var bootstrapper = Bootstrapper.Create<TestDependencyModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });

            //Act
            bootstrapper.Initialize();
            bootstrapper.Dispose();
            //Assert            
            Counter.ShutdownCalls.ElementAtOrDefault(0).ShouldBe(nameof(TestDependencyModule));
            Counter.ShutdownCalls.ElementAtOrDefault(1).ShouldBe(nameof(Test2Module));
            Counter.ShutdownCalls.ElementAtOrDefault(2).ShouldBe(nameof(Test1Module));
        }

        public static class Counter
        {
            static Counter()
            {
                InitializeCalls = new List<string>();
                ShutdownCalls = new List<string>();
            }
            public static List<string> InitializeCalls { get; set; }
            public static List<string> ShutdownCalls { get; set; }
        }

        public class Test1Module : Module
        {
            public override void Initialize()
            {
                Counter.InitializeCalls.Add(nameof(Test1Module));
            }
            public override void Shutdown()
            {
                Counter.ShutdownCalls.Add(nameof(Test1Module));
            }

        }
        public class Test2Module : Module
        {
            public override void Initialize()
            {
                Counter.InitializeCalls.Add(nameof(Test2Module));
            }
            public override void Shutdown()
            {
                Counter.ShutdownCalls.Add(nameof(Test2Module));
            }

        }

        [DependsOn(typeof(Test1Module), typeof(Test2Module))]
        public class TestDependencyModule : Module
        {
            public int PreInitializeCount { get; set; }
            public int InitializeCount { get; set; }
            public int PostInitializeCount { get; set; }
            public int ShutdownCount { get; set; }

            public override void PreInitialize()
            {
                PreInitializeCount++;

            }
            public override void Initialize()
            {
                InitializeCount++;
                Counter.InitializeCalls.Add(nameof(TestDependencyModule));
            }
            public override void PostInitialize()
            {
                PostInitializeCount++;
            }
            public override void Shutdown()
            {
                ShutdownCount++;
                Counter.ShutdownCalls.Add(nameof(TestDependencyModule));
            }
        }
    }
}