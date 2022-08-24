using System;
using System.Collections.Generic;
using System.Linq;

namespace Build1.UnityUtils.Weighted
{
    public static class WeightedExtensions
    {
        private static readonly Random _random = new();
        
        /*
         * Items.
         */
        
        public static T RandomByWeights<T>(this T[] weights) where T : IWeighted
        {
            if (weights.Length < 2)
                return weights[0];
            
            var total = weights.Sum(i => i.Weight);
            var number = _random.Next(0, total);
            for (var i = 0; i < weights.Length; i++)
            {
                var element = weights.ElementAt(i);
                if (number <= element.Weight)
                    return element;
                number -= element.Weight;
            }
            
            throw new Exception("Something went wrong =(");
        }
        
        public static T RandomByWeights<T>(this IList<T> weights) where T : IWeighted
        {
            if (weights.Count < 2)
                return weights[0];
            
            var total = weights.Sum(i => i.Weight);
            var number = _random.Next(0, total);
            for (var i = 0; i < weights.Count; i++)
            {
                var element = weights.ElementAt(i);
                if (number <= element.Weight)
                    return element;
                number -= element.Weight;
            }
            
            throw new Exception("Something went wrong =(");
        }
        
        public static T RandomByWeights<T>(this IEnumerable<T> weights) where T : IWeighted
        {
            return RandomByWeights(weights as T[] ?? weights.ToArray());
        }
        
        /*
         * Index.
         */
        
        public static int RandomIndexByWeights(this int[] weights)
        {
            if (weights.Length < 2)
                return weights.Length - 1;
            
            var total = weights.Sum();
            var number = _random.Next(0, total);
            for (var i = 0; i < weights.Length; i++)
            {
                var element = weights[i];
                if (number <= element)
                    return i;
                number -= element;
            }
            
            throw new Exception("Something went wrong =(");
        }
        
        public static int RandomIndexByWeights(this IList<int> weights)
        {
            if (weights.Count < 2)
                return weights.Count - 1;
            
            var total = weights.Sum();
            var number = _random.Next(0, total);
            for (var i = 0; i < weights.Count; i++)
            {
                var element = weights[i];
                if (number <= element)
                    return i;
                number -= element;
            }
            
            throw new Exception("Something went wrong =(");
        }
        
        public static int RandomIndexByWeights(this IEnumerable<int> weights)
        {
            return RandomIndexByWeights(weights as int[] ?? weights.ToArray());
        }
    }
}