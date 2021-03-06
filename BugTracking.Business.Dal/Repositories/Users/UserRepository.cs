﻿using BugTracking.Business.Contracts.Repositories.Users;
using BugTracking.Business.Dal.Repositories.General;
using BugTracking.Database.Domain;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;
using Z.EntityFramework.Plus;

namespace BugTracking.Business.Dal.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BugTrackingEntities context) : base(context)
        {
            Context = context;
        }

        public int EmployeeCount()
        {
            return Context.Users
                .Where(user => user.IsActive)
                .Count();
        }

        public User GetById(int id)
        {
            return Context.Users
                .Where(user => user.Id == id)
                .Include(user => user.User_Roles)
                .IncludeFilter(user => user.Bugs
                    .Where(bug => bug.Bug_Status.BugStatus != "Solved"))
                .FirstOrDefault();
        }

        List<User> IUserRepository.GetAll()
        {
            return Context.Users
                .Include(user => user.User_Roles)
                .Where(user => user.IsActive && user.User_Roles.RoleName != "Admin")
                .Include(user => user.Project_Developers)
                .IncludeFilter(user => user.Bugs
                    .Where(bug => bug.Bug_Status.BugStatus != "Solved"))
                .ToList();
        }

        void IUserRepository.Delete(User user)
        {
            user.IsActive = false;
            Update(user);
        }

        public List<User> GetFreeEmployees()
        {
            return Context.Users
                .Where(user => user.Project_Developers.Count == 0 && user.IsActive && user.User_Roles.RoleName == "Employee")
                .ToList();
        }

        public User Authenticate(string email, string password)
        {
            return Context.Users
                .Where(u => u.IsActive && u.Email == email && u.Password == password)
                .FirstOrDefault();
        }

        public bool ChangePassword(int id, string oldPassword, string newPassword)
        {
            User user = Context.Users
                .Where(u => u.Id == id && u.Password == oldPassword)
                .FirstOrDefault();

            if (user != null)
            {
                user.Password = newPassword;
                Context.Entry(user).State = EntityState.Modified;
                Context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
