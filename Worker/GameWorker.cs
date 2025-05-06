using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static BowlingScoreApp.Frame;

namespace BowlingScoreApp.Worker
{
    public class GameWorker : IWorker
    {
        public GameWorker() { }

        public bool CheckStates(Frame Frame) 
        {
            switch (Frame.CurrentState)
            {
                case FrameState.FrameInitialised:
                    Frame.CurrentState = FrameState.FrameBowling;
                    return true;
                case FrameState.FrameBowling:
                    return true;
                case FrameState.FrameFillerBowling:
                    return true;
                case FrameState.FrameAwaitingScoring:
                    Frame.GetFrameScore();
                    return false;
                case FrameState.FrameScored:
                    return false;
            }
            return false;
        }

        public void ProcessBowl(Frame Frame, Bowl Bowl)
        {
            if (Frame.CurrentState == FrameState.FrameBowling)
            {
                Frame.NewBowl(Bowl);
            } else
            {
                
                Frame.NewFillerBowl(Bowl);
            }
        }

        public bool CheckThreeActive(Frame Frame, int index)
        {
            
            if (Frame.CurrentState == FrameState.FrameFillerBowling)
            {
                return true;
            }
            return false;
            
        }
        public bool CheckStrike(Frame Frame, Bowl currBowl)
        {
            if (currBowl.isStrike == true)
            {
                Frame.FrameFillers += 2;
                Console.WriteLine("STRIKE");
                return true;
            }
            return false;
        }

        public bool CheckForSpare(Frame Frame)
        {
            if (Frame.FrameBowls.Count == 2)
            {
                // check for first two results spare
                if (Frame.FrameBowls[0].BowlScore + Frame.FrameBowls[1].BowlScore == 10)
                {
                    Console.WriteLine("SPARE!");
                    return true;
                }
            }
            return false;
        }        
    }
}
