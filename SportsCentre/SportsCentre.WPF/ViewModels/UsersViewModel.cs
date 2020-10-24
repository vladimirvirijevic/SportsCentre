using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using SportsCentre.WPF.State.Authenticator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private string name;
        private string username;
        private string password;
        private string selectedRole;

        private readonly IAuthenticator authenticator;

        private ObservableCollection<string> roles = new ObservableCollection<string>();
        private ObservableCollection<User> users = new ObservableCollection<User>();
        #endregion

        #region Public Getters and Setters
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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

        public string SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }

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

        public ObservableCollection<string> Roles
        {
            get { return roles; }
            set
            {
                roles = value;
                OnPropertyChanged("Roles");
            }
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public UsersViewModel()
        {
            Name = "";
            Username = "";
            Password = "";
            SelectedRole = "";

            authenticator = new Authenticator();

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            roles.Add("Admin");
            roles.Add("Manager");
            roles.Add("Sales");

            GetUsers();
        }

        private void Add(object obj)
        {
            if (Username == "" || Password == "" || Name == "" || SelectedRole == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                bool success = authenticator.Register(Name, Username, Password, Password, SelectedRole);

                if (!success)
                {
                    MessageForeground = "Red";
                    MessageText = "There was an error!";
                    return;
                }
                
                Name = "";
                Username = "";
                Password = "";
                SelectedRole = "";

                MessageForeground = "Green";
                MessageText = "User Successfully Added!";
                GetUsers();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetUsers()
        {
            users.Clear();

            List<User> itemList = new List<User>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Users.ToList();
            }

            itemList.ForEach(x => users.Add(x));
        }

        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((User)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var user = context.Users.Find(id);
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete user";
                return;
            }

            GetUsers();

            MessageForeground = "Green";
            MessageText = "User deleted successfully!";
        }
    }
}
