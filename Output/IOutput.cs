using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp.Output
{
    public interface IOutput
    {
        void OutputUpdate(Frame frame);
        void OutputOverall(Dictionary<int, Frame> Frame);
        void OutputGreeting();
    }
}
