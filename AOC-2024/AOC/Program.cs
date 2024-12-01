
class AOC
{
    // example input: 2024 1 1
    public static void Main(string[] args)
    {
        int year = int.Parse(args[0]);
        int day = int.Parse(args[1]);
        int part = int.Parse(args[2]);

        // Construct the class name dynamically
        string className = $"Day{day}_{year}";

        // Get the type of the class
        Type? type = Type.GetType(className);

        // Invoke the method if the class and method exist
        type?.GetMethod("Solution")?.Invoke(null, new object[] { part });
    }
}

