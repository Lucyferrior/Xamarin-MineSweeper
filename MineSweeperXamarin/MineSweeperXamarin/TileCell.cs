using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MineSweeperXamarin
{
    public class TileCell : Frame
    {
        TapGestureRecognizer recognizer;
        int x, y;
        public TileCell()
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
        public TileCell(int x, int y) : this()
        {
            this.x = x;
            this.y = y;
            (this.Content as Label).Text = x.ToString() + ", "+ y.ToString();
        }
        public void Clicked(object sender, EventArgs e)
        {
            Trace.WriteLine("Hücrelerden bir tanesine tıklandı");
        }
    }
}
