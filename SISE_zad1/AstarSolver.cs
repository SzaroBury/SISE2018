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
        public int checkedNodes = 1;
        IntervalHeap<AstarState> Open = new IntervalHeap<AstarState>();
        public C5.HashSet<string> Closed = new C5.HashSet<string>();
        public int Depth = 0;
        public int test = 0;

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
                Depth++;
                oldState = Open.DeleteMin();
                if (oldState.isSolved())
                {
                    goal = oldState;
                    Console.WriteLine("!!!SOLVED!!!");
                    Console.WriteLine(ToSolution(goal));
                    Console.WriteLine(goal);
                    break;
                }

                if (Closed.Contains(oldState.ToString())) { test++; continue; }
                Closed.Add(oldState.ToString());

                Console.WriteLine(oldState);
                Console.WriteLine(ToSolution(oldState));

                newState = AstarState.Up(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        Console.Write("U");
                        Open.Add(newState);
                        checkedNodes++;
                    }
                    else test++;
                }

                newState = AstarState.Down(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        Console.Write("D");
                        Open.Add(newState);
                        checkedNodes++;
                    }
                    else test++;
                }

                newState = AstarState.Left(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        Console.Write("L");
                        Open.Add(newState);
                        checkedNodes++;
                    }
                    else test++;
                }

                newState = AstarState.Right(oldState);
                if (newState != null)
                {
                    if (!Closed.Contains(newState.ToString()))
                    {
                        Console.Write("R");
                        Open.Add(newState);
                        checkedNodes++;
                    }
                    else test++;
                }
                Console.WriteLine();
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
                if (parent == null)
                    break;
                result.Append(current.Translation);
                current = parent;
            }
            for (int i = states.Count - 1; i >= 0; i--)
            {
                //Console.Write(now[i]);
            }
            //Depth = states.Count;
            Console.WriteLine("\nVisited nodes: " + checkedNodes);
            Console.WriteLine("Explored nodes: " + Closed.Count);
            Console.WriteLine("Reached depth: " + Depth);
            Console.WriteLine("Repeats: " + test);
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
