using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainbowLight
{
    class Program
    {
        static void Main(string[] args)
        {
            new KeyboardHook(new Blynclight());
        }
    }
}
