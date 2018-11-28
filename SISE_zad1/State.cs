using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class State
    {
        private byte[] byteState;
        private int emptyID;
        private char translation;
        private State previous;
        private Board board;

        public State Previous { get => previous; protected set => previous = value; }
        public char Translation { get => translation; private set => translation = value; }
        public Board Board { get => board; set => board = value; }
        public int EmptyID { get => emptyID; set => emptyID = value; }
        public byte[] ByteState { get => byteState; set => byteState = value; }

        public State() { }

        public State(State state)
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

        public State(Board board)
        {
            this.Board = board;
            EmptyID = board.EmptyId;

            ByteState = new byte[board.Height * board.Width];
            for (int i = 0; i < board.Height * board.Height; i++)
                ByteState[i] = board.Input[i];
        }

        //public State(int[] a)
        //{
        //    int rozmiar = (int)(Math.Sqrt(a.Length) + 1e-6);
        //    int top = rozmiar * rozmiar;
        //    byteState = new byte[top];
        //    for (int i = 0; i < top; i++)
        //    {
        //        byteState[i] = (byte)a[i];
        //        if (byteState[i] == 0)
        //            emptyID = i;
        //    }
        //}

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

        public static State Up(State oldState)
        {
            if (oldState.EmptyID < oldState.Board.Width) return null;
            State newState = new State(oldState);

            newState.changePlaces(newState.EmptyID, newState.EmptyID - newState.Board.Width);
            newState.EmptyID -= oldState.Board.Width;
            newState.Translation = 'U';
            return newState;
        }

        public static State Down(State oldState)
        {
            if (oldState.EmptyID >= oldState.Board.Height * oldState.Board.Width - oldState.Board.Width) return null;
            State newState = new State(oldState);

            newState.changePlaces(newState.EmptyID, newState.EmptyID + newState.Board.Width);
            newState.EmptyID += oldState.Board.Width;
            newState.Translation = 'D';
            return newState;
        }

        public static State Left(State oldState)
        {
            if (oldState.EmptyID % oldState.Board.Width == 0) return null;
            
            State newState = new State(oldState);
            newState.changePlaces(newState.EmptyID, newState.EmptyID - 1);
            newState.EmptyID -= 1;
            newState.Translation = 'L';

            return newState;
        }

        public static State Right(State oldState)
        {
            if (oldState.EmptyID % oldState.Board.Width == oldState.Board.Width - 1) return null;
            State newState = new State(oldState);

            newState.changePlaces(newState.EmptyID, newState.EmptyID + 1);
            newState.EmptyID += 1;
            newState.Translation = 'R';
            return newState;
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
