
class Day3_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        bool isDo = true;
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];

            bool mulFound = false;
            bool onFirstNum = false;
            bool onSecondNum = false;
            string firstNum = "";
            string secondNum = "";
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];
                if (Find("don't()"))
                {
                    isDo = false;
                }
                else if (Find("do()"))
                {
                    isDo = true;
                }
                else if ((isDo || part == 1) && Find("mul"))
                {
                    mulFound = true;
                }
                else if (mulFound)
                {
                    if (!onFirstNum && !onSecondNum)
                    {
                        if (c == '(')
                        {
                            onFirstNum = true;
                        }
                        else
                        {
                            mulFound = false;
                        }
                    }
                    else if (onFirstNum && c == ',')
                    {
                        onFirstNum = false;
                        onSecondNum = true;
                    }
                    else if (onSecondNum && c == ')')
                    {
                        sum += int.Parse(firstNum) * int.Parse(secondNum);
                        Reset();
                    }
                    else if (!char.IsNumber(c)) {
                        Reset();
                    }
                    else if (onFirstNum)
                    {
                        firstNum += c;
                    }
                    else if (onSecondNum)
                    {
                        secondNum += c;
                    }
                }

                bool Find(string word)
                {
                    int size = word.Length;
                    if(k + size < line.Length && line[k..(k + size)] == word)
                    {
                        k += size - 1;
                        return true;
                    }
                    return false;

                } 
                void Reset()
                {
                    mulFound = false;
                    firstNum = "";
                    secondNum = "";
                    onFirstNum = false;
                    onSecondNum = false;
                }
                
            }
        }

        Console.WriteLine(sum);
    }
}