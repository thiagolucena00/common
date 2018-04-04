using System;
using System.Collections.Generic;
using System.Text;
using common.Dependency;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Shouldly;
using Xunit;

namespace common.test.Dependency
{
    public class IocManagerPropertyInjectionTests: TestWithLocalIocManager
    {
        [Fact]
        public void Should_Inject_TestClass_For_ApplicationService()
        {
            var empty = Substitute.For<EmptyClass>();
            empty.Count = 1;
            
            LocalIocManager.IocContainer.Register(Component.For<EmptyClass>().Instance(empty));
            LocalIocManager.Register<IEmptyService, EmptyService>(DependencyLifeStyle.Transient);
            
            LocalIocManager.Resolve<IEmptyService>().Empty.ShouldNotBeNull();
            LocalIocManager.Resolve<IEmptyService>().Empty.Count.ShouldBe(1);
        }

        public class EmptyClass
        {
            public int Count { get; set; }
        }

        public interface IEmptyService
        {
            EmptyClass Empty { get; set; }
        }
        public class EmptyService: IEmptyService
        {
            public EmptyClass Empty { get; set; }
        }
    }
}
