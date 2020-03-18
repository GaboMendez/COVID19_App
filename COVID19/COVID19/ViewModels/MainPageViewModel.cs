using COVID19.Models;
using COVID19.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace COVID19.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        // Properties
        protected IApiService ApiService { get; set; }
        public Global Global { get; set; } 

        // Commands
        public DelegateCommand CountryCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Global Status";
            ApiService = new ApiService();

            Task.Run( () => this.GetGlobalStatus() ).Wait();
            CountryCommand = new DelegateCommand(async () => { await Country(); });
            RefreshCommand = new DelegateCommand(async () => { await Refresh(); });
        }

        private async Task Country()
        {
            if (!IsNotConnected)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading...",
                                                                        configuration: Constants.loadingDialogConfiguration))
                {
                    try
                    {
                        ObservableCollection<Country> countries = await ApiService.GetGlobalCountries();
                        if (countries.Count > 0)
                            await NavigationService.NavigateAsync(new Uri($"/{Constants.Country}", UriKind.Relative), ("Countries", countries));
                        else
                            await MaterialDialog.Instance.AlertAsync(message: "Something went wrong!!! \nTry Again!",
                                                                     title: null,
                                                                     acknowledgementText: "Got It",
                                                                     configuration: Constants.alertDialogConfiguration);
                    }

                    catch (Exception ex)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Something went wrong!!! \nTry Again!",
                                                                 title: null,
                                                                 acknowledgementText: "Got It",
                                                                 configuration: Constants.alertDialogConfiguration);
                    }
                }

            }else
                await MaterialDialog.Instance.AlertAsync(message: "Check your internet Connection and Try Again!",
                                                         title: null,
                                                         acknowledgementText: "Got It",
                                                         configuration: Constants.alertDialogConfiguration);
        }

        private async Task GetGlobalStatus()
        {
            if (!IsNotConnected)
                try
                {
                    Global = await ApiService.GetGlobalStatus();
                }
                catch (Exception)
                {
                    Global = new Global();
                }
            else           
                Global = new Global();            

            if (Global == null)
                Global = new Global();                            

            //message: "So this information is not updated!",
            //title: "Not Internet Connection!"
        }

        private async Task Refresh()
        {
            if (!IsNotConnected)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading...",
                                                                        configuration: Constants.loadingDialogConfiguration))
                {
                    try
                    {
                        Global = await ApiService.GetGlobalStatus();
                    }
                    catch (Exception)
                    {
                        Global = new Global();
                        await MaterialDialog.Instance.AlertAsync(message: "Something went wrong!!! \nTry Again!",
                                                                 title: null,
                                                                 acknowledgementText: "Got It",
                                                                 configuration: Constants.alertDialogConfiguration);
                        return;
                    }
                }

                await MaterialDialog.Instance.AlertAsync(message: "This information is updated!",
                                                         title: null,
                                                         acknowledgementText: "Got It",
                                                         configuration: Constants.alertDialogConfiguration);

            }
            else
                await MaterialDialog.Instance.AlertAsync(message: "Check your internet Connection and Try Again!",
                                                         title: null,
                                                         acknowledgementText: "Got It",
                                                         configuration: Constants.alertDialogConfiguration);
        }

    }
}
