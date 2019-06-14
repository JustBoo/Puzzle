using Autofac;
using Puzzle.Interface;
using System;
using System.Collections.Generic;

namespace Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputList = new List<int[]>()
            {
                new int[] { 1, 2, 3, 4, 6, 5, 0, 7, 8, 9 },
                new int[] { 1, 2, 3, 4, 6, 5, 8, 9, 7, 0 }
            };

            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var resolver = scope.Resolve<IResolver>();

                foreach(var input in inputList)
                {
                    var result = resolver.Solve(input);
                    Console.WriteLine($"Input array: [{string.Join(",", input)}]  /  Result: [{string.Join(",", result)}]");
                }
                
            }
        }
    }
}
