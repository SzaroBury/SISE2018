using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISE_zad1
{
    public class Board
    {
        private byte[] input;
        private byte[] goal;
        private int height;
        private int width;
        private int emptyId;

        public byte[] Goal { get => goal; private set => goal = value; }
        public byte[] Input { get => input; private set => input = value; }
        public int Height { get => height; private set => height = value; }
        public int Width { get => width; private set => width = value; }
        public int EmptyId { get => emptyId; set => emptyId = value; }


        public Board(int height, int width, byte[] board)
        {
            this.Input = board;
            this.Height = height;
            this.Width = width;
            EmptyId = FindEmptyId();
            SetGoal();
        }

        public Board(string path)
        {
            string input = System.IO.File.ReadAllText(@path);
            string[] temp = input.Split( new string[] { " ", Environment.NewLine }, StringSplitOptions.None );
            Height = Convert.ToInt16(temp[0]);
            Width = Convert.ToInt16(temp[1]);
            Input = new byte[Height * Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    Input[i * Width + j] = Convert.ToByte(temp[i * Width + j+2]);
            EmptyId = FindEmptyId();
            SetGoal();
        }

        public int FindEmptyId()
        {
            int id = 0;
            foreach (int i in Input)
            {
                if (i == 0) return id;
                else id++;
            }
            return -1;
        }

        public void SetGoal()
        {
            Goal = new byte[Height * Width];
            for (int i = 0; i < Height * Width - 1; i++)
                Goal[i] = (byte)(i + 1);
            Goal[Height * Width - 1] = 0;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    b.Append(Input[i * Width + j]);
                    b.Append(" ");
                }
                b.AppendLine();
            }
            return b.ToString();
        }
    }
}
