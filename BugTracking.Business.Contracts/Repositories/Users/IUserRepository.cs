﻿using BugTracking.Business.Contracts.Repositories.General;
using BugTracking.Database.Domain;
using System.Collections.Generic;

namespace BugTracking.Business.Contracts.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetById(int id);

        new List<User> GetAll();

        int EmployeeCount();

        new void Delete(User user);

        List<User> GetFreeEmployees();

        User Authenticate(string email, string password);

        bool ChangePassword(int id, string oldPassword, string newPassword);
    }
}
