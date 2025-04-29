using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp
{
    public class Bowl
    {
        public int Score;
        public bool isStrike;

        public Bowl()
        {
            Console.WriteLine("Please Enter The Score For This Bowl:");
            Score = Convert.ToInt32(Console.ReadLine());
            CheckForStrike();
        }

        public void CheckForStrike()
        {
            if (Score == 10) { isStrike = true; }
        }
    }
}
