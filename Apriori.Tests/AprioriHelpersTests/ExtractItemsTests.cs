using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class ExtractItemsTests
    {
        private ITestOutputHelper Console { get; }

        public ExtractItemsTests(ITestOutputHelper console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console));
        }

        [Fact]
        public void ShouldGenerateCandidates()
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var result = transactions.ExtractItems();
            Console.WriteLine(result.Format());
            result.Count().Should().Be(5);
        }
    }
}