using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using common.Exceptions;
using common.Modules;
using Shouldly;
using Xunit;

namespace common.test.Modules
{
    public class ModuleTests
    {

        [Fact]
        public void Should_Get_True_When_Class_Is_A_Module()
        {
            Module.IsModule(typeof(TestTwoModule)).ShouldBeTrue();
        }

        [Fact]
        public void Shoul_Get_Modules_When_Asked_For_Find_Dependeded_Module_Type()
        {
            List<Type> types = Module.FindDependedModuleTypes(typeof(TestDependencyModule));

            types.Any(t => t == typeof(TestOneModule)).ShouldBeTrue();
            types.Any(t => t == typeof(TestTwoModule)).ShouldBeTrue();
        }

        [Fact]
        public void Shoul_Get_Empty_List_When_Asked_For_Find_Non_Dependeded_Module_Type()
        {
            List<Type> types = Module.FindDependedModuleTypes(typeof(TestOneModule));

            types.ShouldNotBeNull();
            types.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Throw_Exception_When_Asked_For_Find_Dependeded_Module_With_Non_Module_Type()
        {
            Should.Throw<InitializationException>(() => Module.FindDependedModuleTypes(typeof(object)));
        }

        [Fact]
        public void Shoul_Get_Modules_Recursively_Including_Given_Module_When_Asked_For_Find_Dependeded_Module_Type()
        {
            List<Type> types = Module.FindDependedModuleTypesRecursivelyIncludingGivenModule(typeof(TestSubDependencyModule));

            types.ShouldNotBeNull();
            types.Any(t => t == typeof(TestDependencyModule)).ShouldBeTrue();
            types.Any(t => t == typeof(TestOneModule)).ShouldBeTrue();
            types.Any(t => t == typeof(TestTwoModule)).ShouldBeTrue();
        }



        public class TestOneModule : Module { }
        public class TestTwoModule : Module { }

        [DependsOn(typeof(TestOneModule), typeof(TestTwoModule))]
        public class TestDependencyModule : Module { }

        [DependsOn(typeof(TestDependencyModule))]
        public class TestSubDependencyModule : Module { }
    }
}
