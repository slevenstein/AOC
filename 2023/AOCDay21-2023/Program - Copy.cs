class Program2
{
    static void Main()
    {

        List<List<char>> lines = File.ReadLines(@"D:\SCL33\Downloads\input").Select(s => s.ToList()).ToList();

        int sumOddSteps = 0;
        int sumEvenSteps = 1;
        // A - alternating (tells loop to ignore changing)
        int step;
        for (step = 1; step <= 64; step++)
        {
            List<List<char>> copy = lines.Select(x => x.ToList()).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                List<char> line = copy[i];
                for (int k = 0; k < lines.Count; k++)
                {
                    if (step > 63)
                        Console.Write(line[k]);
                    if (line[k] == 'O' || line[k] == 'S')
                    {
                        lines[i][k] = 'A';

                        Spread(i + 1, k);
                        Spread(i - 1, k);
                        Spread(i, k + 1);
                        Spread(i, k - 1);
                    }

                }
                if (step > 63)
                    Console.WriteLine();
            }
            if (step > 63)
                Console.WriteLine();
            //Console.WriteLine(step);
            //Console.WriteLine(lines.Select(charList => new string(charList.ToArray())).ToList());
        }

        Console.WriteLine(sumEvenSteps);
        //Console.WriteLine(lines);


        void Spread(int y, int x)
        {
            try
            {
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
            catch (Exception)
            {
            };
        }
    }
}