using COVID19.Models;
using COVID19.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryPage : ContentPage
    {
        public CountryPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as CountryPageViewModel;

            countryListView.IsVisible = true;            
            try
            {
                List<Country> CountryList = _container.Countries.Where(i => i.country.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
                var CountryString = new List<string>();

                foreach (var item in CountryList)
                    if (item != null)
                        CountryString.Add(item.country);

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    countryListView.IsVisible = false;
                else
                    countryListView.ItemsSource = CountryString;
            }
            catch (Exception)
            {
                countryListView.IsVisible = false;
            }            
        }

        private void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            String listsd = e.Item as string;
            searchBar.Text = listsd;
            countryListView.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }
        private ObservableCollection<Country> ToObservable(List<Country> enumerable)
        {
            var ret = new ObservableCollection<Country>();
            foreach (var cur in enumerable)
            {
                ret.Add(cur);
            }
            return ret;
        }
    }
}