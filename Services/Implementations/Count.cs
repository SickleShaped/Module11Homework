using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityBot.Services.Interfaces;

namespace UtilityBot.Services.Implementations
{
    public class Count:ICount
    {
        public int GetCount(string message)
        {
            int count = message.Length;
            return count;
        }
    }
}
