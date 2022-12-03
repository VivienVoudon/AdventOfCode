namespace ConsoleApp2.day3;

public static class Day3_2
{
    public static void Run(string input)
    {
        string ScoringRef = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int PriorityzeItem(char item)
        {
            return ScoringRef.IndexOf(item) + 1;
        }

        var rucksacks = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
            .Select(l => new Rucksack(l))
            .ToArray();

        int i = 0;
        var groups = new Dictionary<int, ElvesGroup>();
        foreach (var rucksack in rucksacks)
        {
            int groupNumber = i / 3;
            if (!groups.ContainsKey(groupNumber))
                groups.Add(groupNumber, new ElvesGroup());

            groups[groupNumber].AddRucksack(rucksack);
            i++;
        }

        Console.WriteLine(groups.Sum(r => PriorityzeItem(r.Value.Badge)));
    }

    public class ElvesGroup
    {
        private List<Rucksack> rucksacks = new List<Rucksack>();

        public ElvesGroup()
        {

        }

        public char Badge => rucksacks.First().Items.First(i => rucksacks.Skip(1).First().HaveItem(i) && rucksacks.Skip(2).First().HaveItem(i));

        public void AddRucksack(Rucksack rucksack)
        {
            this.rucksacks.Add(rucksack);
        }
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

        public string Items => comp1 + comp2;

        public bool HaveItem(char item)
        {
            return comp1.Any(i => i == item) || comp2.Any(i => i == item);
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

