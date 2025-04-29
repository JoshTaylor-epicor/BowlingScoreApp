using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static BowlingScoreApp.Frame;


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
            CurrentFrame = new Frame(1);
            PreviousFrame = CurrentFrame;
            Frames.Add(CurrentFrame.FrameNumber, CurrentFrame);
            State = GameState.GameStarted;
        }
        
        public void RecordScore()
        
        {
            switch (CurrentFrame.CurrentState)
            {
                case Frame.FrameState.FrameInitialised:
                    CurrentFrame.CurrentState = Frame.FrameState.FrameBowling;
                    CheckPrev();
                    break;
                case Frame.FrameState.FrameBowling:
                    Bowl local = new Bowl();
                    CheckPrev(local);
                    CheckStrike(local);
                    CurrentFrame.NewBowl(local);
                    break;
                case Frame.FrameState.FrameFillerBowling:
                    NextFrame();
                    Bowl currBowl = new Bowl();
                    CheckPrev(currBowl);
                    CheckStrike(currBowl);
                    CurrentFrame.NewBowl(currBowl);
                    break;
                case Frame.FrameState.FrameAwaitingScoring:
                    CheckPrev();
                    CurrentFrame.GetFrameScore();
                    NextFrame();
                    break;
            }
        }


        public void CheckStrike(Bowl currBowl)
        {
            if (currBowl.isStrike == true)
            {
                CurrentFrame.CurrentState = FrameState.FrameFillerBowling;
                CurrentFrame.FrameFillers += 2;
                Console.WriteLine("STRIKE");
            }
        }
        public void CheckPrev(Bowl currBowl)
        {
            if(PreviousFrame != CurrentFrame)
            {
                switch (PreviousFrame.CurrentState)
                {
                    case Frame.FrameState.FrameFillerBowling:
                        if (currBowl.isStrike == true)
                        {
                            PreviousFrame.NewFillerBowl(currBowl);
                            PreviousFrame.CurrentState = FrameState.FrameAwaitingScoring;
                            CheckPrev();
                        } else
                        {
                            PreviousFrame.NewFillerBowl(currBowl);
                            CheckPrev();
                        }

                        break;
                    case Frame.FrameState.FrameAwaitingScoring:
                        PreviousFrame.GetFrameScore();
                        break;
                }
            }
            
        }

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
        public void NextFrame()
        {
            // To Do
            if (Frames.Count >= 10 && Frames[Frames.Count].CurrentState == Frame.FrameState.FrameScored)
            {
                State = GameState.GameFinished;
            } else
            {
                PreviousFrame = CurrentFrame;
                CurrentFrame = new Frame(Frames.Count + 1);
                Frames.Add(Frames.Count + 1, CurrentFrame);
            }
        }
       
        public void OutputScores()
        {
            // To Do, Output Scores In The Command Line
            Console.WriteLine("Bowling Over! The Scores Are in:");
            foreach (KeyValuePair<int,Frame> kvp in Frames)
            {
                TotalScore += kvp.Value.FrameScore;
                Console.WriteLine("==============================");
                Console.WriteLine("          Frame " + kvp.Value.FrameNumber);
                Console.WriteLine("          Score: " + kvp.Value.FrameScore);
                Console.WriteLine("          Net Score: " + TotalScore);
                Console.WriteLine("==============================");
                
            }
            Console.WriteLine("Total Player Score: " + TotalScore);
        }
    }
}
