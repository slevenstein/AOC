
class Day5_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        Dictionary<int, List<int>> map = [];
        int sum = 0;
        bool endMapRead = false;
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            if (!endMapRead)
            {
                if (line == "")
                {
                    endMapRead = true;
                    continue;
                }

                string[] strings = line.Split('|');
                int left = int.Parse(strings[0]);
                int right = int.Parse(strings[1]);
                if (map.TryGetValue(left, out List<int>? value))
                {
                    value.Add(right);
                }
                else
                {
                    map.Add(left, [right]);
                }
            }
            else
            {
                bool failed = false;
                int[] pages = line.Split(',').Select(int.Parse).ToArray();
                for (int j = 0; j < pages.Length-1; j++)
                {
                    map.TryGetValue(pages[j], out List<int>? right);
                    if (right == null)
                    {
                        failed = true;
                        break;
                    }
                    for (int k = j+1; k < pages.Length; k++)
                    {
                        if (!right.Contains(pages[k]))
                        {
                            failed = true;
                            break;
                        }
                    }

                    if (failed)
                    {
                        break;
                    }
                }

                if (!failed)
                {
                    if (part == 1)
                        sum += pages[(pages.Length - 1) / 2];
                }
                else
                {
                    if (part == 2)
                    {
                        Array.Sort(pages, 
                            (a, b) => 
                            map.TryGetValue(a, out List<int>? value) && 
                            value.Contains(b) ? -1 : 1);

                        sum += pages[(pages.Length - 1) / 2];
                    }
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

*/