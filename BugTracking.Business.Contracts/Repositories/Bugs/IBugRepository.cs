﻿using BugTracking.Business.Contracts.Repositories.General;
using BugTracking.Database.Domain;
using System.Collections.Generic;

namespace BugTracking.Business.Contracts.Repositories.Bugs
{
    public interface IBugRepository : IBaseRepository<Bug>
    {
        new List<Bug> GetAll();

        Bug GetById(int id);

        int OpenBugCount();
    }
}
