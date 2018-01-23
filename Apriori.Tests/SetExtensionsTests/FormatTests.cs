using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Apriori.Tests.SetExtensionsTests
{
    public sealed class FormatTests
    {
        [Fact]
        public void ShouldFormatTwoItems()
        {
            var set = new HashSet<string> { "foo", "bar" };
            set.Format().Should().Be("{ foo, bar }");
        }

        [Fact]
        public void ShouldFormatOneItems()
        {
            var set = new HashSet<string> { "foo" };
            set.Format().Should().Be("{ foo }");
        }

        [Fact]
        public void ShouldFormatEmptySet()
        {
            var set = new HashSet<string>();
            set.Format().Should().Be("{  }");
        }
    }
}