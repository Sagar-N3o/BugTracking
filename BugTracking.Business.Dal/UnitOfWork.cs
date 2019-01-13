﻿using System;
using BugTracking.Database.Domain;
using BugTracking.Business.Contracts.Repositories.Status;
using BugTracking.Business.Dal.Repositories.Status;
using BugTracking.Business.Contracts.Repositories.Role;
using BugTracking.Business.Dal.Repositories.Role;

namespace BugTracking.Business.Dal
{
    public class UnitOfWork : IDisposable
    {
        private readonly BugTrackingEntities context;

        #region Constructor
        public UnitOfWork()
        {
            context = new BugTrackingEntities();

            StatusRepository = new StatusRepository(context);
            RoleRepository = new RoleRepository(context);
        }
        #endregion

        #region Properties
        public IStatusRepository StatusRepository { get; }
        public IRoleRepository RoleRepository { get; }
        #endregion

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
