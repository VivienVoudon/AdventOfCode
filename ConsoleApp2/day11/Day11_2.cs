﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.day11.Day11_2;

namespace ConsoleApp2.day11
{
    internal class Day11_2:Day
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

        //List<Monkey> monkeys = new List<Monkey>
        //{
        //    new Monkey(new List<BigInteger>() { 79, 98 }, i => i * 19, 23, 3, 2),
        //    new Monkey(new List<BigInteger>() { 54, 65, 75, 74 }, i => i + 6, 19, 0, 2),
        //    new Monkey(new List<BigInteger>() { 79, 60, 97 }, i => i * i, 13, 3, 1),
        //    new Monkey(new List<BigInteger>() { 74 }, i => i + 3, 17, 1, 0),
        //};
        //ConcurrentBag<int> monkeyUsed = new ConcurrentBag<int>();

        //private int[] counters = new int[4];

        protected override void Run(string input)
        {
            //for (var iMonkey = 0; iMonkey < 4; iMonkey++)
            //{
            //    Parallel.ForEach(monkeys[iMonkey].Items, new ParallelOptions()
            //    {
            //        MaxDegreeOfParallelism = 1
            //    }, item =>
            //    {
            //        (BigInteger item, int iMonkey) some = (item, iMonkey);
            //        for (var round = 1; round <= 10000; round++)
            //        {
            //            if(round % 10 == 0)
            //                Console.WriteLine(round);
            //            some = glop(some.iMonkey, some.item);
            //        }
            //    });
            //}

            //foreach (var o in monkeyUsed.GroupBy(m => m))
            //{
            //    monkeys[o.Key].InspectCount += o.Count();
            //}

            //var goodWorkers = monkeys.OrderByDescending(m => m.InspectCount).Take(2).ToArray();
            //Console.WriteLine(goodWorkers[0].InspectCount * goodWorkers[1].InspectCount);

            var monkeys = new[]
            {
                new Monkey(new List<Item>() { new(76), new(88), new(96), new(97), new(58), new(61), new(67) }, i => i.Multiply(19), 3, 3, 2),
                new Monkey(new List<Item>() { new(93), new(71), new(79), new(83), new(69), new(70), new(94), new(98) }, i => i.Add(8), 11, 6, 5),
                new Monkey(new List<Item>() { new(50), new(74), new(67), new(92), new(61), new(76) }, i => i.Multiply(13), 19, 1, 3),
                new Monkey(new List<Item>() { new(76), new(92) }, i => i.Add(6), 5, 6, 1),
                new Monkey(new List<Item>() { new(74), new(94), new(55), new(87), new(62) }, i => i.Add(5), 2, 0, 2),
                new Monkey(new List<Item>() { new(59), new(62), new(53), new(62) }, i => i.Square(), 7, 7, 4),
                new Monkey(new List<Item>() { new(62)}, i => i.Add(2), 17, 7, 5),
                new Monkey(new List<Item>() { new(85), new(54), new(53) }, i => i.Add(3), 13, 0, 4),
            };

            //var monkeys = new[]
            //{
            //    new Monkey(new List<Item>() { new(79), new(98) }, i => i.Multiply(19), 23, 3, 2),
            //    new Monkey(new List<Item>() { new(54), new(65), new(75), new(74) }, i => i.Add(6), 19, 0, 2),
            //    new Monkey(new List<Item>() { new(79), new(60), new(97) }, i => i.Square(), 13, 3, 1),
            //    new Monkey(new List<Item>() { new(74) }, i => i.Add(3), 17, 1, 0),
            //};

            var timer = Stopwatch.StartNew();

            for (var round = 1; round <= 10000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Items)
                    {
                        monkey.InspectCount++;
                        monkey.Operate(item);
                        //newItem = newItem % newItem;
                        //newItem = newItem / 3;
                        var target = item.Divisors.First(o => o.divisor == monkey.DivisibleBy).reste == 0 ? monkey.TrueTarget : monkey.FalseTarget;
                        monkeys[target].Items.Add(item);
                    }
                    
                    monkey.Items.Clear();
                }



                if (round % 100 == 0)
                    Console.WriteLine($"{round} : {timer.Elapsed.TotalSeconds}");
            }

            var goodWorkers = monkeys.OrderByDescending(m => m.InspectCount).Take(2).ToArray();
            Console.WriteLine(goodWorkers[0].InspectCount * goodWorkers[1].InspectCount);
        }

        //public (BigInteger item, int iMonkey) glop(int currentMonkey, BigInteger item)
        //{
        //    counters[currentMonkey]++;
        //    //monkeyUsed.Add(currentMonkey);
        //    var newItem = monkeys[currentMonkey].Operate(item);
        //    newItem = newItem / 3;
        //    var targetMonkey = newItem % monkeys[currentMonkey].DivisibleBy == 0 ? monkeys[currentMonkey].TrueTarget : monkeys[currentMonkey].FalseTarget;

        //    if (targetMonkey > currentMonkey)
        //        return glop(targetMonkey, newItem);

        //    return (newItem, targetMonkey);
        //}

        public class Monkey
        {
            public List<Item> Items;
            public Action<Item> Operate;
            public int DivisibleBy;
            public int FalseTarget;
            public int TrueTarget;

            public long InspectCount = 0;

            public Monkey(List<Item> items, Action<Item> operation, int divisibleBy, int falseTarget, int trueTarget)
            {
                Items = items;
                Operate = operation;
                DivisibleBy = divisibleBy;
                FalseTarget = falseTarget;
                TrueTarget = trueTarget;
            }

            public Dictionary<int, int> Coefficient;
        }

        public class Item
        {
            public (int divisor, int reste)[] Divisors;

            public Item(int item)
            {
                Divisors = new[]
                {
                    ( 3, item % 3),
                    ( 19, item % 19 ),
                    ( 2, item % 2 ),
                    ( 7, item % 7 ),
                    ( 17, item % 17 ),
                    ( 13, item % 13 ),
                    ( 5, item % 5 ),
                    ( 11, item % 11 ),
                };
            }

            public void Multiply(int number)
            {
                for (var i = 0; i < Divisors.Length; i++)
                {
                    Divisors[i].reste *= (number % Divisors[i].divisor);
                    Divisors[i].reste %= Divisors[i].divisor;
                }
            }

            public void Add(int number)
            {
                for (var i = 0; i < Divisors.Length; i++)
                {
                    Divisors[i].reste += (number % Divisors[i].divisor);
                    Divisors[i].reste %= Divisors[i].divisor;
                }
            }

            public void Square()
            {
                for (var i = 0; i < Divisors.Length; i++)
                {
                    Divisors[i].reste *= Divisors[i].reste;
                    Divisors[i].reste %= Divisors[i].divisor;
                }
            }
        }
    }

    
}
