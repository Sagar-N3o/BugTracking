﻿using BugTracking.Business.Contracts.Repositories.General;
using BugTracking.Database.Domain;
using System.Collections.Generic;

namespace BugTracking.Business.Contracts.Repositories.ProjectDevelopers
{
    public interface IProjectDevelopersRepository : IBaseRepository<Project_Developers>
    {
        List<Project_Developers> GetByProjectId(int id);

        Project_Developers GetByUserId(int id);

        void RemoveDeveloper(int projId, int devId);

        void RemoveByProjectId(int id);
    }
}
