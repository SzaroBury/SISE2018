using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public static class Solution
    {
        public static void NewSolution(string solution, long time, string path)
        {
            Console.WriteLine("Solution: " + solution);
            Console.WriteLine("Steps: " + solution.Length);
            Console.WriteLine("Needed time: " + time.ToString() + "ms");
        }
    }
}
