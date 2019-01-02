using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cesi.TicTacToe.Models;
using Cesi.TicTacToe.ViewModels;

namespace Cesi.TicTacToe.WpfApp.Views
{
    /// <summary>
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private GameViewModel gameViewModel;
        public GamePage()
        {
            InitializeComponent();
            gameViewModel = new GameViewModel(App.Navigation);
            this.DataContext = gameViewModel;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Field field = ((Button)sender).Tag as Field;

            try
            {
                gameViewModel.Play(field);
                Player winner = gameViewModel.GetWinner();
                if (winner != null)
                {
                    MessageBox.Show($"Joueur {winner.Name} a gagné.");
                    gameViewModel.Reset();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
    }
}
