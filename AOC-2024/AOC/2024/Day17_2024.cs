internal class Day17_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File
            .ReadLines(@"D:\SCL33\Downloads\input")
            .Select(line => line.Split(' ').Last())
            .ToList();

        long a = int.Parse(lines[0]);
        long b = int.Parse(lines[1]);
        long c = int.Parse(lines[2]);

        string programString = lines[4];
        List<int> program = programString.Split(',').Select(int.Parse).ToList();

        string output = "";
        if (part == 1)
        {
            output = RunProgram();

            Console.WriteLine(output);
        }
        else if (part == 2)
        {
            long n = 0;
            for (int j = 0; j < 1000000; j++)
            {
                //using StreamWriter writer = new(@"D:\SCL33\Downloads\output");
                bool found = false;
                for (int k = 0; k < 8; k++)
                {
                    a = n;
                    b = 0;
                    c = 0;
                    output = RunProgram();

                    if (programString.EndsWith(output))
                    {
                        found = true;
                        break;
                    }

                    n++;

                    //writer.WriteLine("A: " + n + "  output: " + output);
                    //Console.WriteLine("A: " + n + "  output: " + output);
                }

                if (!found)
                {
                    n >>= 3;
                }
                else if (output == programString)
                {
                    Console.WriteLine(n);
                    return;
                }
                else
                {
                    n <<= 3;
                }
            }
        }

        return;

        string RunProgram()
        {
            string sum = "";
            // i = instruction pointer
            for (int i = 0; i < program.Count; i += 2)
            {
                int instruction = program[i];
                // operands
                int literal = program[i + 1];
                long combo = literal switch
                {
                    0 or 1 or 2 or 3 => literal,
                    4 => a,
                    5 => b,
                    6 => c,
                    _ => 0
                };

                switch (instruction)
                {
                    case 0:
                        a /= (int)Math.Pow(2, combo);
                        break;
                    case 1:
                        b ^= literal;
                        break;
                    case 2:
                        b = combo % 8;
                        break;
                    case 3:
                        if (a != 0)
                            i = literal - 2;
                        break;
                    case 4:
                        b ^= c;
                        break;
                    case 5:
                        sum += combo % 8 + ",";
                        break;
                    case 6:
                        b = a / (int)Math.Pow(2, combo);
                        break;
                    case 7:
                        c = a / (int)Math.Pow(2, combo);
                        break;
                }
            }

            return sum.TrimEnd(',');
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