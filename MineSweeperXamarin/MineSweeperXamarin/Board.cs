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
            BackgroundColor = Color.Yellow;
            ChildAdded += (sender, e) =>
            {
                Trace.WriteLine(e.Element.ToString());
            };
            CreateBoard();
        }
        public Board(int n) : this()
        {
            this.n = n;
        }
        public void CreateBoard()
        {
            Children.Clear();
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    TileCellBox cell = new TileCell(x, y);
                    Children.Add(cell, x, y);
                    TapGestureRecognizer recognizer = new TapGestureRecognizer();
                    cell.GestureRecognizers.Add(recognizer);
                }
            }
            CreateMines(100);
        }
        public void CreateMines(int MineCount)
        {
            int x, y;
            TileCellBox mine;
            for (int i = 0; i < MineCount; i++)
            {
                int[] coordinates = getRandomMine();
                x = coordinates[0];
                y = coordinates[1];
                mine = new MineCell(x, y);
                ChangeAt(mine, x, y);
            }
            Trace.WriteLine("ÇOCUKLARIN SAYISI: " + Children.Count);
        }
        private void ChangeAt(TileCellBox box, int x, int y)
        {
            int index = y * n + x;
            Children.RemoveAt(index);
            Children.Insert(index, box);
            Children.Add(box, x, y);
        }
        int[] getRandomMine()
        {
            Random rand = new Random();
            int counter = 0;
            int x, y;
            do
            {
                x = rand.Next(n);
                y = rand.Next(n);
                counter++;
            } while (Children[y * n + x].GetType() == typeof(MineCell));
            return new int[] { x, y };
            
        }
    }
}