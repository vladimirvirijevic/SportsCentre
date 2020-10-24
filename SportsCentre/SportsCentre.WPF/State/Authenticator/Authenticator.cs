using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.State.Authenticator
{
    public class Authenticator : IAuthenticator
    {
        public User CurrenUser { get; private set; }
        public bool IsLoggedIn => CurrenUser != null;

        public User Login(string username, string password)
        {
            IAuthenticationService authenticationService = new AuthenticationService();

            return authenticationService.Login(username, password);
        }

        public void Logout()
        {
            CurrenUser = null;
        }

        public bool Register(string name, string username, string password, string confirmPassword, string role)
        {
            IAuthenticationService authenticationService = new AuthenticationService();

            return authenticationService.Register(name, username, password, confirmPassword, role);
        }
    }
}
