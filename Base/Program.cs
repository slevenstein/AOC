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


/*
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