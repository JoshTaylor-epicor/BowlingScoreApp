using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace BowlingScoreApp
{
    public class BowlingScore
    {
        public enum GameState
        {
            GameStarted,
            GameFinished
        }

        public Frame CurrentFrame;
        public Frame PreviousFrame;
        public int TotalScore;
        public Dictionary<int, Frame> Frames = new Dictionary<int, Frame>();
        public GameState State;

        public BowlingScore()
        {
            CurrentFrame = new Frame(0);
            PreviousFrame = CurrentFrame;
            Frames.Add(CurrentFrame.FrameNumber, CurrentFrame);
            State = GameState.GameStarted;
        }
        
        public void RecordScore(Bowl currBowl)
        {
            //To Do

        }

        public void NextFrame()
        {
            // To Do
            if (Frames.Count >= 10 && Frames[Frames.Count].CurrentState == Frame.FrameState.FrameScored)
            {
                State = GameState.GameFinished;
            } else
            {
                CurrentFrame = new Frame(Frames.Count);
                Frames.Add(Frames.Count, CurrentFrame);
            }
        }
        public int GetFrameCount()
        {
            return Frames.Count;
        }

        public int GetScore()
        {
            return TotalScore + CurrentFrame.CalculateFrameScore();
        }
    }
}
