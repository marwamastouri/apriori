using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using Xunit;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class FrequencyTests
    {
        [Theory]
        [InlineData(An.Onion, 2f / 3f)]
        [InlineData(An.Potato, 5f / 6f)]
        [InlineData(An.Burger, 2f / 3f)]
        [InlineData(An.Milk, 2f / 3f)]
        [InlineData(An.Beer, 0.5f)]
        public void ShouldReturnProbabilityOfSingleItem(string item, float expected)
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var frequency = transactions.Frequency(new HashSet<string> { item });
            frequency.Should().Be(expected);
        }

        [Theory]
        [InlineData(An.Onion, An.Potato, 2f / 3f)]
        [InlineData(An.Potato, An.Burger, 2f / 3f)]
        [InlineData(An.Burger, An.Milk, 1f / 3f)]
        [InlineData(An.Milk, An.Beer, 1f / 3f)]
        [InlineData(An.Beer, An.Onion, 1f / 3f)]
        public void ShouldReturnProbabilityOfTwoItems(string item0, string item1, float expected)
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var frequency = transactions.Frequency(new HashSet<string> { item0, item1 });
            frequency.Should().Be(expected);
        }
    }
}
