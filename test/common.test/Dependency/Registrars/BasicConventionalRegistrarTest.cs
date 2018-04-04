using System;
using System.Collections.Generic;
using System.Text;
using common.Dependency;
using common.Dependency.Registrars;
using common.test.Bootstrappers;
using Castle.DynamicProxy;
using Shouldly;
using Xunit;

namespace common.test.Dependency.Registrars
{
    public class BasicConventionalRegistrarTest : TestWithLocalIocManager
    {
        [Fact]
        public void Should_Get_Different_Objects_For_Transients_Dependency_When_Registred_By_Basic_Conventional_Registrar()
        {
            //Arrange
            LocalIocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            //Act
            LocalIocManager.RegisterAssemblyByConvention(typeof(TestTransientDependencyObject).Assembly);

            var transientDependencyObject1 = LocalIocManager.Resolve<TestTransientDependencyObject>();
            var transientDependencyObject2 = LocalIocManager.Resolve<TestTransientDependencyObject>();

            //Assert
            transientDependencyObject1.ShouldNotBeNull();
            transientDependencyObject2.ShouldNotBeNull();

            transientDependencyObject1.ShouldNotBeSameAs(transientDependencyObject2);
        }

        [Fact]
        public void Should_Get_Same_Objects_For_Singleton_Dependency_When_Registred_By_Basic_Conventional_Registrar()
        {
            //Arrange
            LocalIocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            //Act
            LocalIocManager.RegisterAssemblyByConvention(typeof(TestSingletonDependencyObject).Assembly);

            var transientDependencyObject1 = LocalIocManager.Resolve<TestSingletonDependencyObject>();
            var transientDependencyObject2 = LocalIocManager.Resolve<TestSingletonDependencyObject>();

            //Assert
            transientDependencyObject1.ShouldNotBeNull();
            transientDependencyObject2.ShouldNotBeNull();

            transientDependencyObject1.ShouldBeSameAs(transientDependencyObject2);
        }

        [Fact]
        public void Should_Get_Different_Objects_For_Transients_Dependency_When_Registred_With_Registrar_Passing_By_Parameter()
        {
            //Arrange
            //Act
            LocalIocManager.RegisterAssemblyByConvention(typeof(TestTransientDependencyObject).Assembly, new BasicConventionalRegistrar());

            var transientDependencyObject1 = LocalIocManager.Resolve<TestTransientDependencyObject>();
            var transientDependencyObject2 = LocalIocManager.Resolve<TestTransientDependencyObject>();

            //Assert
            transientDependencyObject1.ShouldNotBeNull();
            transientDependencyObject2.ShouldNotBeNull();

            transientDependencyObject1.ShouldNotBeSameAs(transientDependencyObject2);
        }

        private class TestTransientDependencyObject : ITransientDependency
        {
        }
        private class TestSingletonDependencyObject : ISingletonDependency
        {
        }
    }
}
