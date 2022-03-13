using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MineSweeperXamarin
{
    public abstract class TileCellBox : Frame
    {
        TapGestureRecognizer recognizer;
        int x, y;
        public TileCellBox()
        {
            Content = new Label
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
            (this.Content as Label).Text = x.ToString() + ", "+ y.ToString();
        }
        public virtual void Clicked(object sender, EventArgs e)
        {
            Trace.WriteLine("Hücrelerden bir tanesine tıklandı");
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
            (Content as Label).BackgroundColor = Color.Yellow;
        }
    }
    public class MineCell : TileCellBox
    {
        public MineCell() : base()
        {
        }
        public MineCell(int x,int y) : base(x, y)
        {
            (Content as Label).BackgroundColor = Color.Red;
        }
        public override void Clicked(object sender, EventArgs e)
        {
            base.Clicked(sender, e);
            (Content as Label).BackgroundColor = Color.Red;
        }
    }
}
