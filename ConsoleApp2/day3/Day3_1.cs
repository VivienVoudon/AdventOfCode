namespace ConsoleApp2.day3;

public static class Day3_1
{
    public static void Run(string input)
    {
        var rucksacks = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
            .Select(l => new Rucksack(l))
            .ToArray();
        
        Console.WriteLine(rucksacks.Sum(r => r.Score()));
    }

    public class Rucksack
    {
        private string comp1;
        private string comp2;

        public Rucksack(string input)
        {
            comp1 = input.Substring(0, input.Length / 2);
            comp2 = input.Substring(input.Length / 2, input.Length / 2);
        }

        private static string ScoringRef = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static int PriorityzeItem(char item)
        {
            return ScoringRef.IndexOf(item) + 1;
        }
        
        public int Score()
        {
            foreach (var item in comp1)
            {
                if (comp2.Contains(item))
                    return PriorityzeItem(item);
            }

            return 0;
        }
    }
}

