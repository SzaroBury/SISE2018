using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    class AstarSolver
    {
        private Board board;
        private AstarState goal;
        public long time;
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
            AstarState s;
            AstarState nastepnyStan;
            while(Q.Count != 0)
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
        public string Solve (string Order, int Heurystyka)
        {
            long startTime = Environment.TickCount;
            AstarState s = new AstarState(board);
            Astar(board, Heurystyka);
            time = Environment.TickCount - startTime;
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
                parent = current.Previous;
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
