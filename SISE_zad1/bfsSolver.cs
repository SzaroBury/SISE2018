using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SISE_zad1
{
    public class bfsSolver
    {
        private Board board;
        private State solved;
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
                if (state.isSolved())
                {
                    solved = state;
                    Console.WriteLine(Environment.NewLine + "!!!SOLVED!!!" + Environment.NewLine + state);
                    Console.WriteLine(Depth + " - " + ToSolution(state));
                    break;
                }
                Console.WriteLine(Environment.NewLine + state);
                string pom2 = ToSolution(state);
                Console.WriteLine(Depth + " - " + pom2);
                for (int i = 0; i < order.Length; i++)
                {
                    switch (order[i])
                    {
                        case 'U':
                            newState = State.Up(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                Console.WriteLine(pom2 + " U");
                                S.Add(newState);
                                q.Enqueue(newState);
                            }
                            else Console.WriteLine(pom2 + " U - Null");
                            break;
                        case 'D':
                            newState = State.Down(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                Console.WriteLine(pom2 + " D");
                                S.Add(newState);
                                q.Enqueue(newState);
                            }
                            else Console.WriteLine(pom2 + " D - Null");
                            break;
                        case 'L':
                            newState = State.Left(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                Console.WriteLine(pom2 + " L");
                                S.Add(newState);
                                q.Enqueue(newState);
                            }
                            else Console.WriteLine(pom2 + " L - Null");
                            break;
                        case 'R':
                            newState = State.Right(state);
                            if (newState != null && !S.Contains(newState))
                            {
                                Console.WriteLine(pom2 + " R");
                                S.Add(newState);
                                q.Enqueue(newState);
                            }
                            else Console.WriteLine(pom2 + " R - Null");
                            break;
                    }
                }
                nextLayerCounter += S.Count-pom;
                layerCounter--;
                Console.WriteLine("-----------------------------------------");
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
            return ToSolution(solved);
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
                if (parent == null) break;
                result.Append(current.Translation);
                current = parent;

            }
            Console.WriteLine("\nPassed nodes: " + S.Count);
            Console.WriteLine("Checked nodes: " + checkedNodes);
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
