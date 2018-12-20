using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class AstarState : State
    {
        private int g;
        private int h;
        private static string heuristic;
        private AstarState previous;

        private int H { get => h; set => h = value; }
        public AstarState Previous1 { get => previous; set => previous = value; }

        public int f()
        {
            return H + g;
        }

        public AstarState() {  }

        public AstarState(State state, string h) :base(state)
        {
            heuristic = h;
        }

        public AstarState(Board board, string h) 
        {
            Board = board;
            EmptyID = board.EmptyId;

            ByteState = new byte[board.Height * board.Width];
            for (int i = 0; i < board.Height * board.Height; i++)
                ByteState[i] = board.Input[i];
            heuristic = h;
        }

        #region Heuristics

        public int HammingHeuristic()
        {
            int resultult = 0;
            for (int i = 0; i < Board.Height * Board.Width; i++)
                if (ByteState[i] != Board.Goal[i])  resultult++;
            return resultult;
        }
        

        public int ManhattanHeuristic()
        {
            int resultult = 0;
            for (int i = 0; i < Board.Height * Board.Width; i++)
            {
                int d = ByteState[i] - 1;
                if (i == EmptyID) d = ByteState.Length - 1;
                int x1 = i / Board.Width,
                    y1 = i % Board.Height,
                    x2 = d / Board.Width,
                    y2 = d % Board.Height;
                resultult += Math.Abs(x2 - x1);
                resultult += Math.Abs(y2 - y1);
            }
            //Console.WriteLine("manh: " + resultult);
            return resultult;
        }

        public int Heuristic()
        {
            if (heuristic == "hamm")
                return HammingHeuristic();
            else if (heuristic == "manh")
                return ManhattanHeuristic();
            return 0;
        }
        #endregion

        #region UpDownLeftRight

        public static AstarState Up(AstarState oldState)
        {
            State temp = State.Up(oldState);
            AstarState newState = null;
            if (temp != null)
            {
                newState = new AstarState(temp, heuristic)
                {
                    Previous1 = oldState,
                    g = oldState.g + 1, 
                };
                newState.H = newState.Heuristic();
            }
            return newState;
        }
        public static AstarState Down(AstarState oldState)
        {
            State temp = State.Down(oldState);
            AstarState newState = null;
            if (temp != null)
            {
                newState = new AstarState(temp, heuristic)
                {
                    Previous1 = oldState,
                    g = oldState.g + 1,
                };
                newState.H = newState.Heuristic();
            }
            return newState;
        }
        public static AstarState Left(AstarState oldState)
        {
            State temp = State.Left(oldState);
            AstarState newState = null;
            if (temp != null)
            {
                newState = new AstarState(temp, heuristic)
                {
                    Previous1 = oldState,
                    g = oldState.g + 1,
                };
                newState.H = newState.Heuristic();
            }
            return newState;
        }
        public static AstarState Right(AstarState oldState)
        {
            State temp = State.Right(oldState);
            AstarState newState = null;
            if (temp != null)
            {
                newState = new AstarState(temp, heuristic)
                {
                    Previous1 = oldState,
                    g = oldState.g + 1,
                };
                newState.H = newState.Heuristic();
            }
            return newState;
        }
        #endregion

        public int CompareTo(AstarState stan)
        {
            int l = this.f(), r = stan.f();
            if (l > r)
                return 1;
            if (l < r)
                return -1;
            return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
