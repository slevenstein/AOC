using System;
using System.Drawing;
using System.Linq;

List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

List<(string, string)> connections = [];
for (int i = 0; i < lines.Count; i++)
{
    string line = lines[i];
    string[] strings = line.Split(' ');

    string start = strings[0].Trim(':');
    for (int k = 1; k < strings.Length; k++)
    {
        connections.Add((start, strings[k]));
    }
}


Dictionary<string, string[]> connectionsDict = [];
for (int i = 0; i < lines.Count; i++)
{
    string line = lines[i];
    string[] strings = line.Split(' ');

    connectionsDict.Add(strings[0].Trim(':'), strings[1..]);
}

Dictionary<string, string[]> leftConnections = connectionsDict.Take(connectionsDict.Count / 2).ToDictionary();
Dictionary<string, string[]> rightConnections = connectionsDict.Skip(connectionsDict.Count / 2).ToDictionary();

bool noSwaps = false;
int j = 0;
while(!noSwaps)
{
    noSwaps = true;

    foreach (var node in leftConnections)
    {
        int lefts = 0;
        foreach (var connection in node.Value)
        {
            if (leftConnections.ContainsKey(connection))
            {
                lefts++;
            }
        }
        if (lefts < node.Value.Length - lefts)
        {
            noSwaps = false;
            rightConnections.Add(node.Key, node.Value);
            leftConnections.Remove(node.Key);
        }
    }
    foreach (var node in rightConnections)
    {
        int lefts = 0;
        foreach (var connection in node.Value)
        {
            if (leftConnections.ContainsKey(connection))
            {
                lefts++;
            }
        }
        if (lefts > node.Value.Length - lefts)
        {
            noSwaps = false;
            leftConnections.Add(node.Key, node.Value);
            rightConnections.Remove(node.Key);
        }
    }
    if (j > 100)
    {
        Console.WriteLine();
    }
}


// Launch the graph visualization in a Windows Forms app
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new GraphForm(leftConnections, rightConnections));

Console.WriteLine("SUM: " + leftConnections.Count * rightConnections.Count);

class GraphForm : Form
{
    private readonly Dictionary<string, string[]> _connectionsDict;

    private readonly Dictionary<string, PointF> _nodePositions;

    public GraphForm(Dictionary<string, string[]> connectionsDict)
    {
        _connectionsDict = connectionsDict;
        _nodePositions = GenerateNodePositions();

        Text = "Graph Viewer";
        ClientSize = new Size(800, 600);
        BackColor = Color.White;
        DoubleBuffered = true; // Reduce flickering
    }

    public GraphForm(Dictionary<string, string[]> leftConnections, Dictionary<string, string[]> rightConnections)
    {
        _connectionsDict = leftConnections.Concat(rightConnections).ToDictionary();
        _nodePositions = GenerateNodePositions(isLeft: true, leftConnections).Concat(GenerateNodePositions(isLeft: false, rightConnections)).ToDictionary();

        Text = "Graph Viewer";
        ClientSize = new Size(800, 600);
        BackColor = Color.White;
        DoubleBuffered = true; // Reduce flickering
    }

    private Dictionary<string, PointF> GenerateNodePositions()
    {
        Random random = new();
        return _connectionsDict.ToDictionary(kvp => kvp.Key, kvp => new PointF(random.Next(10, 1300), random.Next(10, 1300)));
    }

    private static Dictionary<string, PointF> GenerateNodePositions(bool isLeft, Dictionary<string, string[]> connections)
    {
        Random random = new();
        return connections.ToDictionary(kvp => kvp.Key, kvp => isLeft ? new PointF(random.Next(10, 600), random.Next(10, 1300)) : new PointF(random.Next(700, 1300), random.Next(10, 1300)));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Draw edges (connections)
        foreach (var (start, ends) in _connectionsDict)
        {
            foreach (var end in ends)
            {
                if (_nodePositions.TryGetValue(start, out PointF pos1) && _nodePositions.TryGetValue(end, out PointF pos2))
                {
                    g.DrawLine(Pens.Black, pos1, pos2);
                }
            }
            
        }

        // Draw nodes
        foreach (var (node, position) in _nodePositions)
        {
            DrawNode(g, node, position);
        }
    }

    private static void DrawNode(Graphics g, string label, PointF position)
    {
        float nodeRadius = 10f;
        RectangleF nodeRect = new (position.X - nodeRadius, position.Y - nodeRadius, nodeRadius * 4, nodeRadius * 2);

        // Draw circle
        g.FillEllipse(Brushes.LightBlue, nodeRect);
        g.DrawEllipse(Pens.Black, nodeRect);

        // Draw label
        using Font font = new ("Arial", 10);
        StringFormat format = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString(label, font, Brushes.Black, nodeRect, format);
    }
}