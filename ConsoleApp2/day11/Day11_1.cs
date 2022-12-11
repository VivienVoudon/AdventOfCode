using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.day11
{
    internal class Day11_1:Day
    {
        protected override string testInput { get; } = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

        protected override string input { get; } = @"Monkey 0:
  Starting items: 76, 88, 96, 97, 58, 61, 67
  Operation: new = old * 19
  Test: divisible by 3
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 93, 71, 79, 83, 69, 70, 94, 98
  Operation: new = old + 8
  Test: divisible by 11
    If true: throw to monkey 5
    If false: throw to monkey 6

Monkey 2:
  Starting items: 50, 74, 67, 92, 61, 76
  Operation: new = old * 13
  Test: divisible by 19
    If true: throw to monkey 3
    If false: throw to monkey 1

Monkey 3:
  Starting items: 76, 92
  Operation: new = old + 6
  Test: divisible by 5
    If true: throw to monkey 1
    If false: throw to monkey 6

Monkey 4:
  Starting items: 74, 94, 55, 87, 62
  Operation: new = old + 5
  Test: divisible by 2
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 5:
  Starting items: 59, 62, 53, 62
  Operation: new = old * old
  Test: divisible by 7
    If true: throw to monkey 4
    If false: throw to monkey 7

Monkey 6:
  Starting items: 62
  Operation: new = old + 2
  Test: divisible by 17
    If true: throw to monkey 5
    If false: throw to monkey 7

Monkey 7:
  Starting items: 85, 54, 53
  Operation: new = old + 3
  Test: divisible by 13
    If true: throw to monkey 4
    If false: throw to monkey 0";

        protected override void Run(string input)
        {
            //var monkeys = new[]
            //{
            //    new Monkey(new List<int>() { 76, 88, 96, 97, 58, 61, 67 }, i => i * 19, 3, 3, 2),
            //    new Monkey(new List<int>() { 93, 71, 79, 83, 69, 70, 94, 98 }, i => i + 8, 11, 6, 5),
            //    new Monkey(new List<int>() { 50, 74, 67, 92, 61, 76 }, i => i * 13, 19, 1, 3),
            //    new Monkey(new List<int>() { 76, 92 }, i => i + 6, 5, 6, 1),
            //    new Monkey(new List<int>() { 74, 94, 55, 87, 62 }, i => i + 5, 2, 0, 2),
            //    new Monkey(new List<int>() { 59, 62, 53, 62 }, i => i * i, 7, 7, 4),
            //    new Monkey(new List<int>() { 62}, i => i + 2, 17, 7, 5),
            //    new Monkey(new List<int>() { 85, 54, 53 }, i => i + 3, 13, 0, 4),
            //};

            var monkeys = new[]
            {
                new Monkey(new List<int>() { 79, 98 }, i => i * 19, 23, 3, 2),
                new Monkey(new List<int>() { 54, 65, 75, 74 }, i => i + 6, 19, 0, 2),
                new Monkey(new List<int>() { 79, 60, 97 }, i => i * i, 13, 3, 1),
                new Monkey(new List<int>() { 74 }, i => i + 3, 17, 1, 0),
            };

            for (var round = 1; round <= 10000; round++)
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Items)
                    {
                        monkey.InspectCount++;
                        var newItem = monkey.Operate(item);
                        newItem = newItem / 3;
                        var target = newItem % monkey.DivisibleBy == 0 ? monkey.TrueTarget : monkey.FalseTarget;
                        monkeys[target].Items.Add(newItem);
                    }

                    monkey.Items.Clear();
                }

            var goodWorkers = monkeys.OrderByDescending(m => m.InspectCount).Take(2).ToArray();
            Console.WriteLine(goodWorkers[0].InspectCount * goodWorkers[1].InspectCount);
        }

        public class Monkey
        {
            public List<int> Items;
            public Func<int, int> Operate;
            public int DivisibleBy;
            public int FalseTarget;
            public int TrueTarget;

            public int InspectCount = 0;

            public Monkey(List<int> items, Func<int, int> operation, int divisibleBy, int falseTarget, int trueTarget)
            {
                Items = items;
                Operate = operation;
                DivisibleBy = divisibleBy;
                FalseTarget = falseTarget;
                TrueTarget = trueTarget;
            }
        }
    }

   
}
