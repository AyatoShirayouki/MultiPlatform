using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admins_Data.Context;

namespace Admins_Repository.Implementations
{
    public class AdminsUnitOfWork : IDisposable
    {
        public DbContext Context { get; private set; }
        private IDbContextTransaction Transaction { get; set; }

        public AdminsUnitOfWork()
        {
            Context = new AdminsDbContext();
        }

        public void BeginTransaction()
        {
            Transaction = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Dispose()
        {
            Context.Dispose();
            Transaction.Dispose();
        }
    }
}

