using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class MembersViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? birthdate;
        private string firstname;
        private string lastname;
        private string address;
        private string phone;

        private ObservableCollection<Member> members = new ObservableCollection<Member>();
        #endregion

        #region Public Getters and Setters
        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
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
        public DateTime? Birthdate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
                OnPropertyChanged("Birthdate");
            }
        }

        public ObservableCollection<Member> Members
        {
            get { return members; }
            set
            {
                members = value;
                OnPropertyChanged("Members");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public MembersViewModel()
        {
            Birthdate = null;
            Firstname = "";
            Lastname = "";
            Address = "";
            Phone = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetMembers();
        }

        private void Add(object obj)
        {
            if (Birthdate == null || Firstname == "" || Lastname == "" || Address == "" || Phone == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var entity = new Member
                    {
                        Birthdate = Birthdate.Value.Date.ToShortDateString(),
                        Firstname = Firstname,
                        Lastname = Lastname,
                        Phone = Phone,
                        Address = address 
                    };

                    context.Members.Add(entity);
                    context.SaveChanges();
                }

                Birthdate = null;
                Firstname = "";
                Lastname = "";
                Address = "";
                Phone = "";

                MessageForeground = "Green";
                MessageText = "Members Successfully Added!";
                GetMembers();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetMembers()
        {
            members.Clear();

            List<Member> itemList = new List<Member>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Members.ToList();
            }

            itemList.ForEach(x => members.Add(x));
        }

        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Member)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var member = context.Members.Find(id);
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete member";
                return;
            }

            GetMembers();

            MessageForeground = "Green";
            MessageText = "Member deleted successfully!";
        }
    }
}
