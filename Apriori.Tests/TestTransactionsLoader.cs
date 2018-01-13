using System;
using System.Collections.Generic;

namespace Apriori.Tests
{
    public sealed class TestTransactionsLoader : ITransactionsLoader
    {
        public IEnumerable<ISet<string>> Load()
        {
            yield return new HashSet<string> { An.Onion, An.Potato, An.Burger };
            yield return new HashSet<string> { An.Potato, An.Burger, An.Milk };
            yield return new HashSet<string> { An.Milk, An.Beer };
            yield return new HashSet<string> { An.Onion, An.Potato, An.Milk };
            yield return new HashSet<string> { An.Onion, An.Potato, An.Burger, An.Beer };
            yield return new HashSet<string> { An.Onion, An.Potato, An.Burger, An.Milk, An.Beer };
        }
    }
}