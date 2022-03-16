using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MineSweeperXamarin
{
    public abstract class TileCellBox : Frame
    {
        public TapGestureRecognizer recognizer;
        public int x, y;
        public TileCellBox Left, Right, Top, Bottom;
        public TileCellBox LeftTop, RightTop, LeftBottom, RightBottom;
        public List<TileCellBox> boxAround;
        public Label label;
        public Boolean Clickable = true;
        public TileCellBox()
        {
            label = new Label()
            {
                TextColor = Color.Black,
                BackgroundColor = Color.Aqua,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0)
            };
            Content = label;
            
            Padding = new Thickness(0);
            Margin = new Thickness(0);
            HorizontalOptions = LayoutOptions.FillAndExpand;

            this.SizeChanged += (s, e) =>
            {
                HeightRequest = Width;
            };
            
            recognizer = new TapGestureRecognizer();
            this.GestureRecognizers.Add(recognizer);
            recognizer.Tapped += Clicked;
        }
        public TileCellBox(int x, int y) : this()
        {
            this.x = x;
            this.y = y;
            //label.Text = x.ToString() + ", "+ y.ToString();
        }
        public void AssignDirections()
        {
            boxAround = new List<TileCellBox>();

            boxAround.Add(LeftTop);
            boxAround.Add(Top);
            boxAround.Add(RightTop);
            boxAround.Add(Right);
            boxAround.Add(RightBottom);
            boxAround.Add(Bottom);
            boxAround.Add(LeftBottom);
            boxAround.Add(Left);
        }
        public virtual void Clicked(object sender, EventArgs e)
        {
            Trace.WriteLine("( " + x.ToString() + ", " + y.ToString() + " ) butonuna tıklandı" );
        }
    }
    public class TileCell : TileCellBox
    {
        public TileCell() : base()
        {
        }
        public TileCell(int x, int y) : base(x, y)
        {
        }
        public override void Clicked(object sender, EventArgs e)
        {
            base.Clicked(sender, e);

            AutoClick(sender as TileCellBox);
        }
        private void AutoClick(TileCellBox cellBox)
        {
            cellBox.label.BackgroundColor = Color.Yellow;

            if (cellBox.Clickable)
            {
                cellBox.Clickable = false;

                int mineCount = CheckMines();
                if (mineCount == 0)
                {
                    foreach (TileCellBox item in cellBox.boxAround)
                    {
                        if (item != null)
                        {
                            AutoClick(item);
                        }
                    }
                }
                else
                {
                    Trace.WriteLine("( " + cellBox.x.ToString() + ", " + cellBox.y.ToString() + " ) etrafında " + mineCount.ToString() + " Mayın var");
                    cellBox.label.Text = mineCount.ToString();
                }

                int CheckMines()
                {
                    int _mineCount = 0;
                    foreach (TileCellBox item in cellBox.boxAround)
                    {
                        if (item != null)
                        {
                            if (item.GetType() == typeof(MineCell))
                            {
                                _mineCount++;
                            }
                        }
                    }
                    return _mineCount;
                }
            }
        }
    }
    public class MineCell : TileCellBox
    {
        public MineCell() : base()
        {
        }
        public MineCell(int x,int y) : base(x, y)
        {
            label.BackgroundColor = new Color(25,0,0,0.5);
        }
        public override void Clicked(object sender, EventArgs e)
        {
            base.Clicked(sender, e);
            label.BackgroundColor = Color.Brown;
            Trace.WriteLine("OYUN BİTTİ AHMAK");

        }

    }
}
