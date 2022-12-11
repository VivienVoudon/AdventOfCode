using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.day10
{
    public class Day10_2 : Day
    {
        protected override string testInput { get; } = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";
        protected override string input { get; } = @"addx 1
addx 4
addx 1
noop
addx 4
addx 3
addx -2
addx 5
addx -1
noop
addx 3
noop
addx 7
addx -1
addx 1
noop
addx 6
addx -1
addx 5
noop
noop
noop
addx -37
addx 7
noop
noop
noop
addx 5
noop
noop
noop
addx 9
addx -8
addx 2
addx 5
addx 2
addx 5
noop
noop
addx -2
noop
addx 3
addx 2
noop
addx 3
addx 2
noop
addx 3
addx -36
noop
addx 26
addx -21
noop
noop
noop
addx 3
addx 5
addx 2
addx -4
noop
addx 9
addx 5
noop
noop
noop
addx -6
addx 7
addx 2
noop
addx 3
addx 2
addx 5
addx -39
addx 34
addx 5
addx -35
noop
addx 26
addx -21
addx 5
addx 2
addx 2
noop
addx 3
addx 12
addx -7
noop
noop
noop
noop
noop
addx 5
addx 2
addx 3
noop
noop
noop
noop
addx -37
addx 21
addx -14
addx 16
addx -11
noop
addx -2
addx 3
addx 2
addx 5
addx 2
addx -15
addx 6
addx 12
addx -2
addx 9
addx -6
addx 7
addx 2
noop
noop
noop
addx -33
addx 1
noop
addx 2
addx 13
addx 15
addx -21
addx 21
addx -15
noop
noop
addx 4
addx 1
noop
addx 4
addx 8
addx 6
addx -11
addx 5
addx 2
addx -35
addx -1
noop
noop";

        private int Cycle = 0;
        private int X = 1;
        private Dictionary<int, int> SignalStrengh = new Dictionary<int, int>();
        
        protected override void Run(string input)
        {
            Screen = new char[6][];
            Screen[0] = new char[40];
            Screen[1] = new char[40];
            Screen[2] = new char[40];
            Screen[3] = new char[40];
            Screen[4] = new char[40];
            Screen[5] = new char[40];

            var instructions = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            
            foreach (var instruction in instructions)
            {
                noop();
                if(instruction.StartsWith("addx"))
                {
                    // second cycle
                    noop();
                    var addedValue = int.Parse(instruction.Split(" ")[1]);
                    X += addedValue;
                }
            }

            foreach (var c in Screen)
                Console.WriteLine(c);


            //Console.WriteLine(SignalStrengh.Sum(x => x.Key * x.Value));

        }

        private void SaveSignalStrength()
        {
            if ((Cycle - 20) % 40 == 0)
                SignalStrengh.Add(Cycle, X);
        }

        private char[][] Screen;
        private void noop()
        {
            var screenY = Cycle / 40;
            var screenX = Cycle % 40;

            Screen[screenY][screenX] = '.';
            if(Math.Abs(X - screenX) <= 1)
                Screen[screenY][screenX] = '#';

            Cycle++;
            //SaveSignalStrength();
        }
    }
}
