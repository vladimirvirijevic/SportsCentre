using SportsCentre.WPF.ViewModels;
using SportsCentre.WPF.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SportsCentre.WPF.Commands
{
    public class LogoutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public LogoutCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext is AdminViewModel || item.DataContext is ManagerViewModel || item.DataContext is SalesViewModel)
                {
                    item.Close();
                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                }
            }
        }
    }
}
