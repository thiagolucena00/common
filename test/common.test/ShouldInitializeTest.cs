using System;
using common.Dependency;
using Shouldly;
using Xunit;

namespace common.test
{
    public class ShouldInitializeTest
    {
        [Fact]
        public void Should_Call_Initialize()
        {
            using (var iocManager = new IocManager())
            {
                iocManager.Register<InitializeClass>();

                var initializeClass = iocManager.Resolve<InitializeClass>();
                
                initializeClass.ShouldNotBeNull();
                initializeClass.Initialization.ShouldBe(1);
            }
        }

        public class InitializeClass : IShouldInitialize
        {
            public int Initialization { get; set; }
            public void Initialize()
            {
                Initialization++;
            }
        }
    }
}
