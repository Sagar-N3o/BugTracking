﻿using BugTracking.Business.Contracts.Repositories.Projects;
using BugTracking.Business.Dal.Repositories.General;
using BugTracking.Database.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Z.EntityFramework.Plus;

namespace BugTracking.Business.Dal.Repositories.Projects
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(BugTrackingEntities context) : base(context)
        {
            Context = context;
        }

        public int ActiveProjectCount()
        {
            return Context.Projects
                .Where(project => project.IsActive)
                .Count();
        }

        public int ProductCount()
        {
            return Context.Projects.Count();
        }

        public Project GetById(int id)
        {
            return Context.Projects
                .Where(project => project.Id == id)
                .Include(project => project.Project_Developers.Select(dev => dev.User))
                .Include(project => project.Project_Status)
                .Include(project => project.Project_Technologies)
                .IncludeFilter(Project => Project.Bugs
                    .Where(bug => bug.Bug_Status.BugStatus != "Solved"))
                .FirstOrDefault();
        }

        List<Project> IProjectRepository.GetAll()
        {
            return Context.Projects.Where(project => project.IsActive)
                .Include(project => project.Project_Developers)
                .Include(project => project.Project_Status)
                .Include(project => project.Project_Technologies)
                .IncludeFilter(Project => Project.Bugs
                    .Where(bug => bug.Bug_Status.BugStatus != "Solved"))
                .ToList();
        }

        void IProjectRepository.Delete(Project project)
        {
            project.IsActive = false;
            Update(project);
        }

        public List<Project> GetFinished()
        {
            return Context.Projects
                .Where(project => project.IsActive
                    && (project.Project_Status.ProjectStatus == "Finished" || project.Project_Status.ProjectStatus == "Under Testing"))
                .Include(project => project.Project_Developers)
                .Include(project => project.Project_Status)
                .Include(project => project.Project_Technologies)
                .IncludeFilter(project => project.Bugs
                    .Where(bug => bug.Bug_Status.BugStatus != "Solved"))
                .ToList();
        }
    }
}
