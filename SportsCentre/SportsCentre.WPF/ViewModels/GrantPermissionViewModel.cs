using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using SportsCentre.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class GrantPermissionViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private int selectedPermitId;
        private int selectedMemberId;
        private string messageForeground;

        private ObservableCollection<Permit> permits = new ObservableCollection<Permit>();
        private ObservableCollection<PermitInfo> permitsInfo = new ObservableCollection<PermitInfo>();
        private ObservableCollection<Member> members = new ObservableCollection<Member>();
        private ObservableCollection<MemberInfo> membersInfo = new ObservableCollection<MemberInfo>();
        #endregion

        #region Public Getters and Setters
        public int SelectedPermitId
        {
            get { return selectedPermitId; }
            set
            {
                selectedPermitId = value;
                OnPropertyChanged("SelectedPermitId");
            }
        }

        public int SelectedMemberId
        {
            get { return selectedMemberId; }
            set
            {
                selectedMemberId = value;
                OnPropertyChanged("SelectedMemberId");
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

        public ObservableCollection<Member> Members
        {
            get { return members; }
            set
            {
                members = value;
                OnPropertyChanged("Guests");
            }
        }
        public ObservableCollection<MemberInfo> MembersInfo
        {
            get { return membersInfo; }
            set
            {
                membersInfo = value;
                OnPropertyChanged("MembersInfo");
            }
        }

        public ObservableCollection<Permit> Permits
        {
            get { return permits; }
            set
            {
                permits = value;
                OnPropertyChanged("Permits");
            }
        }

        public ObservableCollection<PermitInfo> PermitsInfo
        {
            get { return permitsInfo; }
            set
            {
                permitsInfo = value;
                OnPropertyChanged("PermitsInfo");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        #endregion

        public GrantPermissionViewModel()
        {
            SelectedMemberId = -1;
            SelectedPermitId = -1;

            AddCommand = new RelayCommand(new Action<object>(Add));

            GetMembers();
            GetPermits();
            GetPermitsInfo();
        }

        private void Add(object obj)
        {
            if (selectedMemberId == -1 || SelectedPermitId == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    Member member = context.Members.Find(selectedMemberId);
                    Permit permit = context.Permits.Find(SelectedPermitId);

                    permit.Member = member;

                    context.SaveChanges();
                }

                MessageForeground = "Green";
                MessageText = "Permission Granted Successfully!";

                GetMembers();
                GetPermits();
                GetPermitsInfo();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetPermits()
        {
            permits.Clear();

            List<Permit> itemList = new List<Permit>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Permits
                    .Include(x => x.Member)
                    .Include(x => x.Training)
                    .ToList();
            }

            itemList.ForEach(x => {
                permits.Add(x);
            });
        }

        private void GetPermitsInfo()
        {
            permitsInfo.Clear();

            List<Permit> itemList = new List<Permit>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Permits
                    .Include(x => x.Member)
                    .Include(x => x.Training)
                    .ThenInclude(t => t.Court)
                    .Where(x => x.Member == null)
                    .ToList();

                foreach (var permit in itemList)
                {
                    var permitInfo = new PermitInfo(permit);
                    permitInfo.Info = $"Permit Id: {permit.Id}, Trainign Id: {permit.Training.Id}, Court: {permit.Training.Court.Name}";

                    permitsInfo.Add(permitInfo);
                }
            }
        }

        private void GetMembers()
        {
            members.Clear();
            membersInfo.Clear();

            List<Member> itemList = new List<Member>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Members.ToList();
            }

            itemList.ForEach(x => {
                members.Add(x);
                membersInfo.Add(new MemberInfo(x));
            });
        }
    }
}
