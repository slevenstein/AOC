
class Day2_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            if (i == 10)
                Console.WriteLine(lines[i]);
            string line = lines[i];
            List<int> nums = line.Split(' ').Select(int.Parse).ToList();

            for (int j = 0; j < 2; j++)
            {
                bool safe = true;
                bool isIncreasing = j == 0;
                bool removeUsed = part == 1;
                safe = IsLineSafe(isIncreasing, nums, removeUsed);
                if (safe)
                {
                    lines[i] += " 1";
                    sum++;
                    break;
                }
            }
        }

        Console.WriteLine(sum);
    }

    static bool IsLineSafe(bool isIncreasing, List<int> nums, bool removeUsed)
    {
        bool safe = true;
        for (int k = 0; k < nums.Count - 1; k++)
        {
            safe = true;
            int change = nums[k + 1] - nums[k];
            if (Math.Abs(change) > 3 ||
                change == 0 ||
                (change < 0 && isIncreasing) ||
                (change > 0 && !isIncreasing))
            {
                if (removeUsed)
                {
                    safe = false;
                    break;
                }
                else
                {
                    var firstRemoved = nums.ToList();
                    firstRemoved.RemoveAt(k);
                    var secondRemoved = nums.ToList();
                    secondRemoved.RemoveAt(k+1);

                    safe = IsLineSafe(isIncreasing, firstRemoved, true) ||
                        IsLineSafe(isIncreasing, secondRemoved, true);
                }

                if (!safe)
                    return false;
            }
        }
        return safe;
    }
}
