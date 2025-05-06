using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp.Scoring
{
    public class Calculator : ICalculator
    {
        public int TotalScore = 0;
        public Calculator() { }
        public int CalculateScore(Dictionary<int, Frame> Frames)
        {
            TotalScore = 0;
            foreach (KeyValuePair<int, Frame> pair in Frames)
            {
                TotalScore += pair.Value.FrameScore;
            }
            return TotalScore;
        }
        public int CalculateScore(Frame Frame)
        {
            TotalScore = 0;
            foreach (KeyValuePair<int, Bowl> pair in Frame.FrameBowls)
            {
                TotalScore += pair.Value.BowlScore;
            }
            return TotalScore;
        }

    }
}
