internal class Day19_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();
        //List<char[]> lines = File.ReadLines(@"D:\SCL33\Downloads\input").Select(s => s.ToCharArray()).ToList();

        HashSet<string> towels = lines[0].Split(", ").ToHashSet();
        List<string> patterns = lines[2..];

        int sum = 0;

        if (part == 1)
        {
            foreach (string pattern in patterns)
            {
                List<string> visitedPatternsLeft = [];
                if (CheckPattern(pattern))
                    sum++;

                //Console.WriteLine(index);

                bool CheckPattern(string currPattern)
                {
                    if (visitedPatternsLeft.Contains(currPattern))
                        return false;
                    visitedPatternsLeft.Add(currPattern);

                    if (currPattern == "")
                        return true;

                    return towels
                        .Where(currPattern.StartsWith)
                        .Any(towel => CheckPattern(currPattern[towel.Length..]));
                }
            }

            Console.WriteLine(sum);
        }
        else if (part == 2)
        {
            long sum2 = 0;
            foreach (string pattern in patterns)
            {
                Dictionary<string, long> visitedPatternsLeft = [];
                sum2 += CheckPattern(pattern);

                //Console.WriteLine(index);

                long CheckPattern(string currPattern)
                {
                    if (visitedPatternsLeft.TryGetValue(currPattern, out long checkPattern))
                    {
                        return checkPattern;
                    }

                    if (currPattern == "")
                        return 1;

                    long s = towels
                        .Where(currPattern.StartsWith)
                        .Sum(towel => CheckPattern(currPattern[towel.Length..]));

                    visitedPatternsLeft.Add(currPattern, s);

                    return s;
                }
            }

            Console.WriteLine(sum2);
        }
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