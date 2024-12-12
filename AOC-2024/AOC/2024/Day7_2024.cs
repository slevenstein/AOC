// ReSharper disable RedundantToStringCallForValueType

internal class Day7_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        long sum = 0;
        foreach (string line in lines)
        {
            string[] strings = line.Split(' ');
            long result = long.Parse(strings[0].Remove(strings[0].Length - 1));

            Queue<int> numsQueue = new(strings.Skip(1).Select(int.Parse));
            if (EqualsResult(numsQueue, numsQueue.Dequeue(), result, part))
            {
                sum += result;
            }
        }

        Console.WriteLine(sum);
    }

    private static bool EqualsResult(Queue<int> numsRemaining, long currResult, long result, int part)
    {
        if (numsRemaining.Count == 0)
        {
            return currResult == result;
        }

        int next = numsRemaining.Dequeue();
        // +, *, ||
        return EqualsResult(new Queue<int>(numsRemaining),
                   currResult + next, result, part) ||
               EqualsResult(new Queue<int>(numsRemaining),
                   currResult * next, result, part) ||
               (part == 2 && EqualsResult(new Queue<int>(numsRemaining),
                   long.Parse(currResult.ToString() + next.ToString()), result, part));
    }
}

/*
line.Split(' ');
line.Trim().Replace("  ", " ");


(2023)
Help:
3-2 - debugging help -> add *'s to border
5-2 - hint -> do in reverse
8-2 - hint -> LCM
10-2 - hint -> rasterization
12-2 - hint -> DP/cache, dictionary key to string
17-1 - Dijkstra’s refresher
       hint -> visited data: posn, direction, sameMoves
18-2 - hint -> Shoelaace + Pick's formula
24-2 - include unknown in initial equation

Tips:
12-2, 11-2 - use long for big numbers (wrap in checked context?)
all - SLEEP/TAKE BREAK WHEN STUCK

(2024)
Tips:
10 - dont overthink
*/