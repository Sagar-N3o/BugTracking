﻿using BugTracking.Business.Contracts.Repositories.Users;
using BugTracking.Business.Dal.Repositories.General;
using BugTracking.Database.Domain;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace BugTracking.Business.Dal.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BugTrackingEntities context) : base(context)
        {
            Context = context;
        }

        public User GetById(int id)
        {
            return Context.Users
                .Where(user => user.Id == id)
                .Include(user => user.User_Roles)
                .FirstOrDefault();
        }

        List<User> IUserRepository.GetAll()
        {
            return Context.Users
                .Include(user => user.User_Roles)
                .Include(user => user.Project_Developers)
                .Include(user => user.Bugs).ToList();
        }
    }
}