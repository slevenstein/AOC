
using System.Collections.Generic;

class Day10_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();
        List<List<int>> grid = lines.Select(
            s => s.ToCharArray().Select(a => a - '0').ToList()
            ).ToList();

        int sum = 0;

        for (int y = 0; y < grid.Count; y++)
        {
           
            for (int x = 0; x < grid[y].Count; x++)
            {
                List<(int y, int x)> visited = [];
                sum += FindScore(y, x, 0);
                
                int FindScore(int y, int x, int height)
                {
                    int score = 0;

                    int currHeight = grid[y][x];
                    if (currHeight == height && !visited.Contains((y,x)))
                    {
                        if (part == 1)
                            visited.Add((y, x));

                        if (currHeight == 9)
                        {
                            return 1;
                        }

                        int newHeight = height + 1;

                        int newY = y + 1;
                        int newX = x;
                        if (newY < grid.Count)
                            score += FindScore(newY, newX, newHeight);

                        newY = y - 1;
                        if (newY >= 0)
                            score += FindScore(newY, newX, newHeight);

                        newY = y;
                        newX = x + 1;
                        if (newX < grid[0].Count)
                            score += FindScore(newY, newX, newHeight);

                        newX = x - 1;
                        if (newX >= 0)
                            score += FindScore(newY, newX, newHeight);
                    }
                    
                    return score;
                }
            }

            
        }


        Console.WriteLine(sum);
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

*/