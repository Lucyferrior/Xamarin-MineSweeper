using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            StackLayout stack = new StackLayout() 
            {
                BackgroundColor = Color.AliceBlue
            };
            stack.Children.Add(new Board());
            Content = stack;
        }
    }

}