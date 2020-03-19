using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PancakeView : ContentView
    {
        public bool Selected { get; set; } = false;

        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message", typeof(string), typeof(PancakeView), null, BindingMode.Default);
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public string Image
        {
            set => image.Source = value;
        }
        public string Title
        {
            set => title.Text = value;
        }
        public double TitleSize 
        {
            set => title.FontSize = value;
        }
        public double MessageSize 
        {
            set => message.FontSize = value;
        }


        public PancakeView()
        {
            InitializeComponent();
        }

        private void Pancake_OnTapped(object sender, EventArgs e)
        {
            if (Selected)
            {
                Selected = false;

                title.FontSize -= 1;
                title.TextColor = Color.FromHex("00675b");
                message.FontSize -= 1;
                message.TextColor = Color.FromHex("00675b");
                pancake.BackgroundColor = Color.FromHex("FFFFFF");
                pancake.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                Selected = true;

                title.FontSize += 1;
                title.TextColor = Color.White;
                message.FontSize += 1;
                message.TextColor = Color.White;
                pancake.BackgroundColor = Color.FromHex("009688");
                pancake.Margin = new Thickness(0, -5, 0, 5);
            }
        }
    }
}