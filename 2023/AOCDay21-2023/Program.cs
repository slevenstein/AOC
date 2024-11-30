bool testDouble = false;
List<List<char>> lines = File.ReadLines(@"D:\SCL33\Downloads\input").Select(s => (testDouble ? s+s : s).ToList()).ToList();

long part2Sum = (202300L + 1) * (202300 + 1) * 7315 + 202300L * 202300 * 7354 - (202300 + 1) * 3653 + 202300 * 3683;

int sumOddSteps = 0;
int sumEvenSteps = 1;
int startPrint = 65; // use this for debugging
// A - alternating (tells loop to ignore changing)
int step;
for (step = 0; step < 300; step++)
{
    int sumPlots = 0;
    List<List<char>> copy = lines.Select(x => x.ToList()).ToList();
    for (int i = 0; i < lines.Count; i++)
    {
        List<char> line = copy[i];
        for (int k = 0; k < line.Count; k++)
        {
            if (step >= startPrint)
                Console.Write(line[k]);

            if (line[k] == 'O' || (line[k] == 'S' && k < 131))
            {
                sumPlots++;
                lines[i][k] = '.';

                Spread(i + 1, k);
                Spread(i - 1, k);
                Spread(i, k + 1);
                Spread(i, k - 1);
            }
        }
        if (step >= startPrint) 
            Console.WriteLine();
    }
    if (step >= startPrint) 
        Console.WriteLine();
    //Console.WriteLine(step);
    //Console.WriteLine(lines.Select(charList => new string(charList.ToArray())).ToList());
}



Console.WriteLine(sumEvenSteps);
//Console.WriteLine(lines);


void Spread(int y, int x)
{
    if (y >= 0 && x >= 0 && y < lines.Count && x < lines[0].Count) {
        if (lines[y][x] == '.')
        {
            lines[y][x] = 'O';
            if (step % 2 == 0)
            {
                sumEvenSteps++;
            }
            else
            {
                sumOddSteps++;
            }
        }
    }
}

// https://github.com/villuna/aoc23/wiki/A-Geometric-solution-to-advent-of-code-2023,-day-21

// diamond radius = 26501365 
// n = (26501365 - 65) / 131 = 202300 plots to left/right/up/down of center

/*
even = 7354
odd = 7315

odd - corners = 3701
even - corners = 3632

odd corners = 3653
even corners = 3683

(26501365 + 1)^2 x 7315 + 26501365^2 x 7354 - (26501365 + 1) * 3653 + 26501365 x 3683
*/