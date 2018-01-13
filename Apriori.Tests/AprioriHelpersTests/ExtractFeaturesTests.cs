using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class ExtractFeaturesTests
    {
        private ITestOutputHelper Console { get; }

        public ExtractFeaturesTests(ITestOutputHelper console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console));
        }

        [Fact]
        public void ShouldExtractFeatures()
        {
            var transactions = new TestTransactionsLoader().Load();
            var features = transactions.ExtractFeatures().ToImmutableArray();
            features.Select(set => set.Format()).ForEach(line => Console.WriteLine(line));
            features.Count().Should().Be(5);
        }
    }
}