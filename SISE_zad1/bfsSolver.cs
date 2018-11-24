using System;
using System.Collections.Generic;
using System.Text;

namespace SISE_zad1
{
    public class bfsSolver
    {
        private Board board;
        private State goal;
        private long time;
        private List<State> now;
        private int passedNodes = 0;
        private HashSet<State> S = new HashSet<State>();
        private Queue<State> q = new Queue<State>();

        public long Time { get => time; private set => time = value; }

        public bfsSolver(Board board)
        {
            this.board = board;
        }

        private void bfs(State state, string order)
        {
            S.Clear();
            q.Clear();
            S.Add(state);
            q.Enqueue(state);
            State nextState;
            while (q.Count != 0)
            {
                passedNodes++;
                state = q.Dequeue();
                Console.WriteLine("----------------------------");
                Console.Write(state.ToString() + Environment.NewLine);
                if (state.isSolved())
                {
                    goal = state;
                    break;
                }

                for (int i = 0; i < order.Length; i++)
                {
                    switch (order[i])
                    {
                        case 'U':
                            Console.WriteLine("U");
                            nextState = State.Up(state);
                            if (nextState != null && !S.Contains(nextState))
                            {
                                S.Add(nextState);
                                q.Enqueue(nextState);
                                passedNodes++;
                                Console.Write(nextState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                        case 'D':
                            Console.WriteLine("D");
                            nextState = State.Down(state);
                            if (nextState != null && !S.Contains(nextState))
                            {
                                S.Add(nextState);
                                q.Enqueue(nextState);
                                passedNodes++;
                                Console.Write(nextState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                        case 'L':
                            Console.WriteLine("L");
                            nextState = State.Left(state);
                            if (nextState != null && !S.Contains(nextState))
                            {
                                S.Add(nextState);
                                q.Enqueue(nextState);
                                passedNodes++;
                                Console.Write(nextState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                        case 'R':
                            Console.WriteLine("R");
                            nextState = State.Right(state);
                            if (nextState != null && !S.Contains(nextState))
                            {
                                S.Add(nextState);
                                q.Enqueue(nextState);
                                passedNodes++;
                                Console.Write(nextState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                    }
                    //Console.ReadKey();
                }
            }
        }

        //public string Solve(int[] tab, string Order)
        //{
        //    long startTime = Environment.TickCount;
        //    goal = null;
        //    State s = new State(tab);
        //    bfs(s, Order);
        //    Time = System.Environment.TickCount - startTime;
        //    return CaleRozwiazanie();
        //}

        public string Solve(string Order, Board board)
        {
            long startTime = Environment.TickCount;
            State s = new State(board);
            bfs(s, Order);
            Time = Environment.TickCount - startTime;
            return ToSolution();
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
