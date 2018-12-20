using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using C5;

namespace SISE_zad1
{
    class AstarSolver
    {
        private Board board;
        private AstarState goal;
        public TimeSpan time;
        private List<AstarState> states;
        public int openedStates = 1;
        IntervalHeap<AstarState> Open = new IntervalHeap<AstarState>();
        public C5.HashSet<string> Closed = new C5.HashSet<string>();
        public int Depth = 0;

        public AstarSolver(Board b)
        {
            board = b;
        }

        public void Astar(AstarState state)
        {
            Closed.Clear();
            Open.Add(state);
            AstarState oldState, newState;
            while (Open.Count != 0)
            {
                oldState = Open.DeleteMin();
                if (oldState.Depth > Depth) Depth = oldState.Depth;
                if (oldState.isSolved())
                {
                    goal = oldState;
                    //Console.WriteLine("!!!SOLVED!!!");
                    //Console.WriteLine(ToSolution(goal));
                    //Console.WriteLine(goal);
                    break;
                }

                if (Closed.Contains(oldState.ToString())) continue;

                Closed.Add(oldState.ToString());
                //Console.WriteLine(oldState);
                //Console.WriteLine(ToSolution(oldState));

                newState = AstarState.Up(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        //Console.Write("U");
                        Open.Add(newState);
                        openedStates++;
                    }
                }

                newState = AstarState.Down(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        //Console.Write("D");
                        Open.Add(newState);
                        openedStates++;
                    }
                }

                newState = AstarState.Left(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        //Console.Write("L");
                        Open.Add(newState);
                        openedStates++;
                    }
                }

                newState = AstarState.Right(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        //Console.Write("R");
                        Open.Add(newState);
                        openedStates++;
                    }
                }
                //Console.WriteLine();
            }
        }

        public string Solve(string heuristic)
        {
            Stopwatch stopwatch = new Stopwatch();
            AstarState s = new AstarState(board, heuristic);

            stopwatch.Start();
            Astar(s);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution(goal);
        }

        public string ToSolution(AstarState input)
        {
            AstarState current = input, parent;
            StringBuilder result = new StringBuilder();
            states = new List<AstarState>();
            while (true)
            {
                states.Add(current);
                parent = current.Previous1;

                if (parent == null) break;
                result.Append(current.Translation);
                current = parent;
            }
            
            //Console.WriteLine("\nOpened nodes: " + openedStates);
            //Console.WriteLine("Closed nodes: " + Closed.Count);
            //Console.WriteLine("Reached depth: " + Depth);
            return Reverse(result.ToString());
        }

        public string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
