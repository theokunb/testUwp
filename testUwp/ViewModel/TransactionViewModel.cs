using System.Windows.Input;
using testUwp.Core;

namespace testUwp.ViewModel
{
    public class TransactionViewModel : BaseViewModel
    {
        public TransactionViewModel()
        {
            NavigateBack = new RelayCommand(param => OnBackNavigate(param));
        }

        public ICommand NavigateBack { get;private set; }

        private void OnBackNavigate(object param)
        {
            Frame.GoBack();
        }
    }
}
