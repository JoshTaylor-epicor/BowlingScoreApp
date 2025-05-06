using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreApp.Worker
{
    public interface IWorker
    {
        bool CheckStates(Frame Frame);
        void ProcessBowl(Frame Frame, Bowl Bowl);
        bool CheckStrike(Frame Frame, Bowl Bowl);

        bool CheckThreeActive(Frame Frame, int Index);
    }
}
