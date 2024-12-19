internal class Day16_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = int.MaxValue;
        (int y, int x) start = (0, 0);

        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];

            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];

                if (c == 'S')
                {
                    start = (i, k);
                    break;
                }
            }

            if (start != (0, 0))
                break;
        }

        bool endFound = false;
        int tiles = 1;
        HashSet<(int y, int x)> path = [];
        Dictionary<((int y, int x) pos, (int y, int x) prevDir), (int score, HashSet<(int y, int x)> path)>
            visited = []; // value is cost
        PriorityQueue<((int y, int x) pos, (int y, int x) prevDir, HashSet<(int y, int x)> path), int> queue = new();
        queue.Enqueue((start, (0, 1), []), 0);

        while (queue.TryDequeue(
                   out ((int y, int x) pos, (int y, int x) prevDir, HashSet<(int y, int x)> path) entry,
                   out int cumScore))
        {
            (int y, int x) pos = entry.pos;
            (int y, int x) prevDir = entry.prevDir;

            if (endFound)
            {
                if (cumScore != sum)
                    break;
                path = entry.path;
                tiles = Math.Max(tiles, path.Count);
            }

            path = entry.path;

            if (visited.TryGetValue((pos, prevDir), out (int score, HashSet<(int y, int x)> path) value))
            {
                if (value.score == cumScore)
                {
                    path = value.path.Concat(entry.path).ToHashSet();
                    visited[(pos, prevDir)] = (cumScore, path.ToHashSet());

                    if (endFound)
                        tiles = path.Count;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                visited.Add((pos, prevDir), (cumScore, path));
            }

            if (lines[pos.y][pos.x] == 'E')
            {
                endFound = true;
                sum = cumScore;

                tiles = path.Count;
                //break;
                continue;
            }

            List<(int y, int x)> moves = [(1, 0), (0, 1), (-1, 0), (0, -1)];
            moves.Remove((-prevDir.y, -prevDir.x));

            foreach ((int y, int x) move in moves)
            {
                (int y, int x) newPos = (pos.y + move.y, pos.x + move.x);
                char c = lines[newPos.y][newPos.x];

                if (c == '#')
                    continue;

                ((int, int) newPos, (int y, int x) move) newEntry = (newPos, move);

                int score = 1;
                if (move != prevDir)
                    score += 1000;

                HashSet<(int y, int x)> newPath = path.ToHashSet();
                newPath.Add(newPos);

                queue.Enqueue((newPos, move, newPath), cumScore + score);
            }
        }

        //print path
        List<char[]> grid = File.ReadLines(@"D:\SCL33\Downloads\input").Select(s => s.ToCharArray()).ToList();
        foreach ((int y, int x) prevPosn in path)
        {
            grid[prevPosn.y][prevPosn.x] = '0';
        }

        using (StreamWriter writer = new(@"D:\SCL33\Downloads\output"))
        {
            foreach (char[] row in grid)
            {
                writer.WriteLine(new string(row));
            }
        }

        tiles++;
        Console.WriteLine(tiles);
        Console.WriteLine(sum);
    }

    private struct Entry
    {
        private (int y, int x) pos;
        private (int y, int x) prevDir;
        private List<(int y, int x)> path;
        private int cost;

        public Entry((int y, int x) pos, (int y, int x) prevDir, List<(int y, int x)> path)
        {
            this.pos = pos;
            this.prevDir = prevDir;
            this.path = path;
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
10 - dont overthink


Github Repo: https://github.com/slevenstein/AOC/tree/main/AOC-2024/AOC/2024
*/