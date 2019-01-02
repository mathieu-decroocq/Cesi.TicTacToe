namespace Cesi.TicTacToe.ViewModels.Common
{
    public interface INavigationService
    {
        void GoForward();
        void GoBack();
        bool Navigate(string page);
    }
}
