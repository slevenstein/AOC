internal class Day14_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        const int seconds = 100;

        int upLeftMult = 0;
        int upRightMult = 0;
        int bottomRightMult = 0;
        int bottomLeftMult = 0;

        const int maxWidth = 101;
        const int maxHeight = 103;
        const int horizontalMiddle = (maxWidth - 1) / 2;
        const int verticalMiddle = (maxHeight - 1) / 2;

        if (part == 1)
        {
            foreach (string line in lines)
            {
                string[] strings = line.Split(' ');

                int[] pVals = strings[0][2..].Split(',').Select(int.Parse).ToArray();
                (int x, int y) p = (pVals[0], pVals[1]);

                int[] vVals = strings[1][2..].Split(',').Select(int.Parse).ToArray();
                (int x, int y) v = (vVals[0], vVals[1]);

                (int x, int y) finalP = (
                    Mod(p.x + v.x * seconds, maxWidth),
                    Mod(p.y + v.y * seconds, maxHeight));

                if (finalP.x < horizontalMiddle)
                {
                    if (finalP.y < verticalMiddle)
                        upLeftMult++;
                    else if (finalP.y > verticalMiddle)
                        bottomLeftMult++;
                }
                else if (finalP.x > horizontalMiddle)
                {
                    if (finalP.y < verticalMiddle)
                        upRightMult++;
                    else if (finalP.y > verticalMiddle)
                        bottomRightMult++;
                }
            }

            sum = upLeftMult * bottomLeftMult * bottomRightMult * upRightMult;
        }
        else if (part == 2)
        {
            // find convergence
            HashSet<int> first = [];
            for (int i = 1706; i < 10500; i += 103)
            {
                first.Add(i);
            }

            for (int i = 1715; i < 10500; i += 101)
            {
                if (first.Contains(i))
                {
                    sum = i;
                }
            }

            // visualization
            bool visualize = true;
            if (visualize)
            {
                List<(int y, int x)> positions = [];
                List<(int y, int x)> velocities = [];
                int[,] grid = new int[maxHeight, maxWidth];
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];
                    string[] strings = line.Split(' ');

                    int[] pVals = strings[0][2..].Split(',').Select(int.Parse).ToArray();
                    positions.Add((pVals[0], pVals[1]));

                    int[] vVals = strings[1][2..].Split(',').Select(int.Parse).ToArray();
                    velocities.Add((vVals[0], vVals[1]));
                }

                for (int s = sum; s < sum + 1; s++)
                {
                    for (int y = 0; y < maxHeight; y++)
                    {
                        for (int x = 0; x < maxWidth; x++)
                        {
                            grid[y, x] = 0;
                        }
                    }

                    for (int i = 0; i < positions.Count; i++)
                    {
                        (int x, int y) p = positions[i];
                        (int x, int y) v = velocities[i];

                        (int x, int y) finalP = (
                            Mod(p.x + v.x * s, maxWidth),
                            Mod(p.y + v.y * s, maxHeight));

                        grid[finalP.y, finalP.x] = 1;
                    }

                    for (int y = 0; y < maxHeight; y++)
                    {
                        for (int x = 0; x < maxWidth; x++)
                        {
                            Console.Write(grid[y, x] == 0 ? '.' : '0');
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine(s);
                }
            }
        }

        Console.WriteLine(sum);
    }

    // mod without returning negative
    private static int Mod(int a, int b)
    {
        int ret = a % b;
        return ret >= 0 ? ret : ret + b;
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


Github Repo: https://github.com/slevenstein/AOC/tree/main/AOC-2024/AOC/2024
*/