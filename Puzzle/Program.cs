using Autofac;
using Puzzle.Interface;
using System;

namespace Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();

            var input = new int[] { 1, 2, 3, 4, 6, 5, 8, 9, 7, 0 };

            int[] result = Array.Empty<int>();

            using (var scope = container.BeginLifetimeScope())
            {
                var resolver = scope.Resolve<IResolver>();
                result = resolver.Solve(input);
            }

            Console.WriteLine($"[{string.Join(",", result)}]");


        }
    }
}
