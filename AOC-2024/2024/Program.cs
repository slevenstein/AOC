
class AOC
{
    static void Main(string[] args)
    {
        int day = int.Parse(args[0]);
        int part = int.Parse(args[1]);

        switch (day) {
            case 1:
                Day1.Solution(part);
                break;
            case 2:
                Day2.Solution(part);
                break;
        }

    }
}

