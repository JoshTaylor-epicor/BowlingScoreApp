
namespace BowlingScoreApp
{
    public class Frame
    {
        public int FrameScore = 0;  //Total bowl score in the frame
        public int FrameNumber; // The number of the frame, relative to state of the game.
        public Bowl FirstBowl;
        public Bowl SecondBowl;
        public bool SpareOverflow;
        public bool StrikeOverflow;

        //Constructor
        public Frame(int frameNum)
        {
            FrameNumber = frameNum;
            FirstBowl = new Bowl(0);
            SecondBowl = new Bowl(0);
            SpareOverflow = false;
            StrikeOverflow = false;
        }

        public bool CheckForSpare()
        {
            SpareOverflow = (FirstBowl.BowlScore + SecondBowl.BowlScore == 10 && FirstBowl.isStrike == false);
            return (FirstBowl.BowlScore + SecondBowl.BowlScore == 10 && FirstBowl.isStrike == false);
        }

        public bool CheckForStrike()
        {
            StrikeOverflow = (FirstBowl.BowlScore == 10);
            return (FirstBowl.BowlScore == 10);
        }

        public int GetFrameScore()
        {
            return FirstBowl.BowlScore + SecondBowl.BowlScore;
        }
    }
}
    