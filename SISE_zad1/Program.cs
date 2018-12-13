using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            string chosenStartegy = "dfs";//bfs dfs astr
            string additionalParameter = "DRUL";// LRUD hamm manh
            string boardFileName = "Boards\\4x4_02_00002.txt";
            string solutionFileName = "solution.txt";
            string infoFileName = "info.txt";
            string directoryOfProgram = Directory.GetCurrentDirectory();
            bool sortInFolders = true;

            args = new string[5]; args[0] = chosenStartegy; args[1] = additionalParameter; args[2] = boardFileName; args[3] = solutionFileName; args[4] = infoFileName; //Odkomentować jeśli domyślne argumenty

            if (args.Length > 0)
            {
                if (args[0] == "bfs" || args[0] == "dfs" || args[0] == "astr") { chosenStartegy = args[0]; Console.WriteLine("Chosen strategy: " + chosenStartegy); }
                else { Console.WriteLine("First arg error"); Console.ReadKey(); return; }

                if (args[1] == "hamm" || args[1] == "manh" ||
                    (args[1].Contains("L") && args[1].Contains("R") && args[1].Contains("U") && args[1].Contains("D") && args[1].Length == 4))
                        { additionalParameter = args[1]; Console.WriteLine("Additional parameter: " + additionalParameter); }
                else { Console.WriteLine("Second arg error"); Console.ReadKey(); return; }

                if (File.Exists(directoryOfProgram + "\\" + args[2])) { boardFileName = directoryOfProgram + "\\" + args[2]; Console.WriteLine("Start file path: " + boardFileName); }
                else { Console.WriteLine("Third arg error: " + directoryOfProgram + "\\" + args[2]); Console.ReadKey(); return; }

                if (!File.Exists(directoryOfProgram + "\\" + args[3])) { solutionFileName = args[3]; Console.WriteLine("Solution saving path: " + solutionFileName); }
                else { Console.WriteLine("Fourth arg error: " + directoryOfProgram + "\\" + args[3]); Console.ReadKey(); return; }

                if (!File.Exists(directoryOfProgram + "\\" + args[4])) { infoFileName = args[4]; Console.WriteLine("Additional info path: " + infoFileName); }
                else { Console.WriteLine("Fivth arg error: " + directoryOfProgram + "\\" + args[4]); Console.ReadKey(); return; }
            }    
            else
            {
                Console.WriteLine("Syntax error");
                Console.ReadKey();
                return;
            }
            Board board = new Board(boardFileName);
            Console.Write(board.ToString());
            //Console.ReadKey();
            switch (chosenStartegy)
            {
                case "bfs":
                    {
                        Console.WriteLine("Breadth first searching");
                        Console.WriteLine("Order: " + additionalParameter);

                        bfsSolver solver = new bfsSolver(board);
                        Solution.NewSolution(solver.Solve(additionalParameter), solver.S.Count, solver.checkedNodes, solver.Depth, solver.Time, solutionFileName, infoFileName, directoryOfProgram, sortInFolders);
                        break;
                    }
                case "dfs":
                    {
                        Console.WriteLine("Depth first searching");
                        Console.WriteLine("Order: " + additionalParameter);

                        dfsSolver solver = new dfsSolver(board);
                        Solution.NewSolution(solver.Solve2(additionalParameter), solver.S.Count, solver.checkedNodes, solver.depth, solver.time, solutionFileName, infoFileName, directoryOfProgram, sortInFolders);
                        break;
                    }
                case "astr":
                    {
                        Console.WriteLine("A* search algorithm");
                        Console.WriteLine("Heuristic: " + additionalParameter);

                        AstarSolver solver = new AstarSolver(board);
                        Solution.NewSolution(solver.Solve(additionalParameter), solver.passedNodes, solver.S.Count, solver.Depth, solver.time, solutionFileName, infoFileName, directoryOfProgram, sortInFolders);
                        break;
                    }

            }
            Console.ReadKey();
        }
    }
}
