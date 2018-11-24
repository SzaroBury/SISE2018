using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            int chosenStartegy = 1;
            string OrderOfSearching = "LRUD";
            //string Heuristic = "hamm";
            string startFilePath = "C:\\Users\\SzaroBury\\Desktop\\V sem\\SISE\\SISE_zad1\\WIKAMP_APPS\\4x4_01_00001.txt";
            string solutionFileName = "solution.txt";
            //string infoFileName = "info.txt";
            Board board = new Board(startFilePath);
            switch (chosenStartegy)
            {
                case 1:
                    {
                        Console.WriteLine("Breadth First Searching");
                        Console.Write("Order: " + OrderOfSearching + Environment.NewLine);

                        bfsSolver solver = new bfsSolver(board);
                        Solution.NewSolution(solver.Solve(OrderOfSearching, board), solver.Time, solutionFileName);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Depth First Searching");
                        Console.Write("Order: " + OrderOfSearching + Environment.NewLine);

                        dfsSolver solver = new dfsSolver(board);
                        Solution.NewSolution(solver.Solve2(OrderOfSearching, board), solver.time, solutionFileName);
                        break;
                    }
                    
            }
            Console.ReadKey();
        }
    }
}
