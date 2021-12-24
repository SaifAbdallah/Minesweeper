using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIB
{
    public enum Level { Beginner = 10, Intermediate = 20, Advanced = 40 };
    public class Game
    {/*
        private int bombs;
        //getter
        public int GetBombs()
        {
            return this.bombs;

        }
        public void SetBombs(int v)
        {
            this.bombs = v;
        }*/
        public int Bombs { get; set; } //lecture et ecriture
        public int[,] Grid { get; } //lecture seule
        public Game(Level level)
        {
            Bombs = (int)level;
            switch (level)
            {
                case Level.Beginner:
                    Grid = new int[9, 9];
                    //Bombs = (int)Level.Beginner;
                    break;
                case Level.Intermediate:
                    Grid = new int[12, 12];
                    //Bombs = (int)Level.Intermediate;
                    break;
                case Level.Advanced:
                    Grid = new int[12, 16];
                    //Bombs = (int)Level.Advanced;
                    break;
            }
            SetBombs();
            SetDigits();
        }
        public Game(Level level, int bombs) : this(level)
        {
            this.Bombs = bombs;
        }
        private void SetBombs()
        {
            int n = 0;
            Random r = new Random();
            while (n < Bombs)
            {

                int x = r.Next(0, Grid.GetLength(0));
                int y = r.Next(0, Grid.GetLength(1));
                if (Grid[x, y] != 9)
                {
                    Grid[x, y] = 9;
                    n = n + 1;
                }
            }
        }

        private void SetDigits()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] == 9)
                    {
                        SetCountour(i, j);
                    }
                }
            }
        }
        private void SetCountour(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i >= 0 && i < Grid.GetLength(0))
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (j >= 0 && j < Grid.GetLength(0))
                        {
                            if (Grid[i, j] != 9)
                                Grid[i, j]++;
                        }
                    }
                }   
            }
        }
    }
}
