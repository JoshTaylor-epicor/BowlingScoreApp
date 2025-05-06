using System.ComponentModel.Design;
using BowlingScoreApp.Input;
using BowlingScoreApp.Output;
using BowlingScoreApp.Scoring;
using BowlingScoreApp.Worker;
using static BowlingScoreApp.Frame;

namespace BowlingScoreApp
{
    public class BowlingGame : IBowlingGame
    {
        private readonly ICalculator _calculator;
        private readonly IInput _gameInput;
        private readonly IOutput _gameOutput;
        private readonly IWorker _gameWorker;
        private int _prevFrameIndex;
        private int _currFrameIndex;
        private Dictionary<int, Frame> _frames;
        private bool _threeActive;


        public BowlingGame(ICalculator calculator, IInput input, IOutput output, IWorker GameWorker)
        {
            _calculator = calculator;
            _gameInput = input;
            _gameOutput = output;
            _gameWorker = GameWorker;
            _prevFrameIndex = 1;
            _currFrameIndex = 1;
            _threeActive = false;
            _frames = new Dictionary<int, Frame>();
        }
        public void StartGame() 
        {
            _gameOutput.OutputGreeting();

            //Create our 10 frames for the game
            GenerateFrames();
            while (_frames[_frames.Count - 1].CurrentState != Frame.FrameState.FrameScored) { Roll(); };
            _gameOutput.OutputOverall(_frames);
            
        }
       public int GetScore()
        {
            return _calculator.CalculateScore(_frames);
        }
        public void Roll() 
        {
            if (_prevFrameIndex != _currFrameIndex)
            {
                if (_gameWorker.CheckStates(_frames[_prevFrameIndex]) == true && _gameWorker.CheckStates(_frames[_currFrameIndex]) == true)
                {
                    Bowl _currentBowl = new Bowl(_gameInput.GetInput());
                    if (_threeActive == true)
                    {
                        _gameWorker.ProcessBowl(_frames[_prevFrameIndex - 1], _currentBowl);
                    }
                    if (_gameWorker.CheckStrike(_frames[_currFrameIndex], _currentBowl) == true) 
                    {
                        _gameWorker.ProcessBowl(_frames[_prevFrameIndex], _currentBowl);
                        _gameWorker.ProcessBowl(_frames[_currFrameIndex], _currentBowl);
                        _frames[_currFrameIndex].CurrentState = FrameState.FrameFillerBowling;
                        NextFrame();
                    } else
                    {
                        _gameWorker.ProcessBowl(_frames[_prevFrameIndex], _currentBowl);
                        _gameWorker.ProcessBowl(_frames[_currFrameIndex], _currentBowl);
                    }
                    _threeActive = _gameWorker.CheckThreeActive(_frames[_prevFrameIndex - 1], _currFrameIndex);
                } else if (_gameWorker.CheckStates(_frames[_prevFrameIndex]) == true)
                {
                    Bowl _currentBowl = new Bowl(_gameInput.GetInput());
                    _gameWorker.ProcessBowl(_frames[_prevFrameIndex], _currentBowl);
                } else if (_gameWorker.CheckStates(_frames[_currFrameIndex]) == true)
                {
                    if (_frames[_currFrameIndex].CurrentState == FrameState.FrameFillerBowling)
                    {
                        NextFrame();
                    }
                    else
                    {
                        Bowl _currentBowl = new Bowl(_gameInput.GetInput());
                        if (_gameWorker.CheckStrike(_frames[_currFrameIndex], _currentBowl) == true) 
                        { 
                            _gameWorker.ProcessBowl(_frames[_currFrameIndex], _currentBowl); 
                            _frames[_currFrameIndex].CurrentState = FrameState.FrameFillerBowling;
                            NextFrame(); 
                        } else
                        {
                            _gameWorker.ProcessBowl(_frames[_currFrameIndex], _currentBowl);
                        }
                        
                    }
                       
                } else
                {
                    NextFrame();
                }
            } else
            {
                if (_gameWorker.CheckStates(_frames[_currFrameIndex]) == true)
                {
                    Bowl _currentBowl = new Bowl(_gameInput.GetInput());
                    if (_gameWorker.CheckStrike(_frames[_currFrameIndex], _currentBowl) == true)
                    {
                        _gameWorker.ProcessBowl(_frames[_currFrameIndex], _currentBowl);
                        _frames[_currFrameIndex].CurrentState = FrameState.FrameFillerBowling;
                        NextFrame();
                    }
                    else
                    {
                        _gameWorker.ProcessBowl(_frames[_currFrameIndex], _currentBowl);
                        if (_frames[_currFrameIndex].CurrentState == FrameState.FrameFillerBowling)
                        {
                            NextFrame();
                        }
                    }
                } else
                {
                    NextFrame();
                }
            }
           
        }

        public void NextFrame()
        {
            if (_currFrameIndex < 11)
            {
                _prevFrameIndex = _currFrameIndex;
                _currFrameIndex++;
                _frames[_currFrameIndex].CurrentState = FrameState.FrameBowling;
                _gameOutput.OutputUpdate(_frames[_currFrameIndex]);
            }
        }
        public void GenerateFrames()
        {
            for (int i = 1; i < 12; i++)
            {
                _frames.Add(i, new Frame(i));
            }
            _gameOutput.OutputUpdate(_frames[_currFrameIndex]);
            _frames[_currFrameIndex].CurrentState = FrameState.FrameBowling;
        }
    }
}
