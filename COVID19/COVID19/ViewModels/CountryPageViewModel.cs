using COVID19.Models;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Commands;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;
using COVID19.Resx;

namespace COVID19.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        // Properties
        public ObservableCollection<Country> Countries { get; set; }
        public Country Country { get; set; }
        public string SearchTerm { get; set; }

        private ObservableCollection<string> _countryList; 
        public ObservableCollection<string> CountryList
        {
            get
            {
                if (_countryList == null)
                    _countryList = new ObservableCollection<string>();
                return _countryList;
            }
            set
            {
                if (CountryList != value)
                {
                    _countryList = value;
                }
            }
        }    

        private string _selectedCountry;
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                if (_selectedCountry != null)
                {
                    var country = Countries.Where(x => x.country.Equals(_selectedCountry)).FirstOrDefault();
                    if (country != null)
                    {
                        Country = country;
                        Title = Country.country;
                        SearchTerm = null;
                    }
                    _ = MaterialDialog.Instance.AlertAsync( message: $"{AppResources.MsgCountryStatus}\n{Title}",
                                                            title: null,
                                                            acknowledgementText: AppResources.MsgOk,
                                                            configuration: Constants.alertDialogConfiguration);
                }
            }
        }

        // Commands
        public DelegateCommand SearchCommand { get; set; }


        public CountryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            SearchCommand = new DelegateCommand(async () => { await Search(); });
        }

        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {                
                var country = Countries.Where( x => x.country.ToLower().Contains(SearchTerm.ToLower()) ).FirstOrDefault();

                if (country != null)
                {
                    Country = country;
                    Title = Country.country;
                    SearchTerm = Country.country;
                }else
                    await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgCountryError,
                                                             title: AppResources.MsgError,
                                                             acknowledgementText: AppResources.MsgOk,
                                                             configuration: Constants.alertDialogConfiguration);

                SelectedCountry = null;
            }
            else
                await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgFieldEmpty,
                                                         title: AppResources.MsgError,
                                                         acknowledgementText: AppResources.MsgOk,
                                                         configuration: Constants.alertDialogConfiguration);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.ContainsKey("Countries"))
            {
                Countries = (ObservableCollection<Country>)parameters["Countries"];
                var country = Countries.Where(x => x.country.Equals("Dominican Republic")).FirstOrDefault();

                if (country != null)
                {
                    Country = country;
                    Title = Country.country;

                    foreach (var item in Countries)
                    {
                        if (item.country != null)
                            CountryList.Add(item.country);
                    }

                    _ = MaterialDialog.Instance.AlertAsync( message: $"{AppResources.MsgCountryStatus}\n{Title}",
                                                            title: null,
                                                            acknowledgementText: AppResources.MsgOk,
                                                            configuration: Constants.alertDialogConfiguration);
                }
                
            }
        }
    }
}
