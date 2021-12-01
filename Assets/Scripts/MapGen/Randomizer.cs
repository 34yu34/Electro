using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Randomizer
{
    public static void Initialize(int seed)
    {
        Random.InitState(seed);
    }

    public static List<T> Shuffle<T>(IEnumerable<T> list)
    {
        var data = list.ToList();

        for (int currentIndex = 0; currentIndex != data.Count(); ++currentIndex)
        {
            var swap_index = Random.Range(currentIndex, data.Count());
            var temp = data[swap_index];
            data[swap_index] = data[currentIndex];
            data[currentIndex] = temp;
        }

        return data;
    }
}
