using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using SportsCentre.WPF.State.Authenticator;
using SportsCentre.WPF.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        #region Private Properties
        private string messageText;
        private string messageForeground;
        private string username;
        private string password;
        private readonly IAuthenticator authenticator;

        #endregion

        #region Public Getters and Setters
        public string MessageText
        {
            get { return messageText; }
            set
            {
                messageText = value;
                OnPropertyChanged("MessageText");
            }
        }
        public string MessageForeground
        {
            get { return messageForeground; }
            set
            {
                messageForeground = value;
                OnPropertyChanged("MessageForeground");
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands
        public ICommand LoginCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            Username = "";
            Password = "";

            LoginCommand = new RelayCommand(new Action<object>(Login));
            authenticator = new Authenticator();
        }

        private void Login(object obj)
        {
            PasswordBox passwordBox = (PasswordBox)obj;
            string password = string.Empty;

            if (passwordBox != null)
            {
                password = passwordBox.Password;
            }

            if (Username == "" || password == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                User loggedUser = authenticator.Login(Username, password);

                if (loggedUser == null)
                {
                    MessageForeground = "Red";
                    MessageText = "Wrong creditentials!";
                    return;
                }

                MessageForeground = "Green";
                MessageText = "Login was successful!";

                Window window = null;

                if (loggedUser.Role == "Admin")
                {
                    window = new AdminView();
                }
                else if (loggedUser.Role == "Manager")
                {
                    window = new ManagerView();
                }
                else if (loggedUser.Role == "Sales")
                {
                    window = new SalesView();
                }

                if (window != null)
                {
                    window.Show();

                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.DataContext == this) item.Close();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }
    }
}
