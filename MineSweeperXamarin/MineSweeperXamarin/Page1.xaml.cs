using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MineSweeperXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            int n = 10;
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            StackLayout stack = new StackLayout() { BackgroundColor = Color.AliceBlue };
            Grid grid = new Grid()
            {
                RowSpacing = 1,
                ColumnSpacing = 1,
                HeightRequest = Width,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            grid.BackgroundColor = new Color(25, 0, 0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Frame frame = new Frame()
                    {
                        Content = new Label
                        {
                            Text = i.ToString() + j.ToString(),
                            TextColor = Color.Black,
                            BackgroundColor = Color.Aqua,
                            VerticalOptions = LayoutOptions.Fill,
                            HorizontalOptions = LayoutOptions.Fill,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0),
                            Padding = new Thickness(0)
                        },
                        Padding = new Thickness(0),
                        Margin = new Thickness(0),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };
                    frame.SizeChanged += (s, e) => {
                        frame.HeightRequest = frame.Width;
                    };
                    frame.GestureRecognizers.Add(recognizer);
                    recognizer.Tapped += Clicked;
                    grid.Children.Add(frame, i, j);
                }
            }
            stack.Children.Add(grid);
            stack.Children.Add(label);
            Content = stack;
        }
        Label label = new Label()
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            TextColor = Color.Black,
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
        };
        public void Clicked(object sender, EventArgs e)
        {
            Frame frame = (sender as Frame);
            Label lbl = (frame.Content as Label);
            lbl.BackgroundColor = Color.Yellow;
            label.Text = lbl.Text;
        }
    }
}