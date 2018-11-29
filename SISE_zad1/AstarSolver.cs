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
        private int passedNodes = 0;
        public static int maxDepth = 30;
        private Queue<AstarState> Q = new Queue<AstarState>();
        private HashSet<AstarState> S = new HashSet<AstarState>();

        public AstarSolver(Board b)
        {
            board = b;
        }

        public void Astar (AstarState state)
        {
            Q.Clear();
            S.Clear();
            Q.Enqueue(state);
            AstarState s = new AstarState();
            AstarState nastepnyStan;
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
            }
            nastepnyStan = AstarState.Up(s);
            if(nastepnyStan !=null && S.Contains(nastepnyStan))
            {
                Q.Enqueue(nastepnyStan);
                passedNodes = passedNodes + 1;
            }
            nastepnyStan = AstarState.Down(s);
            if (nastepnyStan != null && S.Contains(nastepnyStan))
            {
                Q.Enqueue(nastepnyStan);
                passedNodes = passedNodes + 1;
            }
            nastepnyStan = AstarState.Left(s);
            if (nastepnyStan != null && S.Contains(nastepnyStan))
            {
                Q.Enqueue(nastepnyStan);
                passedNodes = passedNodes + 1;
            }
            nastepnyStan = AstarState.Right(s);
            if (nastepnyStan != null && S.Contains(nastepnyStan))
            {
                Q.Enqueue(nastepnyStan);
                passedNodes = passedNodes + 1;
            }

        }
        public string Solve (string heuristic)
        {
            Stopwatch stopwatch = new Stopwatch();
            AstarState s = new AstarState(board, heuristic);

            stopwatch.Start();
            Astar(s);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution();
        }
        public string ToSolution()
        {
            AstarState current = goal, parent;
            StringBuilder result = new StringBuilder();
            now = new List<AstarState>();
            while (true)
            {
                now.Add(current);
                parent = (AstarState) current.Previous;
                if (parent == null)
                    break;
                result.Append(current.Translation);
                current = parent;
            }
            for (int i = now.Count - 1; i >= 0; i--)
                Console.Write(now[i]);
            Console.WriteLine("\nPassed nodes: " + passedNodes);
            return Reverse(result.ToString());
        }
        public string Reverse (string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
