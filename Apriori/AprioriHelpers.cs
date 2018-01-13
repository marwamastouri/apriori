using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Apriori
{
    public static class AprioriHelpers
    {
        /// <summary>
        /// Extract a list of unique items from a the <paramref name="transactions"/>. 
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns></returns>
        public static IEnumerable<ISet<string>> ExtractFeatures(this IEnumerable<ISet<string>> transactions)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));

            var returnedFeatureSets = ImmutableList.Create<ISet<string>>();
            foreach (var line in transactions)
            foreach (var feature in line)
            {
                var featureSet = ImmutableHashSet.Create(new[] { feature });
                if (returnedFeatureSets.Any(c => c.SetEquals(featureSet))) continue;
                returnedFeatureSets = returnedFeatureSets.Add(featureSet);
                yield return featureSet;
            }
        }

        /// <summary>
        /// Joins the sets <paramref name="candidates"/> into a single set. 
        /// </summary>
        /// <param name="candidates"></param>
        /// <returns></returns>
        public static ISet<string> ExtractItems(this IEnumerable<ISet<string>> candidates)
        {
            if (candidates == null) throw new ArgumentNullException(nameof(candidates));

            return candidates
                .SelectMany(line => line)
                .Aggregate(ImmutableHashSet.Create<string>(), (current, feature) => current.Add(feature));
        }

        /// <summary>
        /// Filters <paramref name="candidates"/> which are not frequent enough. 
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="transactions"></param>
        /// <param name="minFrequency"></param>
        /// <returns></returns>
        public static IEnumerable<ISet<string>> FilterByFrequency(this IEnumerable<ISet<string>> candidates, IEnumerable<ISet<string>> transactions, float minFrequency = 0.1f)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (candidates == null) throw new ArgumentNullException(nameof(candidates));
            if (minFrequency < 0f) throw new ArgumentOutOfRangeException(nameof(minFrequency), minFrequency, "Must be a positive number");
            if (minFrequency > 1f) throw new ArgumentOutOfRangeException(nameof(minFrequency), minFrequency, "Must be lower than 100%.");

            return candidates.Where(c => Frequency(transactions, c) > minFrequency);
        }

        /// <summary>
        /// Generates a list of candidates, which represent the boundary for the next search iteration. 
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="candidates"></param>
        /// <param name="items"></param>
        /// <param name="minConfidence"></param>
        /// <param name="checkConfidence"></param>
        /// <returns></returns>
        public static IEnumerable<ISet<string>> GenerateCandidates(this IEnumerable<ISet<string>> transactions, IEnumerable<ISet<string>> candidates, ISet<string> items, float minConfidence = 0.3f, bool checkConfidence = true)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (candidates == null) throw new ArgumentNullException(nameof(candidates));
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (minConfidence < 0f) throw new ArgumentOutOfRangeException(nameof(minConfidence), minConfidence, "Must be a positive number");
            if (minConfidence > 1f) throw new ArgumentOutOfRangeException(nameof(minConfidence), minConfidence, "Must be lower than 100%.");

            var transactionsArray = transactions.ToImmutableArray();
            var candidateArray = candidates.ToImmutableArray();
            var foundCandidates = ImmutableList.Create<ISet<string>>();

            foreach (var candidate in candidateArray)
            foreach (var remainingItem in items)
            {
                var item = ImmutableHashSet.Create(new[] { remainingItem });
                var newCandidate = candidate.Union(item).ToImmutableHashSet();
                if (newCandidate.SetEquals(candidate)) continue; // item is already part of the set
                if (foundCandidates.Any(c => c.SetEquals(newCandidate))) continue; // candidate set was already returned
                if (checkConfidence && Confidence(transactionsArray, item, candidate) < minConfidence) continue; // filter by confidence
                foundCandidates = foundCandidates.Add(newCandidate);
                yield return newCandidate;
            }
        }

        /// <summary>
        /// The probability that the given <paramref name="set"/> occours in an <paramref name="transactions">transaction</paramref>. 
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static float Frequency(this IEnumerable<ISet<string>> transactions, ISet<string> set)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (set == null) throw new ArgumentNullException(nameof(set));

            var ts = transactions.ToImmutableArray();
            var totalNumberOfItems = ts.Count();
            return (float)Support(ts, set) / totalNumberOfItems;
        }

        /// <summary>
        /// Probability, that when an set of items <paramref name="x"/> exists in the <paramref name="transactions"/>, 
        /// the set of item <paramref name="y"/> also exists in the <paramref name="transactions"/>. 
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Confidence(this IEnumerable<ISet<string>> transactions, ISet<string> x, ISet<string> y)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            var ts = transactions.ToImmutableArray();
            return Frequency(ts, x.Union(y).ToHashSet())
                   / Frequency(ts, y);
        }

        /// <summary>
        /// The number of <paramref name="transactions"/>, which contain the subset <paramref name="feature"/>.
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static int Support(this IEnumerable<ISet<string>> transactions, ISet<string> feature)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (feature == null) throw new ArgumentNullException(nameof(feature));

            return transactions.Sum(line => line.IsSupersetOf(feature) ? 1 : 0);
        }

        /// <summary>
        /// The ratio of <paramref name="transactions"/> where <paramref name="x"/> and <paramref name="y"/> are dependend 
        /// and <paramref name="transactions"/> where <paramref name="x"/> and <paramref name="y"/> are independent.
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Lift(this IEnumerable<ISet<string>> transactions, ISet<string> x, ISet<string> y)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            var ts = transactions.ToImmutableArray();
            return Frequency(ts, x.Union(y).ToImmutableHashSet())
                   / (Frequency(ts, x) * Frequency(ts, y));
        }

        /// <summary>
        /// The ratio of <paramref name="transactions"/> where <paramref name="x"/> is expected to be without <paramref name="y"/> when <paramref name="x"/> and <paramref name="y"/> are independent
        /// and <paramref name="transactions"/> where <paramref name="x"/> and <paramref name="y"/>. 
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Conviction(this IEnumerable<ISet<string>> transactions, ISet<string> x, ISet<string> y)
        {
            if (transactions == null) throw new ArgumentNullException(nameof(transactions));
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            var ts = transactions.ToImmutableArray();
            return (1 - Frequency(ts, y))
                   / (1 - Confidence(ts, x, y));
        }
    }
}