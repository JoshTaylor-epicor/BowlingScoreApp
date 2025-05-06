using BowlingScoreApp.Worker;

namespace BowlingScoreApp
{
    public class Frame
    {
        public enum FrameState
        {
            FrameInitialised, // Frame has just been created (initialised)
            FrameBowling,   // Frame is in regular bowling state (bowling it's first two bowls)
            FrameFillerBowling, // Frame is in filler bowling state (first two bowls were either SPARE or STRIKE)
            FrameAwaitingScoring, // All bowls have been resolved and now it is waiting to be scored
            FrameScored // Frame has been scored
        }
        public FrameState CurrentState; // The state of the frame, type FrameState.
        public int FrameScore = 0;  //Total bowl score in the frame
        public Dictionary<int, Bowl> FrameBowls; //Stores the current bowls in the frame
        public int FrameNumber; // The number of the frame, relative to state of the game.
        public int FrameFillers = 0; // Number of filler bowls to bowl in the frame, default 0.

        //Constructor
        public Frame(int frameNum)
        {
            CurrentState = FrameState.FrameInitialised;
            FrameNumber = frameNum;
            FrameBowls = new Dictionary<int, Bowl>();
        }

        /**
         * Sets the current frame as scored and returns current FrameScore
         */
        public int GetFrameScore()
        {
            CurrentState = FrameState.FrameScored;
            return FrameScore;
        }
        /**
         * Processing a new bowl in the frame
         */
        public void NewBowl(Bowl bowl)
        {
            switch(FrameBowls.Count + 1)
            {
                case 2: // if frame has two bowls, frame has bowled its regular bowls
                    if (CheckForSpare(bowl) == true)
                    {
                        FrameFillers += 1;
                        AddBowl(bowl);
                        CurrentState = FrameState.FrameFillerBowling;
                    } else
                    {
                        AddBowl(bowl);
                        CurrentState = FrameState.FrameAwaitingScoring;
                        GetFrameScore();
                    }
                    break;
                case > 2 and <= 4: // if frame is between 2 and 4 bowls, frame is into its filler bowls
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
                default: // regular bowl in the frame otherwise
                    AddBowl(bowl);
                    break;
            }
        }

        /**
         * Function responsible for checking last first two bowls of the frame for a spare
         */
        public bool CheckForSpare(Bowl bowl)
        {
            if (FrameBowls.Count == 2)
            {
                // check for first two results spare
                if (FrameBowls[0].BowlScore + FrameBowls[1].BowlScore == 10)
                {
                    Console.WriteLine("SPARE!");
                    return true;
                }
            }
            else if (FrameBowls.Count + 1 == 2)
            {
                if (FrameBowls[0].BowlScore + bowl.BowlScore == 10)
                {
                    Console.WriteLine("SPARE!");
                    return true;
                }
            }
            return false;
        }

        
        /**
         * Processes bowl for frame when its in Filler state
         */
        public void NewFillerBowl(Bowl bowl)
        {
            if (FrameFillers > 0)
            {
                AddBowl(bowl);
                FrameFillers--;
                if (FrameFillers == 0)
                {
                    CurrentState = FrameState.FrameAwaitingScoring;
                    GetFrameScore();
                }
            } 
        }
        /**
         * Function adds new bowl to bowl dictionary and adds to the FrameScore
         */
        public void AddBowl(Bowl bowl)
        {
            FrameBowls.Add(FrameBowls.Count, bowl);
            FrameScore = FrameScore + bowl.BowlScore;    
        }
    }
}
