List<List<char>> lines = File.ReadLines(@"D:\SCL33\Downloads\input.txt").Select(s => s.ToList()).ToList();

var visited = new List<(int y, int x)>();
var newVisited = new List<(int y, int x)> { GetStart() };

int sumOddSteps = 0;
int sumEvenSteps = 1;
// A - alternating (tells loop to ignore changing)
int step;
int totalSteps = 100;
for (step = 1; step <= totalSteps; step++)
{
    var copy = newVisited.Select(v => (v.y, v.x)).ToList();
    foreach (var node in copy)
    {
        Spread(node.y + 1, node.x);
        Spread(node.y - 1, node.x);
        Spread(node.y, node.x + 1);
        Spread(node.y, node.x - 1);
    }

    visited.Clear();
    foreach (var node in copy)
    {
        visited.Add(node); //lines[i][k] = 'A';
        newVisited.Remove(node);
    }
    //Console.WriteLine(step);
    //Console.WriteLine(visited);
    //Console.WriteLine(lines.Select(charList => new string(charList.ToArray())).ToList());
}

Console.WriteLine((totalSteps % 2 == 0) ? sumEvenSteps : sumOddSteps);
//Console.WriteLine(lines);


void Spread(int y, int x)
{
    try
    {
        if (lines[mod(y, lines.Count)][mod(x, lines[0].Count)] != '#' && !visited.Any(v => v.y == y && v.x == x) && !newVisited.Any(v => v.y == y && v.x == x))
        {
            newVisited.Add((y, x));
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
        Console.WriteLine(y);
        Console.WriteLine(x);
        throw;
    }
}

(int y, int x) GetStart()
{
    for (int i = 0; i < lines.Count; i++)
    {
        List<char> line = lines[i];
        for (int k = 0; k < lines.Count; k++)
        {
            if (line[k] == 'S') return (i, k);
        }
    }
    throw new Exception();
}

int mod(int x, int m)
{
    return (x % m + m) % m;
}