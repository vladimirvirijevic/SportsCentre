using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        bool Register(string name, string username, string password, string confirmPassword, string role);
        User Login(string username, string password);
    }
}
