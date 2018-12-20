﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SISE_zad1
{
    public class dfsSolver
    {
        private Board board;
        private State solved;
        public TimeSpan time;
        public int openedStates = 0;
        private List<State> states;
        public HashSet<string> Closed = new HashSet<string>();
        private Queue<State> Opened = new Queue<State>();
        public int Depth = 0;
        public static int MaxDepth = 20;

        public dfsSolver(Board b)
        {
            board = b;
        }

        void dfsid(State state, string Order)
        {
            if (state.Depth > MaxDepth) return;
            if (state.Depth >= Depth) Depth = state.Depth;
            if (state.isSolved())
            {
                solved = state;
                //Console.WriteLine(Environment.NewLine + "!!!SOLVED!!!" + Environment.NewLine + state);
                //Console.WriteLine(state.Depth + " - " + ToSolution(state));
                return;
            }
            Closed.Add(state.ToString());
            //Console.WriteLine(Environment.NewLine + (state.Depth) + " - " + ToSolution(state));
            //Console.WriteLine(state);
            State newState;
            for (int i = 0; i < Order.Length; i++)
            {
                switch (Order[i])
                {
                    case 'U':
                        newState = State.Up(state);
                        if (newState != null && !Closed.Contains(newState.ToString()))
                        {
                            openedStates++;
                            dfsid(newState, Order);
                            if (solved != null) return;
                            //Console.WriteLine(newState.Depth + " - " + ToSolution(state));
                        }
                        break;
                    case 'D':
                        newState = State.Down(state);
                        if (newState != null && !Closed.Contains(newState.ToString()))
                        {
                            openedStates++;
                            dfsid(newState, Order);
                            if (solved != null) return;
                            //Console.WriteLine(newState.Depth + " - " + ToSolution(state));
                        }
                        break;
                    case 'L':
                        newState = State.Left(state);
                        if (newState != null && !Closed.Contains(newState.ToString()))
                        {
                            openedStates++;
                            dfsid(newState, Order);
                            if (solved != null) return;
                            //Console.WriteLine(newState.Depth + " - " + ToSolution(state));
                        }
                        break;
                    case 'R':
                        newState = State.Right(state);
                        if (newState != null && !Closed.Contains(newState.ToString()))
                        {
                            openedStates++;
                            dfsid(newState, Order);
                            if (solved != null) return;
                            //Console.WriteLine(newState.Depth + " - " + ToSolution(state));
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
            Closed.Clear();
            Closed.Add(s.ToString());
            dfsid(s, Order);
            stopwatch.Stop();

            time = stopwatch.Elapsed;
            return ToSolution(solved);
        }

        void iteracyjnePoglebienie(State s, string order)
        {
            Closed.Clear();
            Closed.Add(s.ToString());
            for (int i = 1; i <= MaxDepth; i++)
            {
                s.Depth = i;
                dfsid(s, order);
                if (solved != null)
                    return;
            }
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

            //Console.WriteLine("\nOpened states: " + openedStates);
            //Console.WriteLine("Closed states: " + Closed.Count);
            //Console.WriteLine("Reached depth: " + Depth);
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
