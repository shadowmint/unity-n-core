using System.Linq;
using System.Collections.Generic;

namespace N.Package.Core
{
  /// Some common random helpers
  public class Random
  {
    private static System.Random _random;

    private static System.Random Rand
    {
      get
      {
        if (_random == null)
        {
          _random = new System.Random();
        }
        return _random;
      }
    }

    /// Check if a random query is <= chance
    /// @param chance The chance of an value being true, where 0 < chance < 1
    public static bool Chance(float chance)
    {
      var value = Rand.NextDouble();
      var ok = value < chance;
      return ok;
    }

    /// Returns random integers that range from minValue to maxValue – 1
    /// As per System.Random use this for Between(0, array.Length);
    public static int Between(int minValue, int maxValue)
    {
      return Rand.Next(minValue, maxValue);
    }

    /// Returns random float that range from minValue to maxValue
    public static float Between(float minValue, float maxValue)
    {
      return minValue + (float) (Rand.NextDouble()*(maxValue - minValue));
    }

    /// Pick a random item from a list
    public static T Pick<T>(IEnumerable<T> options)
    {
      var list = new List<T>(options);
      return list[Between(0, list.Count)];
    }

    /// Pick a Distinct subset of a the options
    public static List<T> Distinct<T>(IEnumerable<T> options, int count)
    {
      var list = options.ToList();
      Shuffle(list);
      if (count < list.Count)
      {
        list.RemoveRange(1, list.Count - count);
      }
      return list;
    }

    /// Shuffle a list
    public static void Shuffle<T>(IList<T> list)
    {
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = Rand.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }
  }
}