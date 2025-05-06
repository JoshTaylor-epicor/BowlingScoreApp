using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingScoreApp.Scoring;

namespace BowlingScoreApp.Output
{
    public class GameOutput : IOutput
    {

        public void OutputUpdate(Frame Frame) 
        {
            Console.WriteLine("======================================");
            Console.WriteLine("               Frame " + (Frame.FrameNumber == 11 ? "Filler" : Frame.FrameNumber));
            Console.WriteLine("======================================");
        }
     
        public void OutputOverall(Dictionary<int, Frame> Frames)
        {
            var TotalScore = 0;
            foreach (KeyValuePair<int, Frame> kvp in Frames) //loop through all frames and output score, net score, frame number
            {
                if (kvp.Value.FrameNumber <= 10)
                {
                    TotalScore += kvp.Value.FrameScore; // Add to net score each loop
                    Console.WriteLine("==============================");
                    Console.WriteLine("          Frame " + kvp.Value.FrameNumber);
                    Console.WriteLine("          Score: " + kvp.Value.FrameScore);
                    Console.WriteLine("          Net Score: " + TotalScore);
                    Console.WriteLine("==============================");
                }
                
            }
            Console.WriteLine("GAME OVER!");
        }
        public void OutputGreeting()
        {
            Console.WriteLine("Welcome To Bowl Scorer!");
        }
    }
}
