using COVID19.Models;
using COVID19.Services;
using COVID19.Resx;
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
using System.Threading;
using System.Globalization;
using Prism.Services.Dialogs;

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
        public DelegateCommand DetailCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDialogService dialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Global Status";
            ApiService = new ApiService();

            CountryCommand = new DelegateCommand(async () => { await Country(); });
            RefreshCommand = new DelegateCommand(async () => { await Refresh(); });
            DetailCommand = new DelegateCommand( () =>
            {
                dialogService.ShowDialog("DetailOptionDialogView", CloseDialogCallback);
            });
        }

        private async Task Country()
        {
            if (!IsNotConnected)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: AppResources.MsgLoad,
                                                                        configuration: Constants.loadingDialogConfiguration))
                {
                    try
                    {
                        ObservableCollection<Country> countries = await ApiService.GetGlobalCountries();
                        foreach (var item in countries)
                        { }

                        if (countries.Count > 0)
                            await NavigationService.NavigateAsync(new Uri($"/{Constants.Country}", UriKind.Relative), ("Countries", countries));
                        else
                            await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgErrorTitle,
                                                                     title: null,
                                                                     acknowledgementText: AppResources.MsgOk,
                                                                     configuration: Constants.alertDialogConfiguration);
                    }

                    catch (Exception)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgError,
                                                                 title: AppResources.MsgErrorTitle,
                                                                 acknowledgementText: AppResources.MsgOk,
                                                                 configuration: Constants.alertDialogConfiguration);
                    }
                }

            }
            else
                await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgInternet,
                                                         title: null,
                                                         acknowledgementText: AppResources.MsgOk,
                                                         configuration: Constants.alertDialogConfiguration);
        }

        private async Task Refresh()
        {
            if (!IsNotConnected)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: AppResources.MsgLoad,
                                                                        configuration: Constants.loadingDialogConfiguration))
                {
                    try
                    {
                        Global = await ApiService.GetGlobalStatus();
                    }
                    catch (Exception)
                    {
                        Global = new Global();
                        await MaterialDialog.Instance.AlertAsync( message: AppResources.MsgError,
                                                                  title: AppResources.MsgErrorTitle,
                                                                  acknowledgementText: AppResources.MsgOk,
                                                                  configuration: Constants.alertDialogConfiguration);
                        return;
                    }
                }

                await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgUpdate,
                                                         title: null,
                                                         acknowledgementText: AppResources.MsgOk,
                                                         configuration: Constants.alertDialogConfiguration);

            }
            else
                await MaterialDialog.Instance.AlertAsync(message: AppResources.MsgInternet,
                                                         title: null,
                                                         acknowledgementText: AppResources.MsgOk,
                                                         configuration: Constants.alertDialogConfiguration);
        }

        private void CloseDialogCallback(IDialogResult dialogResult) { }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.ContainsKey("Global"))
            {
                Global = (Global)parameters["Global"];
            }
        }

    }
}
