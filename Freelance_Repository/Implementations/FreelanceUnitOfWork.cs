using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance_Data.Context;

namespace Freelance_Repository.Implementations
{
    public class FreelanceUnitOfWork : IDisposable
    {
        public DbContext Context { get; private set; }
        private IDbContextTransaction Transaction { get; set; }

        public FreelanceUnitOfWork()
        {
            Context = new FreelanceDbContext();
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

