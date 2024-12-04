
using System.Xml.Linq;

class Day4_2024
{
    public static void Solution(int part)
    {
        if (part == 1)
            Part1();
        else
            Part2();
    }

    static void Part1()
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];

                if (c == 'X')
                {
                    for (int xMove = -1; xMove <= 1; xMove++)
                    {
                        for (int yMove = -1; yMove <= 1; yMove++)
                        {
                            if (xMove == 0 && yMove == 0)
                            {
                                continue;
                            }

                            int currY = i;
                            int currX = k;
                            for (int n = 0; n < 3; n++) {
                                currY += yMove;
                                currX += xMove;
                                if (currY < 0 || currY >= lines.Count || currX < 0 || currX >= line.Length)
                                {
                                    break;
                                }

                                char nextChar = lines[currY][currX];

                                if (n == 0)
                                {
                                    if (nextChar != 'M')
                                    {
                                        break;
                                    }
                                }
                                else if (n == 1)
                                {
                                    if (nextChar != 'A')
                                    {
                                        break;
                                    }
                                }
                                else if (n == 2)
                                {
                                    if (nextChar == 'S')
                                    {
                                        sum++;
                                    }
                                }
                            }
                        }
                    } 
                }
            }
        }


        Console.WriteLine(sum);
    }

    static void Part2()
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        for (int i = 1; i < lines.Count - 1; i++)
        {
            string line = lines[i];
            for (int k = 1; k < line.Length - 1; k++)
            {
                char c = line[k];

                if (c == 'A')
                {
                    string aroundString = "";

                    aroundString += lines[i - 1][k - 1];
                    aroundString += lines[i - 1][k + 1];
                    aroundString += lines[i + 1][k + 1];
                    aroundString += lines[i + 1][k - 1];

                    if (aroundString == "SSMM" || 
                        aroundString == "MSSM" || 
                        aroundString == "MMSS" || 
                        aroundString == "SMMS")
                        sum++;
                }
            }
        }


        Console.WriteLine(sum);
    }
}

/*
line.Split(' ');
line.Trim().Replace("  ", " ");
*/