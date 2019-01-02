using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cesi.TicTacToe.Models;
using Cesi.TicTacToe.ViewModels.Common;

namespace Cesi.TicTacToe.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private ICommand goToGamePageCommand;
        

        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }



        public ICommand GoToGamePageCommand
        {
            get
            {
                if (goToGamePageCommand == null)
                {
                    goToGamePageCommand = new RelayCommand((param) =>
                    {
                        this.navigationService.Navigate("GamePage");
                    }, null);
                }

                return goToGamePageCommand;
            }
        } 
    }
}
