using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Apriori.Tests.SetExtensionsTests
{
    public sealed class ItemSupportTests
    {
        [Fact]
        public void ShouldFormatMultipleItems()
        {
            var set = new ItemSupport
            {
                Item = new HashSet<string> { ">50K", "Master" },
                Support = 3851,
                Frequency = 0.1f
            };
            set.ToString().Should().Be("3851:>50K, Master");
        }

        [Fact]
        public void ShouldFormatOneItems()
        {
            var set = new ItemSupport
            {
                Item = new HashSet<string> { ">50K" },
                Support = 3851,
                Frequency = 0.1f
            };
            
            set.ToString().Should().Be("3851:>50K");
        }
    }
}