using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp
{
    public class Bowl
    {
        public int BowlScore; // Score of the bowl
        public bool isStrike; // Flag for whether bowl is strike or not

        //Constructor
        public Bowl(int Score)
        {
            BowlScore = Score;
            CheckForStrike();
        }
        /**
         * Defines the isStrike flag
         */
        public void CheckForStrike()
        {
            if (BowlScore == 10) { isStrike = true; }
        }
    }
}
