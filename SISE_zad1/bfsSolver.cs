using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SISE_zad1
{
    public class bfsSolver
    {
        private Board board;
        private State goal;
        private TimeSpan time;
        private List<State> now;
        public int checkedNodes = 0;
        public HashSet<State> S = new HashSet<State>();
        private Queue<State> q = new Queue<State>();
        public int Depth = 0;
        private int layerCounter = 0;
        private int nextLayerCounter = 1;
        private int pom = 0;

        public TimeSpan Time { get => time; private set => time = value; }

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
            State newState;
            while (q.Count != 0)
            {
                if (layerCounter == 0)
                {
                    Depth++;
                    layerCounter = nextLayerCounter;
                    nextLayerCounter = 0;
                }
                pom = S.Count;
                checkedNodes++;
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
                            newState = State.Up(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                S.Add(newState);
                                q.Enqueue(newState);
                                Console.Write(newState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                        case 'D':
                            Console.WriteLine("D");
                            newState = State.Down(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                S.Add(newState);
                                q.Enqueue(newState);
                                Console.Write(newState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                        case 'L':
                            Console.WriteLine("L");
                            newState = State.Left(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                S.Add(newState);
                                q.Enqueue(newState);
                                Console.Write(newState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                        case 'R':
                            Console.WriteLine("R");
                            newState = State.Right(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                S.Add(newState);
                                q.Enqueue(newState);
                                Console.Write(newState.ToString());
                            }
                            else Console.WriteLine("Null");
                            break;
                    }
                }
                nextLayerCounter += S.Count-pom;
                layerCounter--;
            }
        }

        public string Solve(string Order)
        {
            State s = new State(board);
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            bfs(s, Order);
            stopwatch.Stop();

            Time = stopwatch.Elapsed;
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
            Console.WriteLine("\nPassed nodes: " + checkedNodes);
            Console.WriteLine("Checked nodes: " + S.Count);
            Console.WriteLine("Max depth: " + Depth);
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
