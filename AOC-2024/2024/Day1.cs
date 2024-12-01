
class Day1
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        List<int> left = [];
        List<int> right = [];
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            string[] input = line.Split("   ");

            left.Add(int.Parse(input[0]));
            right.Add(int.Parse(input[1]));
        }

        left.Sort();
        right.Sort();

        for (int i = 0; i < left.Count; i++)
        {
            if (part == 1)
            {
                sum += Math.Abs(left[i] - right[i]);
            } else if (part == 2)
            {
                sum += left[i] * right.Count(r => r == left[i]);
            }
           
        }

        Console.WriteLine(sum);
    }
}
