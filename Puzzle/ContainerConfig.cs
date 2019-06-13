using Autofac;
using Puzzle.Interface;
using Puzzle.PathFinders.Implementation;
using Puzzle.Services.Implementation;
using Puzzle.Services.Interfaces;

namespace Puzzle
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PuzzleResolverService>().As<IResolver>();
            builder.RegisterType<GameFieldCreationService>().As<IGameFieldCreationService>();
            builder.RegisterType<PuzzleValidationService>().As<IPuzzleValidationService>();
            builder.RegisterType<AStarPathFinder>().As<IPathFinder>();

            return builder.Build();
        }
    }
}
