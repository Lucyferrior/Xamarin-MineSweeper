using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MineSweeperXamarin
{
    public class Board : Grid
    {
        TapGestureRecognizer mineRecognizer;
        int n = 10;

        //public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler GameOver; 


        protected virtual void onGameOver(object sender, EventArgs e)
        {
            GameOver?.Invoke(sender, EventArgs.Empty);
        }
        public Board() : this(10)
        {

        }
        public Board(int N)
        {
            this.n = N;
            RowSpacing = 1;
            ColumnSpacing = 1;
            HeightRequest = Width;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            BackgroundColor = Color.Yellow;
            mineRecognizer = new TapGestureRecognizer();
            CreateBoard();
        }
        public void CreateBoard()
        {
            Trace.WriteLine(n.ToString() + " KARELİ OYUN OLUŞTURULUYOR");
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
            CreateMines(20);
            AssignDirections();
        }
        private void AssignDirections()
        {
            TileCellBox tempBox;

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    tempBox = GetChild(x, y);
                    if (x > 0)          //Left
                    {
                        tempBox.Left = GetChild(x - 1, y);

                        if(y > 0)       //LeftTop
                        {
                            tempBox.LeftTop = GetChild(x - 1, y - 1);
                        }

                        if (y < n - 1)  //LeftBottom
                        {
                            tempBox.LeftBottom = GetChild(x - 1, y + 1);
                        }
                    }
                    if (x < n - 1)      //Right
                    {
                        tempBox.Right = GetChild(x + 1, y);

                        if (y > 0)      //RightTop
                        {
                            tempBox.RightTop = GetChild (x + 1, y - 1);
                        }
                        if(y < n - 1)   //RightBottom
                        {
                            tempBox.RightBottom = GetChild(x + 1, y + 1);
                        }
                    }
                    if (y > 0)          //Top
                    {
                        tempBox.Top = GetChild(x, y - 1);
                    }
                    if (y < n - 1)      //Bottom
                    {
                        tempBox.Bottom = GetChild(x, y + 1);
                    }
                    tempBox.AssignDirections();
                }
            }
        }
        public TileCellBox GetChild(Point point)
        {
            int index = (int)(point.Y * n + point.X);
            return Children[index] as TileCellBox;
        }
        public TileCellBox GetChild(int x, int y)
        {
            return GetChild(new Point(x, y));
        }
        public void CreateMines(int MineCount) // mayınların oluşturulması geliştirilmeli
        {
            int x, y;
            TileCellBox mine;
            for (int i = 0; i < MineCount; i++)
            {
                int[] coordinates = getRandomMine();
                x = coordinates[0];
                y = coordinates[1];
                mine = new MineCell(x, y);
                mine.GestureRecognizers.Add(mineRecognizer);
                //mineRecognizer.Tapped += onGameOver;
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
            } while (GetChild(x, y).GetType() == typeof(MineCell));
            return new int[] { x, y };
        }
    }
}