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
        public void Astar (AstarState st)
        {
            Q.Clear();
            S.Clear();
            Q.Enqueue(st);
            AstarState s;
            AstarState nastepnyStan;
            while(Q.Count != 0)
            {
                s = Q.Dequeue();
                if (s.iscel())
                {
                    goal = s;
                    break;
                }
            }
            
        }
    }
}
