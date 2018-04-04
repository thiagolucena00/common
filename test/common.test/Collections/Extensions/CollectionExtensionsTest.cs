using System;
using System.Collections.Generic;
using System.Text;
using common.Collections.Extensions;
using Shouldly;
using Xunit;

namespace common.test.Collections.Extensions
{
    public class CollectionExtensionsTest
    {
        [Fact]
        public void Should_Be_Empty_When_Instantiated()
        {
            var collection = new List<string>();
            collection.IsNullOrEmpty().ShouldBeTrue();
        }

        [Fact]
        public void Should_Not_Be_Empty_When_Assigned_Values()
        {
            var collection = new List<string>();
            collection.Add("A");
            collection.Add("B");

            collection.IsNullOrEmpty().ShouldBeFalse();
        }

        [Fact]
        public void Should_Allow_To_Add_If_Not_Contains()
        {
            var collection = new List<string>();
            collection.Add("A");
            collection.Add("B");

            collection.AddIfNotContains("C");

            collection.ShouldContain("C");
        }

        [Fact]
        public void Should_Not_Allow_To_Add_If_Not_Contains()
        {
            var collection = new List<string>();
            collection.Add("A");
            collection.Add("B");
            collection.Add("C");

            collection.AddIfNotContains("C");

            collection.Count.ShouldBe(3);
            collection.ShouldContain("C");
        }
    }
}
