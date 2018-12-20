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
        public TimeSpan time;
        private List<State> states;
        public int openedStates = 0;
        public HashSet<string> Closed = new HashSet<string>();
        private Queue<State> Opened = new Queue<State>();
        public int Depth = 0;

        public bfsSolver(Board board)
        {
            this.board = board;
        }

        private void bfs(State state, string order)
        {
            Closed.Clear();
            Opened.Clear();
            Opened.Enqueue(state);
            State newState;
            while (Opened.Count != 0)
            {
                state = Opened.Dequeue();
                if (state.Depth >= Depth) Depth = state.Depth;
                Closed.Add(state.ToString());
                if (state.isSolved())
                {
                    solved = state;
                    //Console.WriteLine(Environment.NewLine + "!!!SOLVED!!!" + Environment.NewLine + state);
                    //Console.WriteLine(Depth + " - " + ToSolution(state));
                    break;
                }
                //Console.WriteLine(Environment.NewLine + state);
                //string pom2 = ToSolution(state);
                //Console.WriteLine(Depth + " - " + pom2);
                for (int i = 0; i < order.Length; i++)
                {
                    switch (order[i])
                    {
                        case 'U':
                            newState = State.Up(state);
                            if (newState != null && !Closed.Contains(newState.ToString()))
                            {
                                //Console.WriteLine(pom2 + " U");
                                openedStates++;
                                Opened.Enqueue(newState);
                            }
                            //else //Console.WriteLine(pom2 + " U - Null");
                            break;
                        case 'D':
                            newState = State.Down(state);
                            if (newState != null && !Closed.Contains(newState.ToString()))
                            {
                                //Console.WriteLine(pom2 + " D");
                                openedStates++;
                                Opened.Enqueue(newState);
                            }
                            //else //Console.WriteLine(pom2 + " D - Null");
                            break;
                        case 'L':
                            newState = State.Left(state);
                            if (newState != null && !Closed.Contains(newState.ToString()))
                            {
                                //Console.WriteLine(pom2 + " L");
                                openedStates++;
                                Opened.Enqueue(newState);
                            }
                            //else //Console.WriteLine(pom2 + " L - Null");
                            break;
                        case 'R':
                            newState = State.Right(state);
                            if (newState != null && !Closed.Contains(newState.ToString()))
                            {
                                //Console.WriteLine(pom2 + " R");
                                openedStates++;
                                Opened.Enqueue(newState);
                            }
                            //else //Console.WriteLine(pom2 + " R - Null");
                            break;
                    }
                }
                //Console.WriteLine("-----------------------------------------");
            }
        }

        public string Solve(string Order)
        {
            State s = new State(board);
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            bfs(s, Order);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution(solved);
        }

        public string ToSolution(State input)
        {
            State current = input, parent;
            StringBuilder result = new StringBuilder();
            states = new List<State>();
            while (true)
            {
                states.Add(current);
                parent = current.Previous;

                if (parent == null) break;
                result.Append(current.Translation);
                current = parent;
            }
            //Console.WriteLine("\nOpened nodes: " + openedStates);
            //Console.WriteLine("Closed Nodes: " + Closed.Count);
            //Console.WriteLine("Max depth: " + Depth);
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
