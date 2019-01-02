using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cesi.TicTacToe.WpfApp.Navigation;
using Cesi.TicTacToe.WpfApp.Views;

namespace Cesi.TicTacToe.WpfApp
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static NavigationService Navigation;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Navigation = new NavigationService(mainWindow.MainFrame);
            Navigation.Navigate<HomePage>();
        }
    }
}
