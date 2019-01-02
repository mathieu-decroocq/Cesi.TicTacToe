using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cesi.TicTacToe.ViewModels.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel(INavigationService navigationService)
        {
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
