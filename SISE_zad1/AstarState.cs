using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class AstarState
    {
        private byte[] byteState;
        private int emptyID;
        private char translation;
        private AstarState previous;
        private Board board;
        int g;
        private int h;
        private int Heurystyka;
        //private string order;

        public AstarState Previous { get => previous; private set => previous = value; }
        public char Translation { get => translation; private set => translation = value; }
        public Board Board { get => board; set => board = value; }
        public int EmptyID { get => emptyID; set => emptyID = value; }
        public byte[] ByteState { get => byteState; set => byteState = value; }
        public void setHigh (int x)
        {
            h = x;
        }
        public int f()
        {
            return h + g;
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
        private void changePlaces(int i, int j)
        {
            byte t = ByteState[i];
            ByteState[i] = ByteState[j];
            ByteState[j] = t;
        }

        public bool isSolved()
        {
            for (int i = 0; i < Board.Height * Board.Width; i++)
                if (ByteState[i] != Board.Goal[i]) return false;
            return true;
        }
        public int ZnajdzH()
        {
            if(Heurystyka ==1)
            {
                return Heurystyka1();
            }
            else
            {
                return Heurystyka2();
            }
        }



        public int compareTo(AstarState stan)
        {
            int l = this.f(), r = stan.f();
            if (l > r)
                return 1;
            if (l < r)
                return -1;
            return 0;
        }



        public static AstarState Up(AstarState oldState)
        {
            AstarState res = (AstarState)State.Up(oldState);
            if ( res !=null)
            {
                res.g = oldState.g + 1;
                res.h = res.ZnajdzH();
            }
            return res;
        }
        public static AstarState Down(AstarState oldState)
        {
            AstarState res = (AstarState)State.Down(oldState);
            if (res != null)
            {
                res.g = oldState.g + 1;
                res.h = res.ZnajdzH();
            }
            return res;
        }
        public static AstarState Left(AstarState oldState)
        {
            AstarState res = (AstarState)State.Left(oldState);
            if (res != null)
            {
                res.g = oldState.g + 1;
                res.h = res.ZnajdzH();
            }
            return res;
        }
        public static AstarState Right(AstarState oldState)
        {
            AstarState res = (AstarState)State.Right(oldState);
            if (res != null)
            {
                res.g = oldState.g + 1;
                res.h = res.ZnajdzH();
            }
            return res;
        }

        public int Heurystyka1()
        {
            int res = 0, top = Board.Height * Board.Width;
            for (int i=0; i < top; i++)
            {
                if (liczba[i] != celliczba[i])
                    res++;
            }
            return res;
        }
        public int Heurystyka2()
        {
            int res = 0, top = Board.Height * Board.Width;
            for (int i = 0; i < top; i++)
            {
                int d = this.getLiczba(i) - 1;
                int x1 = i / this.getRozmiar(), y1 = i % this.getRozmiar, x2 = d / this.getRozmiar(), y2 = d % this.getRozmiar();
                res += Math.Abs(x2 - x1);
                res += Math.Abs(y2 - y1);
            }
            return res;
        }
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Board.Height * Board.Width; i++)
            {
                if (ByteState[i] == 0)
                    res.Append("0 ");
                else
                    res.Append(ByteState[i].ToString() + " ");
                if (i % Board.Width == Board.Width - 1) res.Append(Environment.NewLine);
            }
            return res.ToString();
        }

    }
}
