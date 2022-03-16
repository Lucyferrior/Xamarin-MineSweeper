using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MineSweeperXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        Board board;
        public Page1()
        {
            InitializeComponent();
            int n = 10;
            board = new Board(n);
            StackLayout stack = new StackLayout()
            {
                BackgroundColor = Color.YellowGreen
            };
            stack.Children.Add(board);
            Button restartBtn = new Button()
            {
                Text = "Yeniden başlat"
            };
            restartBtn.Clicked += (s, e) => { board.CreateBoard(); };
            stack.Children.Add(restartBtn);
            Content = stack;
            board.GameOver += denemeOver;
            board.GameWin += denemeWin;
        }
        public void denemeWin(object sender, EventArgs e)
        {
            DisplayAlert("Tebrikler", "Kazandın!", "Oynamaya Devam Et");
            Trace.WriteLine("CONGRATS!! You WIN");
        }
        public void denemeOver(object sender, EventArgs e)
        {
            DisplayAlert("Ünzünüm", "Kaybettin!", "Yeniden Başlat");
            //board.CreateBoard();
        }
        

    }
}