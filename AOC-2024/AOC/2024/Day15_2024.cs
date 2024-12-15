internal class Day15_2024
{
    public static void Solution(int part)
    {
        Part2();
    }

    private static void Part1()
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        int i;
        List<char[]> grid = [];

        (int y, int x) robot = (0, 0);
        for (i = 0;; i++)
        {
            string line = lines[i];
            if (line == "")
                break;

            int robotXIndex = line.IndexOf('@');
            if (robotXIndex != -1)
                robot = (i, robotXIndex);

            grid.Add(line.ToCharArray());
        }

        for (i++; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];

                (int y, int x) moveRobot;
                switch (c)
                {
                    case '^':
                        CanMove('@', robot, (-1, 0));
                        break;
                    case '>':
                        CanMove('@', robot, (0, 1));
                        break;
                    case 'v':
                        CanMove('@', robot, (1, 0));
                        break;
                    case '<':
                        CanMove('@', robot, (0, -1));
                        break;
                }

                bool CanMove(char obj, (int y, int x) pos, (int y, int x) move)
                {
                    (int y, int x) newPos = (pos.y + move.y, pos.x + move.x);

                    char nextObj = grid[newPos.y][newPos.x];
                    bool canMove = false;
                    switch (nextObj)
                    {
                        case '.':
                            grid[newPos.y][newPos.x] = obj;
                            canMove = true;
                            break;
                        case '#':
                            break;
                        case 'O':
                            if (CanMove(nextObj, newPos, move))
                            {
                                grid[newPos.y][newPos.x] = obj;
                                canMove = true;
                            }

                            break;
                        default:
                            throw new Exception();
                    }

                    if (canMove && obj == '@')
                    {
                        robot = newPos;
                        grid[pos.y][pos.x] = '.';
                    }

                    return canMove;
                }
            }
        }

        //sum = grid.Sum(row => row.Sum(c => c == '0' ? sum : 0));

        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                char c = grid[y][x];
                if (grid[y][x] == 'O')
                {
                    sum += y * 100 + x;
                }
            }
        }

        Console.WriteLine(sum);
    }

    
    
    
    
    private static void Part2()
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        int i;
        List<char[]> grid = [];

        (int y, int x) robot = (0, 0);
        for (i = 0;; i++)
        {
            string line = lines[i];
            if (line == "")
                break;

            List<char> row = [];
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];
                switch (c)
                {
                    case '#':
                        row.AddRange("##");
                        break;
                    case 'O':
                        row.AddRange("[]");
                        break;
                    case '.':
                        row.AddRange("..");
                        break;
                    case '@':
                        robot = (i, row.Count);
                        row.AddRange("@.");
                        break;
                }
            }

            int robotXIndex = row.IndexOf('@');
            if (robotXIndex != -1)
                robot = (i, robotXIndex);

            grid.Add(row.ToArray());
        }

        for (i++; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];

                (int y, int x) moveRobot;
                switch (c)
                {
                    case '^':
                        TryMove('@', robot, (-1, 0));
                        break;
                    case '>':
                        TryMove('@', robot, (0, 1));
                        break;
                    case 'v':
                        TryMove('@', robot, (1, 0));
                        break;
                    case '<':
                        TryMove('@', robot, (0, -1));
                        break;
                }

                void TryMove(char obj, (int y, int x) pos, (int y, int x) move)
                {
                    if (CanMove('@', robot, move))
                    {
                        Move('@', robot, move);
                    }
                }

                bool CanMove(char obj, (int y, int x) pos, (int y, int x) move)
                {
                    (int y, int x) newPos = (pos.y + move.y, pos.x + move.x);

                    char nextObj = grid[newPos.y][newPos.x];
                    bool canMove = false;
                    switch (nextObj)
                    {
                        case '.':
                            canMove = true;
                            break;
                        case '#':
                            break;
                        case '[':
                            if (CanMove(nextObj, newPos, move) && 
                                (move.y == 0 || CanMove(nextObj, (newPos.y, newPos.x+1), move)))
                            {
                                canMove = true;
                            }
                            break;
                        case ']':
                            if (CanMove(nextObj, newPos, move) && 
                                (move.y == 0 || CanMove(nextObj, (newPos.y, newPos.x-1), move)))
                            {
                                canMove = true;
                            }
                            break;
                        default:
                            throw new Exception();
                    }

                    return canMove;
                }

                void Move(char obj, (int y, int x) pos, (int y, int x) move, bool isDirectMove = true)
                {
                    (int y, int x) newPos = (pos.y + move.y, pos.x + move.x);

                    char nextObj = grid[newPos.y][newPos.x];
                    switch (nextObj)
                    {
                        case '.':
                            grid[newPos.y][newPos.x] = obj;
                            break;
                        case '#':
                            break;
                        case '[':
                            grid[newPos.y][newPos.x] = obj;
                            Move('[', newPos, move);
                            if (move.y != 0 && isDirectMove) 
                                Move('.', (pos.y, pos.x+1), move, false);
                            break;
                        case ']':
                            grid[newPos.y][newPos.x] = obj;
                            Move(']', newPos, move);
                            if (move.y != 0 && isDirectMove) 
                                Move('.', (pos.y, pos.x-1), move, false);
                            break;
                        default:
                            throw new Exception();
                    }

                    if (obj == '@')
                    {
                        robot = newPos;
                        grid[pos.y][pos.x] = '.';
                    }
                }
            }
        }

        //sum = grid.Sum(row => row.Sum(c => c == '0' ? sum : 0));

        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                char c = grid[y][x];
                if (grid[y][x] == '[')
                {
                    sum += y * 100 + x;
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

(2024)
Tips:
10 - dont overthink


Github Repo: https://github.com/slevenstein/AOC/tree/main/AOC-2024/AOC/2024
*/