using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        public CountryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
            Title = "Country";
        }
    }
}
