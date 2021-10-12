using System;
using System.Collections.Generic;
using System.Numerics;

namespace Jarvany
{
    class Program
    {
        static void Main(string[] args)
        {
            int betegNap = 5;
            int gyogyuloNap = 4;
            Jarvany jarvany = new Jarvany();
            jarvany.szimulacio("be.txt", betegNap, gyogyuloNap);
        }

    }
}
