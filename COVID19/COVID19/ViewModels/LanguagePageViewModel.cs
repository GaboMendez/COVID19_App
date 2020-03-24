using COVID19.Models;
using COVID19.Resx;
using COVID19.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace COVID19.ViewModels
{
    public class LanguagePageViewModel : ViewModelBase
    {
        // Properties
        protected IApiService ApiService { get; set; }
        public Global Global { get; set; }
        public string InternetMsg { get; set; }
        public string OkayMsg { get; set; }
        public string LoadingMsg { get; set; }
        public string Text { get; set; }
        public string TitleLanguage { get; set; }
        public string English { get; set; }
        public string Spanish { get; set; }
        public string TextButton { get; set; }

        private bool _languageBool;
        public bool LanguageBool
        {
            get
            {
                return _languageBool;
            }
            set
            {
                _languageBool = value;
                if (_languageBool)
                    PlaceSpanish();
                else
                    PlaceEnglish();
            }
        }

        //Commands
        public DelegateCommand ContinueCommand { get; set; }

        public LanguagePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            PlaceEnglish();

            ApiService = new ApiService();
            ContinueCommand = new DelegateCommand(async () => { await Continue(); });
        }

        private async Task Continue()
        {
            if (!IsNotConnected)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: LoadingMsg,
                                                                        configuration: Constants.loadingDialogConfiguration))
                {
                    if (LanguageBool)
                    {
                        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-ES");
                        try
                        {
                            Global = await ApiService.GetGlobalStatus();
                        }
                        catch (Exception)
                        {
                            await MaterialDialog.Instance.AlertAsync(   message: "Por favor inténtalo de nuevo!",
                                                                        title: "Algo salió mal...",
                                                                        acknowledgementText: "Entendido",
                                                                        configuration: Constants.alertDialogConfiguration);
                            return;
                        }

                        await NavigationService.NavigateAsync(new Uri($"/{Constants.Main}", UriKind.Relative), ("Global", Global));
                    }
                    else
                    {
                        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
                        try
                        {
                            Global = await ApiService.GetGlobalStatus();
                        }
                        catch (Exception)
                        {
                            await MaterialDialog.Instance.AlertAsync(   message: "Please try again!",
                                                                        title: "Something went wrong...",
                                                                        acknowledgementText: "Got It",
                                                                        configuration: Constants.alertDialogConfiguration);
                            return;
                        }

                        await NavigationService.NavigateAsync(new Uri($"/{Constants.Main}", UriKind.Relative), ("Global", Global));
                    }
                }
                

            }
            else
                await MaterialDialog.Instance.AlertAsync(message: InternetMsg,
                                                         title: null,
                                                         acknowledgementText: OkayMsg,
                                                         configuration: Constants.alertDialogConfiguration);

        }

        private void PlaceEnglish()
        {
            Text = "REAL-TIME \nINFORMATION";
            TitleLanguage = "SELECT A LANGUAGE:";
            English = "ENGLISH";
            Spanish = "SPANISH";
            TextButton = "CONTINUE";
            InternetMsg = "Check your internet Connection and Try Again!";
            OkayMsg = "Got It";
            LoadingMsg = "Loading...";
            LanguageBool = false;
        }

        private void PlaceSpanish()
        {
            Text = "INFORMACION EN \nTIEMPO REAL";
            TitleLanguage = "SELECCIONA EL IDIOMA:";
            English = "INGLES";
            Spanish = "ESPAÑOL";
            TextButton = "CONTINUAR";
            InternetMsg = "¡Comprueba tu conexión a Internet! \ny vuelve a intentarlo...";
            OkayMsg = "Entendido";
            LoadingMsg = "Cargando...";
            LanguageBool = true;
        }

        private void SetCulture(string culture)
        {
            // es-ES | en-US
            CultureInfo culture_info = new CultureInfo(culture);

            // Set the thread culture and UI culture.
            Thread.CurrentThread.CurrentUICulture = culture_info;
            Thread.CurrentThread.CurrentCulture = culture_info;
            
            CultureInfo.CurrentCulture = culture_info;
            CultureInfo.CurrentUICulture = culture_info;
            CultureInfo.DefaultThreadCurrentCulture = culture_info;
            CultureInfo.DefaultThreadCurrentUICulture = culture_info;
        }
    }
}
