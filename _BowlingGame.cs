using static BowlingScoreApp.Frame;


namespace BowlingScoreApp
{
    /**
     * Class to control the whole bowling game 
     */
    public class _BowlingGame
    {
        // Different states of the game
        public enum GameState
        {
            GameStarted,
            GameFinished
        } 
        public Frame CurrentFrame; // The currently active frame
        public Frame PreviousFrame; // The previously played frame
        public Dictionary<int, Frame> Frames = new Dictionary<int, Frame>(); // Storage of all frames
        public GameState State; // Current state of the game

        //Constructor
        public _BowlingGame()
        {
            CurrentFrame = new Frame(1);
            PreviousFrame = CurrentFrame;
            Frames.Add(CurrentFrame.FrameNumber, CurrentFrame);
            State = GameState.GameStarted;
        }
        /**
         *  Function to record new score of a bowl and map it to correct frame 
         */

       
        /**
         * Checks for previous unresolved frames+
         */
        public void CheckPrev(Bowl currBowl)
        {
            
            
        }

        /**
        * Overloaded Method for the purpose of checking for Frame state change of PreviousState
        */
        public void CheckPrev()
        {
            if (PreviousFrame != CurrentFrame)
            {
                if (PreviousFrame.CurrentState == Frame.FrameState.FrameAwaitingScoring || PreviousFrame.FrameFillers <= 0)
                {
                    PreviousFrame.CurrentState = Frame.FrameState.FrameAwaitingScoring;
                    PreviousFrame.GetFrameScore();
                }

            }
        }
        /*
         * Responsible for Creating a new game Frame
         */
        public void NextFrame()
        {
            // To Do
            if (Frames.Count >= 10 && Frames[Frames.Count].CurrentState == Frame.FrameState.FrameScored) // Check for game end
            {
                State = GameState.GameFinished;
            } else if (Frames.Count + 1 > 10) // Check for Filler Frame
            {
                PreviousFrame = CurrentFrame;
                CurrentFrame = new Frame(Frames.Count + 1);
            }
            else //Create New Frame ELSE
            {
                PreviousFrame = CurrentFrame;
                CurrentFrame = new Frame(Frames.Count + 1);
                Frames.Add(Frames.Count + 1, CurrentFrame);
            }
        }
 
    }
}
