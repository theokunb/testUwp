using System.Windows.Input;
using testUwp.Core;

namespace testUwp.ViewModel
{
    public class HistoryViewModel : BaseViewModel
    {
        public HistoryViewModel()
        {
            NavigateBack = new RelayCommand(param => OnBackNavigate(param));
        }

        public ICommand NavigateBack { get; private set; }

        private void OnBackNavigate(object param)
        {
            Frame.GoBack();
        }
    }
}
