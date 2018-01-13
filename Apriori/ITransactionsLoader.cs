using System.Collections.Generic;

namespace Apriori
{
    public interface ITransactionsLoader
    {
        IEnumerable<ISet<string>> Load();
    }
}