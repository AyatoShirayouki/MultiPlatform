using Admins_Data.Context;
using Base.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admins_Repository.Implementations
{
    public class AdminsBaseRepository<T> where T : BaseEntity
    {
        protected DbSet<T> Items { get; set; }
        protected DbContext Context { get; set; }

        protected virtual IQueryable<T> CascadeInclude(IQueryable<T> query)
        {
            return query;
        }

        public AdminsBaseRepository()
        {
            Context = new AdminDbContext();
            Items = Context.Set<T>();
        }

        public AdminsBaseRepository(AdminsUnitOfWork uow)
        {
            Context = new AdminDbContext();
            Items = Context.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await Task.Run(() => Items.Where(u => u.Id == id).FirstOrDefault());
        }

        public async Task<List<TResult>> GetReferences<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return await Task.Run(() => Items
               .Where(filter)
               .Select(selector)
               .ToList());
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = await Task.Run(() => Items);
            if (filter != null)
            {
                query = await Task.Run(() => query.Where(filter));
            }

            return query.ToList();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> where)
        {
            return await Task.Run(() => Items.Where(where).FirstOrDefault());
        }

        public async Task Save(T item)
        {
            if (item.Id > 0)
            {
                await Task.Run(() => Items.Update(item));
            }
            else
            {
                await Task.Run(() => Items.Add(item));
            }

            Context.SaveChanges();
        }

        public async Task Delete(T item)
        {
            await Task.Run(() => Items.Remove(item));
            Context.SaveChanges();
        }
    }
}
