using System.Collections.Generic;
using System.Text;
using common.Dependency;
using Castle.MicroKernel.Registration;
using Shouldly;
using Xunit;

namespace common.test.Dependency
{
    public class IocManagerTests : TestWithLocalIocManager
    {
        [Fact]
        public void Should_Get_Registered_Class()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register<IEmpty, EmptyOne>();
                //Assert
                LocalIocManager.Resolve<IEmpty>().GetType().ShouldBe(typeof(EmptyOne));
            }
        }
        [Fact]
        public void Should_Get_First_Registered_Class_If_Registered_Multiple_Class_For_Same_Interface()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register(typeof(IEmpty), typeof(EmptyOne));
                LocalIocManager.Register(typeof(IEmpty), typeof(EmptyTwo));

                //Assert
                LocalIocManager.Resolve<IEmpty>().GetType().ShouldBe(typeof(EmptyOne));
            }
        }
        [Fact]
        public void Should_Get_First_Class_Registered_With_Defined_Name()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register<IEmpty, EmptyOne>("emptyOneKey");
                //Assert
                LocalIocManager.Resolve<IEmpty>("emptyOneKey").GetType().ShouldBe(typeof(EmptyOne));
            }
        }
        [Fact]
        public void Should_Get_First_Class_Registered_With_Defined_Name_If_Registered_Multiple_Class_For_Same_Interface()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register<IEmpty, EmptyOne>("emptyOneKey");
                LocalIocManager.Register<IEmpty, EmptyTwo>("emptyTwoKey");
                //Assert
                LocalIocManager.Resolve("emptyTwoKey", typeof(IEmpty)).GetType().ShouldBe(typeof(EmptyTwo));
            }
        }

        [Fact]
        public void Should_Get_Class_Registered_With_Self_Registration()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register(typeof(EmptyOne));
                //Assert
                LocalIocManager.Resolve(typeof(EmptyOne)).GetType().ShouldBe(typeof(EmptyOne));
            }
        }
        [Fact]
        public void Should_Get_Class_Registered_Using_Named_Key_With_Self_Registration()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register<EmptyOne>("emptyOneKey");
                //Assert
                LocalIocManager.Resolve("emptyOneKey", typeof(IEmpty)).GetType().ShouldBe(typeof(EmptyOne));
            }
        }
        [Fact]
        public void Should_Get_True_When_asked_For_Instance_Registration()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register<EmptyOne>();
                //Assert
                LocalIocManager.IsRegistered<EmptyOne>().ShouldBe(true);
            }
        }
        [Fact]
        public void Should_Get_True_When_asked_For_Instance_Registration_Using_Named_Key()
        {
            using (LocalIocManager = new IocManager())
            {
                //Arrange           
                LocalIocManager.Register<EmptyOne>("emptyOneKey");
                //Assert
                LocalIocManager.IsRegistered("emptyOneKey").ShouldBe(true);
            }
        }

        [Fact]
        public void Should_Resolve_Generic_Types()
        {
            using (LocalIocManager = new IocManager())
            {
                LocalIocManager.Register<MyClass>();                
                LocalIocManager.Register(typeof(IEmpty<>), typeof(EmptyOne<>));

                var obj = LocalIocManager.Resolve<IEmpty<MyClass>>();

                obj.GenericArg.GetType().ShouldBe(typeof(MyClass));
            }                       
        }

        public class EmptyOne : IEmpty
        {
        }
        public class EmptyTwo : IEmpty
        {
        }
        public interface IEmpty
        {
        }
        public interface IEmpty<T> where T : class
        {
            T GenericArg { get; set; }
        }
        public class EmptyOne<T> : IEmpty<T> where T : class
        {
            public T GenericArg { get; set; }
        }
        public class MyClass
        {
        }
    }

}
