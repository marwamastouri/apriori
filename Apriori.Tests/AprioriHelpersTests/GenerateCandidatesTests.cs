using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class GenerateCandidatesTests
    {
        private ITestOutputHelper Console { get; }

        public GenerateCandidatesTests(ITestOutputHelper console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console));
        }

        [Fact]
        public void ShouldGenerateCandidates()
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var features = transactions.ExtractFeatures();
            
            var result = transactions
                .GenerateCandidates(features, checkConfidence: false);

            result.Select(set => set.Format()).ForEach(line => Console.WriteLine(line));
            result.Count().Should().Be(10);
        }

        [Fact]
        public void ShouldFilterByConfidence()
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var features = transactions.ExtractFeatures();
            
            var result = transactions
                .GenerateCandidates(features, minConfidence: 1f);

            result.Select(set => set.Format()).ForEach(line => Console.WriteLine(line));
            result.Count().Should().Be(2);
        }
    }
}