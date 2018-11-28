using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class AstarState : State
    {
        int g;
        private int h;
        private int Heurystyka;

        public AstarState()
        {

        }
        private int H { get => h; set => h = value; }
        public int f()
        {
            return H + g;
        }

        public AstarState(AstarState state)
        {
            Previous = state;
            Board = state.Board;
            EmptyID = state.EmptyID;
            ByteState = new byte[state.Board.Height * state.Board.Width];
            for (int i = 0; i < state.Board.Height * state.Board.Width; i++)
            {
                ByteState[i] = state.ByteState[i];
            }
        }

        public AstarState(Board board)
        {
            this.Board = board;
            EmptyID = board.EmptyId;

            ByteState = new byte[board.Height * board.Width];
            for (int i = 0; i < board.Height * board.Height; i++)
                ByteState[i] = board.Input[i];
        }

        public int Heurystyka1()
        {
            int result = 0;
            for (int i = 0; i < Board.Height * Board.Width; i++)
                if (ByteState[i] != Board.Goal[i])  result++;
            return result;
        }

        public int Heurystyka2()
        {
            int result = 0;
            for (int i = 0; i < Board.Height * Board.Width; i++)
            {
                int d = ByteState[i] - 1;
                int x1 = i / Board.Width,
                    y1 = i % Board.Height,
                    x2 = d / Board.Width,
                    y2 = d % Board.Height;
                result += Math.Abs(x2 - x1);
                result += Math.Abs(y2 - y1);
            }
            return result;
        }

        public int FindH()
        {
            if (Heurystyka == 1)
                return Heurystyka1();
            else
                return Heurystyka2();
        }

        public static AstarState Up(AstarState oldState)
        {
            AstarState res = (AstarState)State.Up(oldState);
            if ( res !=null)
            {
                res.g = oldState.g + 1;
                res.H = res.FindH();
            }
            return res;
        }
        public static AstarState Down(AstarState oldState)
        {
            AstarState res = (AstarState)State.Down(oldState);
            if (res != null)
            {
                res.g = oldState.g + 1;
                res.H = res.FindH();
            }
            return res;
        }
        public static AstarState Left(AstarState oldState)
        {
            AstarState res = (AstarState)State.Left(oldState);
            if (res != null)
            {
                res.g = oldState.g + 1;
                res.H = res.FindH();
            }
            return res;
        }
        public static AstarState Right(AstarState oldState)
        {
            AstarState res = (AstarState)State.Right(oldState);
            if (res != null)
            {
                res.g = oldState.g + 1;
                res.H = res.FindH();
            }
            return res;
        }

        public int CompareTo(AstarState stan)
        {
            int l = this.f(), r = stan.f();
            if (l > r)
                return 1;
            if (l < r)
                return -1;
            return 0;
        }

    }
}
