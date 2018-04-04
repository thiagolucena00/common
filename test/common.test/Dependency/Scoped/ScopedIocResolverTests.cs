using System;
using System.Collections.Generic;
using System.Text;
using common.Dependency;
using Shouldly;
using Xunit;

namespace common.test.Dependency.Scoped
{
    public class ScopedIocResolverTests : TestWithLocalIocManager
    {
        private const string Key1 = "disposable1";
        private const string Key2 = "disposable2";
        private const string Key3 = "disposable3";

        [Fact]
        public void UsingScope_Test_ShouldWork()
        {
            LocalIocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);
            TestSimpleDisposableObject testSimpleObj = null;

            LocalIocManager.UsingScope(scope => { testSimpleObj = scope.Resolve<TestSimpleDisposableObject>(); });

            testSimpleObj.DisposeCount.ShouldBe(1);
        }

        [Fact]
        public void UsingScope_Test_With_Constructor_ShouldWork()
        {
            LocalIocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);

            TestSimpleDisposableObject testSimpleObj = null;

            LocalIocManager.UsingScope(scope => { testSimpleObj = scope.Resolve<TestSimpleDisposableObject>(new { myData = 40 }); });

            testSimpleObj.MyData.ShouldBe(40);
        }

        [Fact]
        public void IIocScopedResolver_Test_ShouldWork()
        {
            LocalIocManager.Register<TestSimpleDisposableObject>(Key1, DependencyLifeStyle.Transient);
            LocalIocManager.Register<TestSimpleDisposableObject>(Key2, DependencyLifeStyle.Transient);
            LocalIocManager.Register<TestSimpleDisposableObject>(Key3, DependencyLifeStyle.Transient);

            TestSimpleDisposableObject testSimpleObj;
            TestSimpleDisposableObject testSimpleObj2;
            TestSimpleDisposableObject testSimpleObj3;

            using (var scope = LocalIocManager.CreateScope())
            {
                testSimpleObj =  scope.Resolve<TestSimpleDisposableObject>(Key1);
                testSimpleObj2 = scope.Resolve<TestSimpleDisposableObject>(Key2);
                testSimpleObj3 = scope.Resolve<TestSimpleDisposableObject>(Key3);
            }

            testSimpleObj.DisposeCount.ShouldBe(1);
            testSimpleObj2.DisposeCount.ShouldBe(1);
            testSimpleObj3.DisposeCount.ShouldBe(1);
        }

        [Fact]
        public void IIocScopedResolver_Test_With_ConstructorArgs_ShouldWork()
        {
            LocalIocManager.Register<TestSimpleDisposableObject>(Key1, DependencyLifeStyle.Transient);
            LocalIocManager.Register<TestSimpleDisposableObject>(Key2, DependencyLifeStyle.Transient);
            LocalIocManager.Register<TestSimpleDisposableObject>(Key3, DependencyLifeStyle.Transient);

            TestSimpleDisposableObject testSimpleObj;
            TestSimpleDisposableObject testSimpleObj2;
            TestSimpleDisposableObject testSimpleObj3;

            using (var scope = LocalIocManager.CreateScope())
            {
                testSimpleObj = scope.Resolve<TestSimpleDisposableObject>(Key1, new { myData = 40 });
                testSimpleObj2 = scope.Resolve<TestSimpleDisposableObject>(Key2, new { myData = 4040 });
                testSimpleObj3 = scope.Resolve<TestSimpleDisposableObject>(Key3, new { myData = 404040 });
            }

            testSimpleObj.MyData.ShouldBe(40);
            testSimpleObj2.MyData.ShouldBe(4040);
            testSimpleObj3.MyData.ShouldBe(404040);
        }

        [Fact]
        public void IIocScopedResolver_Test_ResolveAll_Should_DisposeAll_Registrants()
        {
            LocalIocManager.Register<ISimpleDependency, SimpleDependency>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency2>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency3>(DependencyLifeStyle.Transient);

            IEnumerable<ISimpleDependency> simpleDependendcies;

            using (var scope = LocalIocManager.CreateScope())
            {
                simpleDependendcies = scope.ResolveAll<ISimpleDependency>();
            }

            simpleDependendcies.ShouldAllBe(d => d.DisposeCount == 1);
        }

        [Fact]
        public void IIocScopedResolver_Test_ResolveAll_Should_Work_WithConstructor()
        {
            LocalIocManager.Register<ISimpleDependency, SimpleDependency>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency2>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency3>(DependencyLifeStyle.Transient);

            IEnumerable<ISimpleDependency> simpleDependendcies;

            using (var scope = LocalIocManager.CreateScope())
            {
                simpleDependendcies = scope.ResolveAll<ISimpleDependency>(new { myData = 40 });
            }

            simpleDependendcies.ShouldAllBe(x => x.MyData == 40);
        }

        [Fact]
        public void IIocScopedResolver_Test_ResolveAll_Should_Work_With_OtherResolvings()
        {
            LocalIocManager.Register<ISimpleDependency, SimpleDependency>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency2>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency3>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);

            IEnumerable<ISimpleDependency> simpleDependendcies;
            TestSimpleDisposableObject testSimpleObject;

            using (var scope = LocalIocManager.CreateScope())
            {
                simpleDependendcies = scope.ResolveAll<ISimpleDependency>();
                testSimpleObject = scope.Resolve<TestSimpleDisposableObject>();
            }

            simpleDependendcies.ShouldAllBe(x => x.DisposeCount == 1);
            testSimpleObject.DisposeCount.ShouldBe(1);
        }

        [Fact]
        public void IIocScopedResolver_Test_ResolveAll_Should_Work_With_OtherResolvings_ConstructorArguments()
        {
            LocalIocManager.Register<ISimpleDependency, SimpleDependency>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency2>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISimpleDependency, SimpleDependency3>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<TestSimpleDisposableObject>(DependencyLifeStyle.Transient);

            IEnumerable<ISimpleDependency> simpleDependendcies;
            TestSimpleDisposableObject testSimpleObject;

            using (var scope = LocalIocManager.CreateScope())
            {
                simpleDependendcies = scope.ResolveAll<ISimpleDependency>(new { myData = 40 });
                testSimpleObject = scope.Resolve<TestSimpleDisposableObject>(new { myData = 40 });
            }

            simpleDependendcies.ShouldAllBe(x => x.MyData == 40);
            testSimpleObject.MyData.ShouldBe(40);
        }

        [Fact]
        public void IIocScopedResolver_Test_IsRegistered_ShouldWork()
        {
            LocalIocManager.Register<ISimpleDependency, SimpleDependency>(DependencyLifeStyle.Transient);

            using (var scope = LocalIocManager.CreateScope())
            {
                scope.IsRegistered<ISimpleDependency>().ShouldBe(true);
                scope.IsRegistered(typeof(ISimpleDependency)).ShouldBe(true);
            }
        }

        [Fact]
        public void IIocScopedResolver_Test_Custom_Release_ShouldWork()
        {
            LocalIocManager.Register<ISimpleDependency, SimpleDependency>(DependencyLifeStyle.Transient);

            ISimpleDependency simpleDependency;

            using (var scope = LocalIocManager.CreateScope())
            {
                simpleDependency = scope.Resolve<ISimpleDependency>();
                scope.Release(simpleDependency);
            }

            simpleDependency.DisposeCount.ShouldBe(1);
        }
        public interface ISimpleDependency : IDisposable
        {
            int MyData { get; set; }
            int DisposeCount { get; set; }
        }

        public class SimpleDependency : ISimpleDependency
        {
            public int MyData { get; set; }

            public int DisposeCount { get; set; }

            public void Dispose()
            {
                DisposeCount++;
            }
        }

        public class SimpleDependency2 : ISimpleDependency
        {
            public int DisposeCount { get; set; }

            public int MyData { get; set; }

            public void Dispose()
            {
                DisposeCount++;
            }
        }

        public class SimpleDependency3 : ISimpleDependency
        {
            public int MyData { get; set; }

            public int DisposeCount { get; set; }

            public void Dispose()
            {
                DisposeCount++;
            }
        }
    }
}
