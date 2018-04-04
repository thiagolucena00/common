using System.Collections.Generic;
using System.Text;
using common.Dependency;
using Shouldly;
using Xunit;

namespace common.test.Dependency
{
    public class IocManagerLifeStyleTests : TestWithLocalIocManager
    {
        [Fact]
        public void Should_Call_Dispose_Of_Transient_Dependency_When_Object_Is_Released()
        {           
            LocalIocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);
            var obj = LocalIocManager.Resolve<TestSimpleDisposableObject>();
            
            LocalIocManager.Release(obj);
            
            obj.DisposeCount.ShouldBe(1);
        }

        [Fact]
        public void Should_Call_Dispose_Of_Transient_Dependency_When_IocManager_Is_Disposed()
        {
            LocalIocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);
            var obj = LocalIocManager.IocContainer.Resolve<TestSimpleDisposableObject>();

            LocalIocManager.Dispose();

            obj.DisposeCount.ShouldBe(1);
        }

        [Fact]
        public void Should_Call_Dispose_Of_Singleton_Dependency_When_IocManager_Is_Disposed()
        {
            LocalIocManager.Register<TestSimpleDisposableObject>();
            var obj = LocalIocManager.IocContainer.Resolve<TestSimpleDisposableObject>();

            LocalIocManager.Dispose();

            obj.DisposeCount.ShouldBe(1);
        }

        [Fact]
        public void Should_Get_Different_Objects_For_Transients()
        {
            using (var iocManager = new IocManager())
            {
                iocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);

                var obj1 = iocManager.Resolve<TestSimpleDisposableObject>();
                var obj2 = iocManager.Resolve<TestSimpleDisposableObject>();

                obj1.ShouldNotBeSameAs(obj2);
            }
        }
        [Fact]
        public void Should_Get_Same_Object_For_Singleton()
        {
            using (var iocManager = new IocManager())
            {
                iocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Singleton);

                var obj1 = iocManager.Resolve<TestSimpleDisposableObject>();
                var obj2 = iocManager.Resolve<TestSimpleDisposableObject>();

                obj1.ShouldBeSameAs(obj2);
            }
        }
    }
}
