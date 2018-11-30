using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    class AstarSolver
    {
        private Board board;
        private AstarState goal;
        public TimeSpan time;
        private List<AstarState> now;
        public int passedNodes = 0;
        public static int maxDepth = 30;
        private Queue<AstarState> Q = new Queue<AstarState>();
        public HashSet<AstarState> S = new HashSet<AstarState>();
        public int Depth = 0;

        public AstarSolver(Board b)
        {
            board = b;
        }

        public void Astar(AstarState state)
        {
            Q.Clear();
            S.Clear();
            Q.Enqueue(state);
            AstarState s, newState;
            while (Q.Count != 0)
            {
                s = Q.Dequeue();
                if (s.isSolved())
                {
                    goal = s;
                    break;
                }
                if (S.Contains(s))
                    continue;
                S.Add(s);

                newState = AstarState.Up(s);
                if (newState != null && !S.Contains(newState))
                {
                    Q.Enqueue(newState);
                    passedNodes++;
                }
                newState = AstarState.Down(s);
                if (newState != null && !S.Contains(newState))
                {
                    Q.Enqueue(newState);
                    passedNodes++;
                }
                newState = AstarState.Left(s);
                if (newState != null && !S.Contains(newState))
                {
                    Q.Enqueue(newState);
                    passedNodes++;
                }
                newState = AstarState.Right(s);
                if (newState != null && !S.Contains(newState))
                {
                    Q.Enqueue(newState);
                    passedNodes++;
                }
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
            return ToSolution(heuristic);
        }

        public string ToSolution(string heuristic)
        {
            AstarState current = goal, parent;
            StringBuilder result = new StringBuilder();
            now = new List<AstarState>();
            while (true)
            {
                now.Add(current);
                //.CopyState(current.Previous, heuristic);
                if (current.Previous1 == null) { parent = null; break; }
                parent = new AstarState(current.Previous1, heuristic);
                result.Append(current.Translation);
                current = parent;
            }
            for (int i = now.Count - 1; i >= 0; i--)
                Console.Write(now[i]);
            Console.WriteLine("\nPassed nodes: " + passedNodes);
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
