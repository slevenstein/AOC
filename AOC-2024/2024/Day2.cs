
class Day2
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int w = 0; w < 2; w++)
                line = line.Trim().Replace("  ", " ");

            for (int k = 0; k < lines.Count; k++)
            {

            }

            string[] strings = line.Split(' ');
        }


        Console.WriteLine(sum);
    }
}
