using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.State.Authenticator
{
    public interface IAuthenticator
    {
        User CurrenUser { get; }
        bool IsLoggedIn { get; }

        bool Register(string name, string username, string password, string confirmPassword, string role);
        User Login(string username, string password);
        public void Logout();
    }
}
