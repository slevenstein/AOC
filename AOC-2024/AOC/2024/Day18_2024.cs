internal class Day18_2024
{
    public static void Solution(int part)
    {
        const int size = 71;
        const int bytesFallen = 1024;

        int sum = 0;

        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        List<(int x, int y)> points = lines.Select(line =>
        {
            int[] splitString = line.Split(',').Select(int.Parse).ToArray();
            return (splitString[0], splitString[1]);
        }).ToList();
        List<(int x, int y)> newPoints;

        if (part == 1)
        {
            newPoints = points.GetRange(0, bytesFallen);
            sum = Run(size, newPoints);
            Console.WriteLine(sum);
        }
        else if (part == 2)
        {
            int i = 2048;
            int final = -1;
            for (int jump = 1024; jump > 0; jump /= 2)
            {
                newPoints = points.GetRange(0, Math.Min(points.Count, i));

                if (Run(size, newPoints) == -1)
                {
                    final = i;
                    i -= jump;
                }
                else
                {
                    i += jump;
                }

                //Console.WriteLine(i);
            }

            (int x, int y) end = points[final - 1];
            Console.WriteLine($"{end.x}, {end.y}");
        }
    }

    private static int Run(int size, List<(int x, int y)> newPoints)
    {
        List<(int x, int y)> visited = [];
        Queue<(int x, int y, int cost)> queue = [];

        queue.Enqueue((0, 0, 0));
        while (queue.Count > 0)
        {
            (int x, int y, int currentCost) = queue.Dequeue();
            (int x, int y) currentPosn = (x, y);

            if (currentPosn == (size - 1, size - 1))
            {
                return currentCost;
            }

            List<(int x, int y)> moves = [(1, 0), (0, 1), (-1, 0), (0, -1)];

            foreach ((int x, int y) move in moves)
            {
                int newX = currentPosn.x + move.x;
                int newY = currentPosn.y + move.y;
                if (newX >= 0 && newY >= 0 && newX < size && newY < size &&
                    !visited.Contains(currentPosn) && !newPoints.Contains(currentPosn))
                    queue.Enqueue((x + move.x, y + move.y, currentCost + 1));
            }

            visited.Add(currentPosn);
        }

        return -1;
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