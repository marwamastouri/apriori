using System;
using System.Collections.Immutable;
using Xunit;
using Xunit.Abstractions;

namespace Apriori.Tests.AprioriHelpersTests
{
    public sealed class CalculateFrequencyTableTests
    {
        private ITestOutputHelper Console { get; }

        public CalculateFrequencyTableTests(ITestOutputHelper console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console));
        }

        [Fact]
        public void ShouldReturnTheCorrectTable()
        {
            var transactions = new TestTransactionsLoader().Load().ToImmutableArray();
            var items = transactions.ExtractFeatures();
            var result = transactions.CalculateFrequencyTable(items).ToImmutableArray();
            foreach (var line in result)
            {
                Console.WriteLine("item: {0}, support: {1}, frequency: {2}", line.Item.Format(), line.Support, line.Frequency);
            }
        }
    }
}
