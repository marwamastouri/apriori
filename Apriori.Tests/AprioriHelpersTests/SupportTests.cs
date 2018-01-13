using System.Collections.Immutable;
using FluentAssertions;
using Xunit;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class SupportTests
    {
        [Theory]
        [InlineData(An.Onion, An.Potato, 4)]
        [InlineData(An.Onion, An.Burger, 3)]
        [InlineData(An.Onion, An.Milk, 2)]
        [InlineData(An.Potato, An.Burger, 4)]
        [InlineData(An.Potato, An.Milk, 3)]
        [InlineData(An.Burger, An.Milk, 2)]
        public void ShouldMatchExpectedSupport(string item0, string item1, int expected)
        {
            var transactions = new TestTransactionsLoader().Load();
            var result = transactions.Support(ImmutableHashSet.Create(item0, item1));
            result.Should().Be(expected);
        }
    }
}