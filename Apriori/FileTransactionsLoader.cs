using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Apriori
{
    public sealed class FileTransactionsLoader : ITransactionsLoader
    {
        public IEnumerable<ISet<string>> Load()
        {
            var directory = Directory.GetCurrentDirectory();
            const string fileName = "adult.txt";
            var content = File.ReadLines(Path.Combine(directory, fileName));
            return content.Select(line => ImmutableHashSet.Create(line.Split(';')));
        }
    }
}