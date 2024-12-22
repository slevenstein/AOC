using System.Diagnostics;

internal class Day22_2024
{
    public static void Solution(int part)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        List<long> lines = File.ReadLines(@"D:\SCL33\Downloads\input").Select(long.Parse).ToList();
        //List<char[]> lines = File.ReadLines(@"D:\SCL33\Downloads\input").Select(s => s.ToCharArray()).ToList();
        List<Dictionary<(int, int, int, int), int>> changesLists = [];

        long m = Mix(42, 15);
        Debug.Assert(m == 37);
        long p = Prune(100000000);
        Debug.Assert(p == 16113920);

        long sum = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            long secret = lines[i];
            List<int> previousChanges = [];
            Dictionary<(int, int, int, int), int> changes = [];

            int last = int.Parse(secret.ToString().Last().ToString());
            for (int k = 0; k < 2000; k++)
            {
                long value = secret * 64;
                secret = Prune(Mix(value, secret));

                value = secret / 32;
                secret = Prune(Mix(value, secret));

                value = secret * 2048;
                secret = Prune(Mix(value, secret));

                int next = int.Parse(secret.ToString().Last().ToString());
                int nextChange = next - last;
                //Console.WriteLine(next + ": " + nextChange);

                previousChanges.Add(nextChange);
                if (previousChanges.Count == 4)
                {
                    changes.TryAdd((
                            previousChanges[0],
                            previousChanges[1],
                            previousChanges[2],
                            previousChanges[3]),
                        next);
                    previousChanges.RemoveAt(0);
                }

                last = next;
            }

            changesLists.Add(changes);
            lines[i] = secret;
        }

        if (part == 1)
        {
            sum = lines.Sum();
        }
        else if (part == 2)
        {
            IEnumerable<int> range = Enumerable.Range(-9, 19); // -9 to 9 inclusive

            IEnumerable<(int a, int b, int c, int d)> combinations =
                from a in range
                from b in range
                from c in range
                from d in range
                select (a, b, c, d);

            sum = combinations.AsParallel().Select(comb =>
                    changesLists.Sum(t =>
                        t.GetValueOrDefault(comb, 0)))
                .Max();

            /*foreach ((int a, int b, int c, int d) comb in combinations)
            {
                int currentSum = 0;

                foreach (Dictionary<(int, int, int, int), int> t in changesLists)
                {
                    if (t.TryGetValue(comb, out int value))
                    {
                        currentSum += value;
                    }
                }

                if (currentSum > sum)
                {
                    sum = currentSum;
                }
            }*/
        }

        stopwatch.Stop();
        Console.WriteLine(sum);

        // Output the elapsed time
        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    private static long Mix(long val, long secret)
    {
        return val ^ secret;
    }

    private static long Prune(long val)
    {
        return val % 16777216;
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
10 - don't overthink


GitHub Repo: https://github.com/slevenstein/AOC/tree/main/AOC-2024/AOC/2024
*/