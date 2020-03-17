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

namespace COVID19.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        // Properties
        ObservableCollection<Country> Countries { get; set; }
        List<Country> list { get; set; }
        Country Country { get; set; }

        // Commands
        public DelegateCommand SearchCommand { get; set; }
        public CountryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            SearchCommand = new DelegateCommand(async () => { await Search(); });
        }

        private async Task Search()
        {
            await Task.Delay(100);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.ContainsKey("Countries"))
            {
                Countries = (ObservableCollection<Country>)parameters["Countries"];
                //list = (List<Country>)parameters["Countries"];
                var country = Countries.Where(x => x.country.Equals("Dominican Republic")).FirstOrDefault();

                if (country != null)
                {
                    Country = country;
                    Title = Country.country;
                }
            }
        }
    }
}
