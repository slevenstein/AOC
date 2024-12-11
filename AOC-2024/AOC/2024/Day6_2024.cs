class Day6_2024
{
    public static void Solution(int part)
    {
        List<char[]> lines = File.ReadLines(@"D:\SCL33\Downloads\input").Select(s => s.ToCharArray()).ToList();

        int sum = 0;

        int startY = 0;
        int startX = 0;
        for ( ; startY < lines.Count; startY++)
        {
            char[] line = lines[startY];
            bool startFound = false;
            for (startX = 0; startX < line.Length; startX++)
            {
                char c = line[startX];

                if (c == '^')
                {
                    startFound = true;
                    break;
                }
            }

            if (startFound)
            {
                break;
            }
        }

        HashSet<(int y, int x)> visited = [];

        RunGuardPatrol(visited, null);

        if (part == 1)
            sum = visited.Count;
        else
        {
            visited.Remove((startY, startX));

            foreach ((int y, int x) in visited)
            {
                char old = lines[y][x];
                lines[y][x] = '#';
                RunGuardPatrol(null, []);
                lines[y][x] = old;

            }
        }

        Console.WriteLine(sum);


        // returns true
        void RunGuardPatrol(HashSet<(int y, int x)>? visited, HashSet<(int y, int x, Direction dir)>? prevMoves)
        {
            int y = startY;
            int x = startX;
            Direction currDir = Direction.UP;
            while (x < lines[0].Length && y < lines[0].Length && x >= 0 && y >= 0)
            {
                if (lines[y][x] == '#')
                {
                    // go back
                    Move(ref y, ref x, currDir, -1);

                    // change direction
                    currDir = (Direction)(((int)currDir + 1) % 4);
                }
                else
                {
                    // do part 1 if visited is not null
                    visited?.Add((y, x));

                    // do part 2 if prevMoves is not null
                    if (prevMoves != null)
                    {
                        // check for loop
                        var move = (y, x, currDir);
                        if (prevMoves.Contains(move))
                        {
                            sum++;
                            break;
                        }
                        prevMoves.Add(move);
                    }
                }


                Move(ref y, ref x, currDir, 1);
            }
        }
    }   
    
    enum Direction
    {
        RIGHT,
        DOWN,
        LEFT,
        UP
    }

    // step is either +1 or -1
    static void Move(ref int y, ref int x, Direction dir, int step)
    {
        if (dir == Direction.RIGHT)
        {
            x += step;
        }
        else if (dir == Direction.DOWN)
        {
            y += step;
        }
        else if (dir == Direction.LEFT)
        {
            x -= step;
        }
        else if (dir == Direction.UP)
        {
            y -= step;
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
*/