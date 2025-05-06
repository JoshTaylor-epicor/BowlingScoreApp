using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp.Scoring
{
    public class Calculator : ICalculator
    {
        public Calculator() { }

        public int CalculateScore(Frame Frame, Dictionary<int, Frame> Frames)
        {
            if (Frame.SpareOverflow)
            {
                Frame.FrameScore = Frame.GetFrameScore() + Frames[Frame.FrameNumber + 1].FirstBowl.BowlScore;
                return Frame.GetFrameScore() + Frames[Frame.FrameNumber + 1].FirstBowl.BowlScore;
            }
            else if (Frame.StrikeOverflow)
            {
                if (Frames[Frame.FrameNumber + 1].FirstBowl.isStrike == true && Frame.FrameNumber + 2 <= 11)
                {
                    Frame.FrameScore = 10 + Frames[Frame.FrameNumber + 1].FirstBowl.BowlScore + Frames[Frame.FrameNumber + 2].FirstBowl.BowlScore;
                    return Frame.FrameScore = 10 + Frames[Frame.FrameNumber + 1].FirstBowl.BowlScore + Frames[Frame.FrameNumber + 2].FirstBowl.BowlScore;
                } else
                {
                    Frame.FrameScore = 10 + Frames[Frame.FrameNumber + 1].FirstBowl.BowlScore + Frames[Frame.FrameNumber + 1].SecondBowl.BowlScore;
                    return 10 + Frames[Frame.FrameNumber + 1].FirstBowl.BowlScore + Frames[Frame.FrameNumber + 1].SecondBowl.BowlScore;
                }
                   
            }
            else
            {
                Frame.FrameScore = Frame.GetFrameScore();
                return Frame.GetFrameScore();
            }
        }
    }
}
