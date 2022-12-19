using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Context;

namespace Users_Repository.Implementations
{
    public class UsersUnitOfWork : IDisposable
    {
        public DbContext Context { get; private set; }
        private IDbContextTransaction Transaction { get; set; }

        public UsersUnitOfWork()
        {
            Context = new UsersDbContext();
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
