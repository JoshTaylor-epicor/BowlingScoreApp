using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp
{
    public class Frame
    {
        public enum FrameState
        {
            FrameInitialised,
            FrameBowling,
            FrameFillerBowling,
            FrameAwaitingScoring,
            FrameScored
        }
        public FrameState CurrentState;
        public int FrameScore;
        public Dictionary<int, Bowl> FrameBowls;
        public int FrameNumber;

        public Frame(int frameNum)
        {
            CurrentState = FrameState.FrameInitialised;
            FrameScore = 0;
            FrameNumber = frameNum;
            FrameBowls = new Dictionary<int, Bowl>();
            Console.WriteLine("Frame Number:", FrameNumber);
        }

        public int CalculateFrameScore()
        {
            // To Do
            CurrentState = FrameState.FrameScored;

            return FrameScore;
        }

        public int GetFrameBowls()
        {
            return FrameBowls.Count;
        }

        public void NewBowl(Bowl bowl)
        {
          
        }

        public void AddBowl(Bowl bowl)
        {
            FrameBowls.Add(FrameBowls.Count, bowl);
            FrameScore += bowl.Score;
        }
    }
}
