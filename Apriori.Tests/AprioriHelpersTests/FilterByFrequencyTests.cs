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
        [InlineData(0.5f, 5)]
        [InlineData(0.1f, 5)]
        [InlineData(0f, 5)]
        public void ShouldFilterByFrequency(float minFrequency, int expected)
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var features = transactions.ExtractFeatures();
            var table = transactions.CalculateFrequencyTable(features);
            var result = table.FilterByFrequency(minFrequency).ToImmutableArray();
            result.Select(set => set.Format()).ForEach(line => Console.WriteLine(line));
            result.Count().Should().Be(expected);
        }
    }
}