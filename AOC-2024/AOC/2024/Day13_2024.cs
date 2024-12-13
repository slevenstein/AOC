internal class Day13_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        long sum = 0;
        for (int i = 0; i < lines.Count; i += 4)
        {
            // Button A: X+94, Y+34
            string[] buttonA = lines[i].Split(' ');
            string[] buttonB = lines[i + 1].Split(' ');
            string[] prize = lines[i + 2].Split(' ');

            (int x, int y) a = (ParseNum(buttonA[2]), ParseNum(buttonA[3]));
            (int x, int y) b = (ParseNum(buttonB[2]), ParseNum(buttonB[3]));
            (long x, long y) p = part == 1
                ? (ParseNum(prize[1]), ParseNum(prize[2]))
                : (ParseAndPad(prize[1]), ParseAndPad(prize[2]));

            // Cramer's Rule
            long aNominator = p.x * b.y - b.x * p.y;
            int aDenominator = a.x * b.y - a.y * b.x;
            if (aNominator % aDenominator != 0)
                continue;

            long bNominator = a.x * p.y - p.x * a.y;
            int bDenominator = a.x * b.y - b.x * a.y;
            if (bNominator % bDenominator != 0)
                continue;

            long aPresses = aNominator / aDenominator;
            long bPresses = bNominator / bDenominator;

            if (aPresses >= 0 && bPresses >= 0)
                sum += aPresses * 3 + bPresses;
        }

        Console.WriteLine(sum);
    }

    private static int ParseNum(string s)
    {
        return int.Parse(s[2..].Trim(','));
    }

    private static long ParseAndPad(string s)
    {
        string trimmedString = s[2..].Trim(',');
        return long.Parse(
            "1" + 
            new string('0', 13 - trimmedString.Length) + 
            trimmedString);
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