using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COVID19.ViewModels
{
    public class LanguagePageViewModel : ViewModelBase
    {
        // Properties
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

            ContinueCommand = new DelegateCommand(async () => { await Continue(); });
        }

        private async Task Continue()
        {
            await Task.Delay(100);

            if (LanguageBool)
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            else
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            await NavigationService.NavigateAsync(new Uri($"/{Constants.Main}", UriKind.Relative));
        }

        private void PlaceEnglish()
        {
            Text = "REAL-TIME \nINFORMATION";
            TitleLanguage = "SELECT A LANGUAGE:";
            English = "ENGLISH";
            Spanish = "SPANISH";
            TextButton = "CONTINUE";
            LanguageBool = false;
        }

        private void PlaceSpanish()
        {
            Text = "INFORMACION EN \nTIEMPO REAL";
            TitleLanguage = "SELECCIONA EL IDIOMA:";
            English = "INGLES";
            Spanish = "ESPAÑOL";
            TextButton = "CONTINUAR";
            LanguageBool = true;
        }
    }
}
