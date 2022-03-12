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
        TapGestureRecognizer recognizer = new TapGestureRecognizer();
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
            Trace.WriteLine("Cleaning Board");
            Children.Clear();
            Trace.WriteLine("Creating Board");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TileCell Cell = new TileCell(i, j);
                    
                    Cell.GestureRecognizers.Add(recognizer);
                    recognizer.Tapped += Clicked;
                    Children.Add(Cell, i, j);
                    Cell.SizeChanged += deneme;
                }
            }
            Trace.WriteLine("Board Created");
        }
        public void deneme ( object sender, EventArgs e)
        {
            TileCell cell = (sender as TileCell);
            cell.HeightRequest = cell.Width;
        }
        public void Clicked(object sender, EventArgs e)
        {
            ((sender as Frame).Content as Label).BackgroundColor = Color.White; 
        }
    }
}
