using System;
using System.Linq;
using MoreLinq;

namespace Apriori
{
    internal class Program
    {
        private static void Main()
        {
            var loader = new FileTransactionsLoader();
            var transactions = loader.Load();

            transactions
                .Select(tx => tx.Format())
                .ForEach(item => Console.WriteLine(item));
        }
    }
}
