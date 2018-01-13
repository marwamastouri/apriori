using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using Xunit;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class ConfidenceTests
    {
        [Theory]
        [InlineData(An.Beer, An.Burger, 0.5f)]
        [InlineData(An.Milk, An.Beer, 2f / 3f)]
        [InlineData(An.Burger, An.Onion, 0.75f)]
        public void ShouldCalculateConfidence(string item0, string item1, float expected)
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var result = transactions.Confidence(new HashSet<string> { item0 }, new HashSet<string> { item1 });
            result.Should().Be(expected);
        }
    }
}
