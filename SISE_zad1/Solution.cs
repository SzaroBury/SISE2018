using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public static class Solution
    {
        public static void NewSolution(string solution, TimeSpan time, string solutionFileName, string infoFileName, string directory, bool folders)
        {
            if(folders)
            {
                Directory.CreateDirectory(directory + "\\Solutions");
                solutionFileName = directory + "\\Solutions\\" + solutionFileName;

                Directory.CreateDirectory(directory + "\\Info");
                infoFileName = directory + "\\Info\\" + infoFileName;
            }

            StreamWriter sw = File.CreateText(solutionFileName);
            if (solution.Length> 0) sw.WriteLine(solution.Length);
            else sw.WriteLine(-1);
            sw.WriteLine(solution);
            sw.Close();

            sw = File.CreateText(infoFileName);
            if (solution.Length > 0) sw.WriteLine(solution.Length);
            else sw.WriteLine(-1);
            sw.WriteLine("2");
            sw.WriteLine("3");
            sw.WriteLine("4");
            sw.WriteLine((double)(time.Ticks/(TimeSpan.TicksPerMillisecond/1000))/1000);
            sw.Close();
        }
    }
}
