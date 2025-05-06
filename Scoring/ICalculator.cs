using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp.Scoring
{
    public interface ICalculator
    {
        int CalculateScore(Dictionary<int, Frame> Frames);
    }
}
