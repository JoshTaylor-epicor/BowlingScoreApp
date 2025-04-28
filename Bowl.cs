using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp
{
    public class Bowl
    {
        public enum Type
        {
            Regular,
            FillerStrike,
            FillerSpare
        }
        public int Score;
        public Type bowlType;

        public Bowl()
        {
            bowlType = Type.Regular;
            Console.WriteLine("Please Enter The Score For This Bowl:");
            Score = Convert.ToInt32(Console.ReadLine());
        }
    }
}
