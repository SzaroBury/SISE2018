using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class dfsSolver
    {
        private Board board;
        private State solved;
        public TimeSpan time;
        public int checkedNodes = 0;
        private List<State> now;
        public HashSet<State> S = new HashSet<State>();
        private Queue<State> Q = new Queue<State>();
        public static int maxDepth = 20;
        public int depth = 0;

        public dfsSolver(Board b)
        {
            board = b;
        }

        void dfsid(State state, int Depth, string Order)
        {
            if (maxDepth -  Depth >= depth) depth = maxDepth - Depth;
            if (Depth == 0) return;
            checkedNodes++;
            if (state.isSolved())
            {
                solved = state;
                Console.WriteLine(Environment.NewLine + "!!!SOLVED!!!" + Environment.NewLine + state);
                Console.WriteLine((maxDepth - Depth) + " - " + ToSolution(state));
                return;
            }
            Console.WriteLine(Environment.NewLine + (maxDepth - Depth) + " - " + ToSolution(state));
            Console.WriteLine(state);
            State newState;
            for (int i = 0; i < Order.Length; i++)
            {
                switch (Order[i])
                {
                    case 'U':
                        newState = State.Up(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            dfsid(newState, Depth - 1, Order);
                            if (solved != null) return;
                            Console.WriteLine((maxDepth - Depth) + " - " + ToSolution(state));
                        }
                        break;
                    case 'D':
                        newState = State.Down(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            dfsid(newState, Depth - 1, Order);
                            if (solved != null) return;
                            Console.WriteLine((maxDepth - Depth) + " - " + ToSolution(state));
                        }
                        break;
                    case 'L':
                        newState = State.Left(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            dfsid(newState, Depth - 1, Order);
                            if (solved != null) return;
                            Console.WriteLine((maxDepth - Depth) + " - " + ToSolution(state));
                        }
                        break;
                    case 'R':
                        newState = State.Right(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            dfsid(newState, Depth - 1, Order);
                            if (solved != null) return;
                            Console.WriteLine((maxDepth - Depth) + " - " + ToSolution(state));
                        }
                        break;
                }
            }
        }

        public string Solve(string Order)
        {
            Stopwatch stopwatch = new Stopwatch();
            solved = null;
            State s = new State(board);

            stopwatch.Start();
            iteracyjnePoglebienie(s, Order);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution(solved);
        }

        public string Solve2(string Order)
        {
            Stopwatch stopwatch = new Stopwatch();
            solved = null;
            State s = new State(board);

            stopwatch.Start();
            S.Add(s);
            dfsid(s, maxDepth - 1, Order);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution(solved);
        }

        void iteracyjnePoglebienie(State s, string order)
        {
            S.Clear();
            S.Add(s);
            for (int i = 1; i <= maxDepth; i++)
            {
                dfsid(s, i, order);
                if (solved != null)
                    return;
            }
        }

        public string ToSolution(State input)
        {
            State current = input, parent;
            StringBuilder result = new StringBuilder();
            now = new List<State>();
            while (true)
            {
                now.Add(current);
                parent = current.Previous;
                if (parent == null)
                    break;
                result.Append(current.Translation);
                current = parent;
            }
            //for (int i = now.Count - 1; i >= 0; i--)
            //Console.Write(i + ". " + now[i].Translation + Environment.NewLine + now[i] + Environment.NewLine);
            Console.WriteLine("\nPassed nodes: " + S.Count);
            Console.WriteLine("Checked nodes: " + checkedNodes);
            Console.WriteLine("Reached depth: " + depth);
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
