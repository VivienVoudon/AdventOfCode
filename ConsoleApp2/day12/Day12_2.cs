using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.day12
{
    internal class Day12_2 : Day
    {
        protected override string testInput { get; } = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

        protected override string input { get; } = @"abccccaaaaaaacccaaaaaaaccccccccccccccccccccccccccccccccccaaaa
abcccccaaaaaacccaaaaaaaaaaccccccccccccccccccccccccccccccaaaaa
abccaaaaaaaaccaaaaaaaaaaaaaccccccccccccccccccccccccccccaaaaaa
abccaaaaaaaaaaaaaaaaaaaaaaacccccccccaaaccccacccccccccccaaacaa
abaccaaaaaaaaaaaaaaaaaacacacccccccccaaacccaaaccccccccccccccaa
abaccccaaaaaaaaaaaaaaaacccccccccccccaaaaaaaaaccccccccccccccaa
abaccccaacccccccccaaaaaacccccccccccccaaaaaaaacccccccccccccccc
abcccccaaaacccccccaaaaaaccccccccijjjjjjaaaaaccccccaaccaaccccc
abccccccaaaaacccccaaaacccccccciiijjjjjjjjjkkkkkkccaaaaaaccccc
abcccccaaaaacccccccccccccccccciiiirrrjjjjjkkkkkkkkaaaaaaccccc
abcccccaaaaaccccccccccccccccciiiirrrrrrjjjkkkkkkkkkaaaaaccccc
abaaccacaaaaacccccccccccccccciiiqrrrrrrrrrrssssskkkkaaaaacccc
abaaaaacaaccccccccccccccccccciiiqqrtuurrrrrsssssskklaaaaacccc
abaaaaacccccccccccaaccccccccciiqqqttuuuurrssusssslllaaccccccc
abaaaaaccccccccaaaaccccccccciiiqqqttuuuuuuuuuuusslllaaccccccc
abaaaaaacccccccaaaaaaccccccciiiqqqttxxxuuuuuuuusslllccccccccc
abaaaaaaccccaaccaaaaacccccchhiiqqtttxxxxuyyyyvvsslllccccccccc
abaaacacccccaacaaaaaccccccchhhqqqqttxxxxxyyyyvvsslllccccccccc
abaaacccccccaaaaaaaacccccchhhqqqqtttxxxxxyyyvvssqlllccccccccc
abacccccaaaaaaaaaaccaaacchhhpqqqtttxxxxxyyyyvvqqqlllccccccccc
SbaaacaaaaaaaaaaaacaaaaahhhhppttttxxEzzzzyyvvvqqqqlllcccccccc
abaaaaaaacaaaaaacccaaaaahhhppptttxxxxxyyyyyyyvvqqqlllcccccccc
abaaaaaaccaaaaaaaccaaaaahhhppptttxxxxywyyyyyyvvvqqqmmcccccccc
abaaaaaaacaaaaaaacccaaaahhhpppsssxxwwwyyyyyyvvvvqqqmmmccccccc
abaaaaaaaaaaaaaaacccaacahhhpppssssssswyyywwvvvvvqqqmmmccccccc
abaaaaaaaacacaaaacccccccgggppppsssssswwywwwwvvvqqqqmmmccccccc
abcaaacaaaccccaaaccccccccgggppppppssswwwwwrrrrrqqqmmmmccccccc
abcaaacccccccccccccccccccgggggpppoosswwwwwrrrrrqqmmmmddcccccc
abccaacccccccccccccccccccccgggggoooosswwwrrrnnnmmmmmddddccccc
abccccccccccccccccccccccccccgggggooossrrrrrnnnnnmmmddddaccccc
abaccccaacccccccccccccccccccccgggfoossrrrrnnnnndddddddaaacccc
abaccaaaaaaccccccccccccccccccccgffooorrrrnnnneeddddddaaaacccc
abaccaaaaaacccccccccccccccccccccfffooooonnnneeeddddaaaacccccc
abacccaaaaaccccccccaaccaaaccccccffffoooonnneeeeccaaaaaacccccc
abcccaaaaacccccccccaaccaaaaccccccffffoooneeeeeaccccccaacccccc
abaccaaaaaccccccccaaaacaaaaccccccafffffeeeeeaaacccccccccccccc
abacccccccccccccccaaaacaaacccccccccffffeeeecccccccccccccccaac
abaaaacccccccaaaaaaaaaaaaaacccccccccfffeeeccccccccccccccccaaa
abaaaacccccccaaaaaaaaaaaaaaccccccccccccaacccccccccccccccccaaa
abaacccccccccaaaaaaaaaaaaaaccccccccccccaacccccccccccccccaaaaa
abaaaccccccccccaaaaaaaaccccccccccccccccccccccccccccccccaaaaaa";

        private string[] Grid;
        private Point Target;
        private char TargetElevation = 'z';

        protected override void Run(string input)
        {
            Grid = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var myPosition = new Point(0, 0);
            Target = new Point(0, 0);

            var paths = new List<List<Point>>();

            
            for (var y = 0; y < Grid.Length; y++)
                for (var x = 0; x < Grid[y].Length; x++)
                {
                    if (Grid[y][x] == 'a' || Grid[y][x] == 'S')
                        paths.Add(new List<Point>() { new(x, y) });
                        //myPosition = new(x, y);
                    if (Grid[y][x] == 'E') Target = new(x, y);
                }

          
            var visitedPoint = new List<Point>() { new Point(myPosition.X, myPosition.Y) };
            int? result = null;
            while (!result.HasValue)
            {
                Console.WriteLine($"new Boucle: {paths.Count} paths to explore");
                var newPathToExplore = new List<List<Point>>();
                foreach (var path in paths)
                {
                    myPosition = path.Last();
                    //Console.WriteLine($"{path.path.Count} length is exploring ...");
                    // test foreach direction if it's possible
                    foreach (var direction in (Direction[])Enum.GetValues(typeof(Direction)))
                    {
                        // is it possible ?
                        var newPoint = direction switch
                        {
                            Direction.East => new Point(myPosition.X + 1, myPosition.Y),
                            Direction.North => new Point(myPosition.X, myPosition.Y - 1),
                            Direction.West => new Point(myPosition.X - 1, myPosition.Y),
                            Direction.South => new Point(myPosition.X, myPosition.Y + 1),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        // point out of the grid
                        if (newPoint.X < 0 || newPoint.X > Grid[0].Length - 1
                                           || newPoint.Y < 0 || newPoint.Y > Grid.Length - 1)
                            continue;

                        // already visited
                        if (visitedPoint.Any(p => p.X == newPoint.X && p.Y == newPoint.Y))
                            continue;

                        // too high
                        var newElevation = GetElevation(newPoint);
                        if (newElevation - GetElevation(path.Last()) > 1)
                            continue;

                        if (newPoint == Target)
                        {
                            ShowMap(path, myPosition);
                            result = path.Count;
                            break;
                        }

                        var newPath = new List<Point>(path);
                        newPath.Reverse();
                        newPath.Add(newPoint);
                        newPathToExplore.Add(newPath);
                        visitedPoint.Add(newPoint);
                    }
                }
                paths = newPathToExplore;
            }

            Console.WriteLine($"Longueur du chemin : {result}");
        }

        private void ShowMap(List<Point> path, Point myPosition)
        {
            for (var y = 0; y < Grid.Length; y++)
            {
                var line = "";
                for (var x = 0; x < Grid[y].Length; x++)
                {
                    if (path.Any(p => p.X == x && p.Y == y))
                        line += (Grid[y][x]).ToString().ToUpper();
                    else
                        line += Grid[y][x];

                }
                Console.WriteLine(line);
            }
        }

        private char GetElevation(Point position)
        {
            var pos = Grid[position.Y][position.X];
            return pos switch
            {
                'S' => 'a',
                'E' => TargetElevation,
                _ => pos
            };
        }

        private enum Direction
        {
            North,
            West,
            South,
            East
        }

    }

}
