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
        public int FrameScore = 0;
        public Dictionary<int, Bowl> FrameBowls;
        public int FrameNumber;
        public int FrameFillers = 0;

        public Frame(int frameNum)
        {
            CurrentState = FrameState.FrameInitialised;
            FrameNumber = frameNum;
            FrameBowls = new Dictionary<int, Bowl>();
            Console.WriteLine("======================================");
            Console.WriteLine("               Frame " + FrameNumber);
            Console.WriteLine("======================================");
        }

        public int GetFrameScore()
        {
            // To Do
            CurrentState = FrameState.FrameScored;
            return FrameScore;
        }
        public void NewBowl(Bowl bowl)
        {
            // To Do
            switch(FrameBowls.Count + 1)
            {
                case 2:
                    if (CheckForSpare(bowl) == true)
                    {
                        FrameFillers += 1;
                        AddBowl(bowl);
                        CurrentState = FrameState.FrameFillerBowling;
                    } else
                    {
                        AddBowl(bowl);
                        CurrentState = FrameState.FrameAwaitingScoring;
                    }
                    break;
                case > 2 and <= 4:
                    if (FrameFillers > 0)
                    {
                        AddBowl(bowl);
                        FrameFillers--;
                    }
                    else
                    {
                        AddBowl(bowl);
                        CurrentState = FrameState.FrameAwaitingScoring;
                    }
                    break;
                default:
                    AddBowl(bowl);
                    break;
            }
        }

        public bool CheckForSpare(Bowl bowl)
        {
            if (FrameBowls.Count == 2)
            {
                // check for first two results spare
                if (FrameBowls[0].Score + FrameBowls[1].Score == 10)
                {
                    Console.WriteLine("SPARE!");
                    return true;
                }
            } else if (FrameBowls.Count + 1 == 2)
            {
                if (FrameBowls[0].Score + bowl.Score == 10)
                {
                    Console.WriteLine("SPARE!");
                    return true;
                }
            }
            return false;
        }

        public void NewFillerBowl(Bowl bowl)
        {
            if (FrameFillers > 0)
            {
                AddBowl(bowl);
                FrameFillers--;
            } else
            {
                CurrentState = FrameState.FrameAwaitingScoring;
            }
        }

        public void AddBowl(Bowl bowl)
        {
            FrameBowls.Add(FrameBowls.Count, bowl);
            FrameScore = FrameScore + bowl.Score;
        }
    }
}
