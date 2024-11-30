using System.Collections.Generic;

bool isPart2 = true;

List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

int sum = 0;
List<Pulse> start = new();
Dictionary<string, Module> modDict = [];
for (int i = 0; i < lines.Count; i++)
{
    string line = lines[i];
    for (int w = 0; w < 2; w++)
        line = line.Trim().Replace("  ", " ");

    string[] strings = line.Split(' ');
    string src = strings[0];
    List<string> destinations = strings.Skip(2).Select(s => s.TrimEnd(',')).ToList();
    if (src == "broadcaster")
    {
        start = new(destinations.Select(d => new Pulse { src = "", dest = d, isHigh = false }));
    }
    else
    {
        Module mod;
        if (src[0] == '%')
        {
            mod = new FlipFlop(destinations);
        }
        else if (src[0] == '&')
        {
            mod = new Conjunction(destinations);
        }
        else
        {
            throw new Exception();
        }

        modDict.Add(src[1..], mod);
    }
}

// add all sources to conjunctions
foreach (KeyValuePair<string, Module> pair in modDict)
{
    Module mod = pair.Value;
    foreach (string dest in mod.destinations)
    {
        if (modDict.TryGetValue(dest, out Module? destModule))
        {
            if (destModule is Conjunction conjModule)
            {
                conjModule.rcvPulses.Add(pair.Key, false);
            }
        }
    }
}

//bool pulse = false; // high == true, low == false
int highs = 0;
int lows = 0;

//part2
int sourcesFound = 0;
Dictionary<string, int> rxSources = new() { { "ln", 0 }, { "db", 0 }, { "vq", 0 }, { "tf", 0 } };

for (int i = 0; i < 1000 || isPart2; i++)
{
    Queue<Pulse> modQueue = new(start);
    lows++;
    while (modQueue.Count != 0)
    {
        Pulse pulse = modQueue.Dequeue();
        if (pulse.isHigh)
        {
            highs++;
        }
        else
        {
            lows++;
        }

        if (pulse.dest == "rx" && !pulse.isHigh)
            Console.WriteLine("min press: " + i);

        if (!modDict.TryGetValue(pulse.dest, out Module? curr))
            continue;

        bool isSendHigh;

        // flip flop
        if (curr is FlipFlop currFF)
        {
            if (pulse.isHigh)
            {
                continue;
            }
            currFF.on ^= true;
            isSendHigh = currFF.on;

        }
        // conjunction
        else if (curr is Conjunction currConj)
        {
            currConj.rcvPulses[pulse.src] = pulse.isHigh;
            isSendHigh = currConj.rcvPulses.Any(r => !r.Value);

            if (isSendHigh)
            {
                if (rxSources.TryGetValue(pulse.dest, out int value) && value == 0)
                {
                    rxSources[pulse.dest] = i;
                    sourcesFound++;
                }
            }
        }

        else
            throw new Exception();

        foreach (string dest in curr.destinations)
        {
            modQueue.Enqueue(new Pulse(pulse.dest, dest, isSendHigh));
        }
    }

    if (sourcesFound == 4)
        break;
}

Console.WriteLine(modDict);

// part2
long sum2 = 1;
foreach (int minPresses in rxSources.Values)
{
    sum2 *= (minPresses + 1);
}
Console.WriteLine(sum2);

sum = highs * lows;
Console.WriteLine(modDict);
Console.WriteLine(sum);


struct Pulse(string source, string destination, bool isPulseHigh)
{
    public string src = source;
    public string dest = destination;
    public bool isHigh = isPulseHigh;
}

public abstract class Module(List<string> outputs)
{
    public List<string> destinations = outputs;
}

public class FlipFlop(List<string> outputs) : Module(outputs)
{
    public bool on = false;
}

public class Conjunction(List<string> outputs) : Module(outputs)
{
    public Dictionary<string, bool> rcvPulses = []; // high == true, low == false
}

/*
Help:
3-2 - debugging help -> add *'s to border
5-2 - hint -> reverse
8-2 - hint -> LCM
10-2 - hint -> rasterization
12-2 - hint -> DP/cache, dictionary key to string
	 - bug
14-2 - bug
17-1 - Dijkstra’s refresher
*/


/*
Help:
3-2 - debugging help -> add *'s to border
5-2 - hint -> reverse
8-2 - hint -> LCM
10-2 - hint -> rasterization
12-2 - hint -> DP/cache, dictionary key to string
	 - bug
14-2 - bug
17-1 - Dijkstra’s refresher
*/