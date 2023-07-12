using System;
using System.Collections.Generic;

public class RandomNumberGenerator
{
    private static Random random = new Random();

    public static List<int> GenerateUniqueRandomNumbers(int minValue, int maxValue)
    {
        List<int> numbers = new List<int>();
        for (int i = minValue; i <= maxValue; i++)
        {
            numbers.Add(i);
        }

        int n = numbers.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            int value = numbers[k];
            numbers[k] = numbers[n];
            numbers[n] = value;
        }

        return numbers;
    }
}