using BowlingScoreApp;
using BowlingScoreApp.Scoring;
using BowlingScoreApp.Output;
using BowlingScoreApp.Input;
using BowlingScoreApp.Worker;
using Autofac;

var builder = new ContainerBuilder();

//register interfaces
builder.RegisterType<Calculator>().As<ICalculator>();
builder.RegisterType<GameOutput>().As<IOutput>();
builder.RegisterType<BowlInput>().As<IInput>();
builder.RegisterType<GameWorker>().As<IWorker>();
builder.RegisterType<BowlingGame>().As<IBowlingGame>();
var container = builder.Build();
using (var scope = container.BeginLifetimeScope())
{
    var game = scope.Resolve<IBowlingGame>();
    game.StartGame();
}