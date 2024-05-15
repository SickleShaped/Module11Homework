using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityBot.Services.Interfaces;

namespace UtilityBot.Services.Implementations
{
    public class Sum:ISum
    {
        public int GetSum(string message)
        {
            int sum = 0;

            string[] textNumbers = message.Split(' ');
            foreach(var textNumber in textNumbers)
            {
                sum += Int32.Parse(textNumber);
            }

            return sum;
        }
    }
}
