using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class State
    {
        public State Previous { get; protected set; }
        public char Translation { get; protected set; }
        public Board Board { get; set; }
        public int EmptyID { get; set; }
        public byte[] ByteState { get; set; }
        public int Depth { get; set; }

        public State() { }

        public State(State state) //copy consturctor
        {
            if (state.Previous != null)
                Previous = state.Previous;
            Board = state.Board;
            EmptyID = state.EmptyID;
            Translation = state.Translation;
            Depth = state.Depth;
            ByteState = new byte[state.Board.Height * state.Board.Width];
            for (int i = 0; i < state.Board.Height * state.Board.Width; i++)
            {
                ByteState[i] = state.ByteState[i];
            }
        }

        public State(Board board) 
        {
            Board = board;
            Depth = 0;
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

        public static State Up(State oldState)
        {
            if (oldState.EmptyID < oldState.Board.Width) return null;

            State newState = new State(oldState);
            newState.Depth++;
            newState.Previous = oldState;
            newState.changePlaces(newState.EmptyID, newState.EmptyID - newState.Board.Width);
            newState.EmptyID -= oldState.Board.Width;
            newState.Translation = 'U';
            return newState;
        }

        public static State Down(State oldState)
        {
            if (oldState.EmptyID >= oldState.Board.Height * oldState.Board.Width - oldState.Board.Width) return null;

            State newState = new State(oldState);
            newState.Depth++;
            newState.Previous = oldState;
            newState.changePlaces(newState.EmptyID, newState.EmptyID + newState.Board.Width);
            newState.EmptyID += oldState.Board.Width;
            newState.Translation = 'D';
            return newState;
        }

        public static State Left(State oldState)
        {
            if (oldState.EmptyID % oldState.Board.Width == 0) return null;
            
            State newState = new State(oldState);
            newState.Depth++;
            newState.Previous = oldState;
            newState.changePlaces(newState.EmptyID, newState.EmptyID - 1);
            newState.EmptyID -= 1;
            newState.Translation = 'L';

            return newState;
        }

        public static State Right(State oldState)
        {
            if (oldState.EmptyID % oldState.Board.Width == oldState.Board.Width - 1) return null;

            State newState = new State(oldState);
            newState.Depth++;
            newState.Previous = oldState;
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
            }
            return res.ToString();
        }

        public override int GetHashCode()
        {
            int res = 0, 
                top = Board.Height * Board.Width - 1;
            for (int i = 0; i < top; i++)
            {
                res *= 31;
                res += ByteState[i];
            }
            return res;
        }

    }

}
