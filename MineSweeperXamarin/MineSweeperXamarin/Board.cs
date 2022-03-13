using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MineSweeperXamarin
{
    public class Board : Grid
    {
        int n = 10;
        public Board()
        {
            RowSpacing = 1;
            ColumnSpacing = 1;
            HeightRequest = Width;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            BackgroundColor = Color.Red;

            CreateBoard();
        }
        public Board(int n) : this()
        {
            this.n = n;
        }
        public void CreateBoard()
        {
            Children.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TileCellBox Cell = new TileCell(i, j);
                    Children.Add(Cell, i, j);
                }
            }
            CreateMines(5);
        }
        private void CreateMines(int MineCount)
        {
            int x, y;
            for (int i = 0; i < MineCount; i++)
            {
                int[] coordinates = getRandomMine();
                x= coordinates[0];
                y = coordinates[1];
                TileCellBox mine = new MineCell(x, y);
                Children.Add(mine, x, y);
            }
        }
        int[] getRandomMine()
        {
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(n);
                y = rand.Next(n);
                Trace.WriteLine(x.ToString() + ", " + y.ToString());
            } while (Children[y*n+x].GetType() == typeof(MineCell));
            return new int[] { x, y };
        }
    }
}

/*private void CreateMines(int MineCount)
        {
            int x = 0, y = 0;

            for (int i = 0; i < MineCount; i++)
            {
                int[] coordinates = getRandomMines();
                x = coordinates[0];
                y = coordinates[1];
                Box mine = new Mine() { Name = x + "," + y };
                mine.Location = new Point(x * (mine.Width + cellPadding) + tableMargin, y * (mine.Height + cellPadding) + tableMargin);
                Trace.WriteLine(mine.Location.X);
                mine.Click += mineClick;
                mine.MouseDown += MineRightClick;
                
                boxes[x, y] = mine;
            }
        }
int[] getRandomMines()
        {
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(_n);
                y = rand.Next(_n);
            } while (boxes[x, y].GetType() == typeof(Mine));
            return new int[] { x, y };
        }*/