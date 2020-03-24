using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.ViewModels
{
    public class DetailOptionViewModel : IDialogAware
    {
        public DelegateCommand CloseCommand { get; set; }
        public DetailOptionViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
