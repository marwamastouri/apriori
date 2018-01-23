using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Apriori
{
    internal class Program
    {
        private static void Main()
        {
            var transactions = new FileTransactionsLoader().Load().ToImmutableArray();
            
            OutputFrequentItems(transactions);
            OutputMostFrequentItemSets(transactions);
        }

        /// <summary>
        /// Writes all the frequent item sets along with their absolute supports into a text ﬁle named "patterns.txt"
        /// </summary>
        /// <param name="transactions"></param>
        private static void OutputMostFrequentItemSets(ImmutableArray<ISet<string>> transactions)
        {
            var result = transactions.SearchMostFrequentItems();
            WriteResultToFile(result, "patterns.txt");
        }

        /// <summary>
        /// Outputs all the length-1 frequent items (itemsets containing only one element) 
        /// with their absolute supports into a textfile named "oneItems.txt"
        /// </summary>
        /// <param name="transactions"></param>
        private static void OutputFrequentItems(ImmutableArray<ISet<string>> transactions)
        {
            const string fileName = "oneItems.txt";

            var features = transactions.ExtractFeatures();
            
            var result = transactions
                .CalculateFrequencyTable(features)
                .FilterByFrequency();

            var resultWithFrequencies = transactions.CalculateFrequencyTable(result);
            WriteResultToFile(resultWithFrequencies, fileName);
        }

        private static void WriteResultToFile(IEnumerable<ItemSupport> result, string fileName)
        {
            var lines = result.Select(feature => feature.ToString());
            var directory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(directory, "..", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var file = new StreamWriter(filePath))
            {
                foreach (var line in lines)
                {
                    file.WriteLineAsync(line).Wait();
                }
            }
        }
    }
}
