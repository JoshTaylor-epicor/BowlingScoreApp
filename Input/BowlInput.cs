using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace BowlingScoreApp.Input
{
    public class BowlInput : IInput
    {
        public BowlInput() { }
        public int GetInput() 
        {
            Console.WriteLine("Please enter how many pins were knocked down for this bowl: ");
            
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
