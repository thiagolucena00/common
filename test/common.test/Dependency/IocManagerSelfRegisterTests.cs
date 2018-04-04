using System;
using System.Collections.Generic;
using System.Text;
using common.Dependency;
using Shouldly;
using Xunit;

namespace common.test.Dependency
{
    public class IocManagerSelfRegisterTests : TestWithLocalIocManager
    {

        [Fact]
        public void Should_Self_Register_With_All_Interfaces()
        {
            //Arrange
            var managerByInterface = LocalIocManager.Resolve<IIocManager>();
            var managerByClass = LocalIocManager.Resolve<IocManager>();

            //Assert
            LocalIocManager.Resolve<IIocRegister>().ShouldBeSameAs(managerByInterface);
            LocalIocManager.Resolve<IIocRegister>().ShouldBeSameAs(managerByClass);

            LocalIocManager.Resolve<IIocResolver>().ShouldBeSameAs(managerByInterface);
            LocalIocManager.Resolve<IIocResolver>().ShouldBeSameAs(managerByClass);

            managerByClass.ShouldBeSameAs(managerByInterface);
        }
    }
}
