using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace COVID19
{
    public static class Config
    {
        public static string api_url = "https://corona.lmao.ninja";
    }

    public static class Constants
    {
        //Navigation
        public static string Navigation = "NavigationPage";

        //Pages
        public static string Main = "MainPage";
        public static string Country = "CountryPage";   
        
        //Dialog
        public static MaterialLoadingDialogConfiguration loadingDialogConfiguration = new MaterialLoadingDialogConfiguration()
        {
            BackgroundColor = Color.FromHex("#00675b"),
            MessageTextColor = Color.White.MultiplyAlpha(0.8),
            TintColor = Color.White,
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#00251a").MultiplyAlpha(0.32)
        };

        public static MaterialAlertDialogConfiguration alertDialogConfiguration = new MaterialAlertDialogConfiguration
        {
            BackgroundColor = Color.FromHex("#00675b"),
            TitleTextColor = Color.White,
            MessageTextColor = Color.White.MultiplyAlpha(0.8),
            TintColor = Color.White,
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#00251a").MultiplyAlpha(0.32),
            ButtonAllCaps = false,
        };


}
}
