using System.ComponentModel.Design;
using BowlingScoreApp.Input;
using BowlingScoreApp.Output;
using BowlingScoreApp.Scoring;
using static BowlingScoreApp.Frame;

namespace BowlingScoreApp
{
    public class BowlingGame : IBowlingGame
    {
        private readonly ICalculator _calculator;
        private readonly IInput _gameInput;
        private readonly IOutput _gameOutput;
        private int _currFrameIndex;
        public Dictionary<int, Frame> _frames;


        public BowlingGame(ICalculator calculator, IInput input, IOutput output)
        {
            _calculator = calculator;
            _gameInput = input;
            _gameOutput = output;
            _currFrameIndex = 1;
            _frames = new Dictionary<int, Frame>();
        }
        public void StartGame() 
        {
            _gameOutput.OutputGreeting();

            //Create our 11 frames for the game
            GenerateFrames();
            //Get all the bowls for each frame
            for (int i=1; i<_frames.Count + 1;i++) 
            {
                if (i < _frames.Count)
                {
                    Roll();
                    continue;
                } 
                if (_frames[_frames.Count-1].CheckForSpare() == true || _frames[_frames.Count-1].CheckForStrike() == true)
                {
                    Roll();
                }
            }
            ;
            // Calculate Score for each frame
            for (int i=1; i<_frames.Count;i++)
            {
                _frames[i].CheckForSpare();
                _frames[i].CheckForStrike();
                _calculator.CalculateScore(_frames[i], _frames);
            }
            //Output information for all frames
            _gameOutput.OutputOverall(_frames);
        }
        public void Roll() 
        {
            _gameOutput.OutputUpdate(_frames[_currFrameIndex]);
            _frames[_currFrameIndex].FirstBowl = new Bowl(_gameInput.GetInput());
            if (_frames[_currFrameIndex].FirstBowl.isStrike == false)
            {
                _frames[_currFrameIndex].SecondBowl = new Bowl(_gameInput.GetInput());
                _frames[_currFrameIndex].StrikeOverflow = true;
            } else if (_frames[_currFrameIndex].SecondBowl.isStrike == true)
            {
                _frames[_currFrameIndex].StrikeOverflow = true;
            } else if (_frames[_currFrameIndex].FirstBowl.isStrike == true && _frames[_currFrameIndex].FrameNumber == 11)
            {
                _frames[_currFrameIndex].SecondBowl = new Bowl(_gameInput.GetInput());
            }
            _currFrameIndex++;
        }

        public void GenerateFrames()
        {
            for (int i = 1; i < 12; i++)
            {
                _frames.Add(i, new Frame(i));
            }
        }
    }

}
