using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class FilterByFrequencyTests
    {
        private ITestOutputHelper Console { get; }

        public FilterByFrequencyTests(ITestOutputHelper console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console));
        }

        [Theory]
        [InlineData(1f, 0)]
        [InlineData(0.5f, 4)]
        [InlineData(0.1f, 5)]
        [InlineData(0f, 5)]
        public void ShouldFilterByFrequency(float minFrequency, int expected)
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var candidates = transactions.ExtractFeatures();
            var result = candidates.FilterByFrequency(transactions, minFrequency);
            result.Select(set => SetExtensions.Format(set)).ForEach(line => Console.WriteLine(line));
            result.Count().Should().Be(expected);
        }
    }
}