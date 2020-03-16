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
        public string Image
        {
            set => image.Source = value;
        }
        public string Title
        {
            set => title.Text = value;
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message", typeof(string), typeof(PancakeView), null, BindingMode.Default);
        public string Message 
        {
            get { return (string)GetValue(MessageProperty); } 
            set { SetValue(MessageProperty, value); } 
        }

        public PancakeView()
        {
            InitializeComponent();
        }
    }
}