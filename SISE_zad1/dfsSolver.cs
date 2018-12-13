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
        private State goal;
        public TimeSpan time;
        public int checkedNodes = 0;
        private List<State> now;
        public HashSet<State> S = new HashSet<State>();
        private Queue<State> Q = new Queue<State>();
        public static int maxDepth = 30;
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
                { goal = state; return; }

            State newState;
            for (int i = 0; i < Order.Length; i++)
            {
                switch (Order[i])
                {
                    case 'U':
                        Console.WriteLine("U");
                        newState = State.Up(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            Console.Write(newState.ToString());

                            dfsid(newState, Depth - 1, Order);
                            if (goal != null)
                                return;
                        }
                        break;
                    case 'D':
                        Console.WriteLine("D");
                        newState = State.Down(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            Console.Write(newState.ToString());

                            dfsid(newState, Depth - 1, Order);
                            if (goal != null)
                                return;
                        }
                        break;
                    case 'L':
                        Console.WriteLine("L");
                        newState = State.Left(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            Console.Write(newState.ToString());

                            dfsid(newState, Depth - 1, Order);
                            if (goal != null)
                                return;
                        }
                        break;
                    case 'R':
                        Console.WriteLine("R");
                        newState = State.Right(state);
                        if (newState != null && !S.Contains(newState))
                        {
                            S.Add(newState);
                            Console.Write(newState.ToString());

                            dfsid(newState, Depth - 1, Order);
                            if (goal != null)
                                return;
                        }
                        break;
                }
            }

        }

        public string Solve(string Order)
        {
            Stopwatch stopwatch = new Stopwatch();
            goal = null;
            State s = new State(board);

            stopwatch.Start();
            iteracyjnePoglebienie(s, Order);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution();
        }

        public string Solve2(string Order)
        {
            Stopwatch stopwatch = new Stopwatch();
            goal = null;
            State s = new State(board);

            stopwatch.Start();
            dfsid(s, maxDepth, Order);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution();
        }

        void iteracyjnePoglebienie(State s, string order)
        {
            S.Clear();
            S.Add(s);
            for (int i = 1; i <= maxDepth; i++)
            {
                dfsid(s, i, order);
                if (goal != null)
                    return;
            }
        }

        public string ToSolution()
        {
            State current = goal, parent;
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
            for (int i = now.Count - 1; i >= 0; i--)
                Console.Write(now[i]);
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
