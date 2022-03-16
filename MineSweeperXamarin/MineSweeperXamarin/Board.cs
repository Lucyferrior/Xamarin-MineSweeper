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
        int n = 10;

        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler GameOver;
        public event EventHandler GameWin;
        protected virtual void onGameWin(object sender, EventArgs e)
        {
            GameWin?.Invoke(sender, EventArgs.Empty);
        }
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
            CreateBoard();
        }
        public void CreateBoard()
        {
            Trace.WriteLine(n.ToString() + " KARELİ OYUN OLUŞTURULUYOR");
            Children.Clear();
            TileCellBox cell;
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    cell = new TileCell(x, y);
                    Children.Add(cell, x, y);
                    cell.recognizer.Tapped += isWin;
                }
            }
            CreateMines(20);
            AssignDirections();
        }
        private void isWin(object sender, EventArgs e)
        {


            if (check())
            {
                Trace.WriteLine("oyun kazanıldı");
                GameWin(sender, EventArgs.Empty);
            }

            bool check()
            {
                Trace.WriteLine("kazanma durumu kontrol ediliyor");
                foreach (TileCellBox item in Children)
                {
                    if(item.GetType() == typeof(TileCell)) //normal hucrelerde
                    {
                        if (item.Clickable) // tıklanmamış buton var mı diye kontrol edliyoruz
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
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
            TileCellBox mine = null;
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
            box.recognizer.Tapped += onGameOver;

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