internal class Day9_2024
{
    public static void Solution(int part)
    {
        string line = File.ReadLines(@"D:\SCL33\Downloads\input").ToList()[0];

        long sum = 0;
        List<int> disk = [];
        for (int i = 0; i < line.Length; i++)
        {
            int n = line[i] - '0';
            for (int k = 0; k < n; k++)
            {
                if (i % 2 == 0)
                {
                    //disk.Add((char)(i / 2));
                    disk.Add(i / 2);
                }
                else
                {
                    disk.Add(-1);
                }
            }
        }

        // do swaps
        if (part == 1)
        {
            int r = disk.Count - 1;
            for (int l = 0; l < r; l++)
            {
                if (disk[l] == -1)
                {
                    while (disk[r] == -1)
                    {
                        r--;
                    }

                    disk[l] = disk[r];
                    disk[r] = -1;
                    r--;
                }
            }
        }
        else if (part == 2)
        {
            for (int r = disk.Count - 1; r >= 0; r--)
            {
                int currNum = disk[r];
                // "."
                if (currNum < 0)
                    continue;

                int size = 1;
                while (r - 1 >= 0 && disk[r - 1] == currNum)
                {
                    r--;
                    size++;
                }

                if (r == 0)
                    break;

                int currFreeSize = 0;
                for (int l = 0; l < r; l++)
                {
                    if (disk[l] < 0)
                    {
                        currFreeSize++;
                        if (currFreeSize == size)
                        {
                            for (int n = 0; n < size; n++)
                            {
                                disk[l - n] = currNum;
                                disk[r + n] = -1;
                            }

                            break;
                        }
                    }
                    else
                    {
                        currFreeSize = 0;
                    }
                }
            }
        }

        // get sum
        for (int i = 0; i < disk.Count; i++)
        {
            if (disk[i] < 0)
            {
                if (part == 1)
                    break;
                continue;
            }

            sum += i * disk[i];
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
10 - don't overthink
*/