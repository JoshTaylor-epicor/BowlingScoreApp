using BowlingScoreApp;

Console.WriteLine("Welcome To Bowl Scorer!");

BowlingScore BowlingGame = new();

while (BowlingGame.State != BowlingScore.GameState.GameFinished)
{
    BowlingGame.RecordScore(new Bowl());
}

