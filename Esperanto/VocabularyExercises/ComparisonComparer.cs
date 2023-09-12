using System;
using System.Collections.Generic;

namespace Esperanto.VocabularyExercises
{
    public class ComparisonComparer<T> : IComparer<T>
    {
        private readonly Comparison<T> comparison;

        public ComparisonComparer(Comparison<T> comparison)
        {
            this.comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
        }

        public int Compare(T x, T y)
        {
            return comparison(x, y);
        }
    }
}