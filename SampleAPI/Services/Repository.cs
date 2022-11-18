using Microsoft.EntityFrameworkCore;
using SampleAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public class Repository : IRepository
    {

        private AppDbContext db;

        public Repository()
        {
            db = new AppDbContext();
        }

        public async Task CommitAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task CreateAsync<T>(T model) where T : class
        {
            await db.Set<T>().AddAsync(model);
            await CommitAsync();
        }


        public async Task UpdateAsync<T>(T model) where T : class
        {
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await CommitAsync();
        }

        public async Task DeleteAsync<T>(string id) where T : class
        {
            var model = await GetByIdAsync<T>(id);
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await CommitAsync();
        }

        //public async Task<IQueryable<T>> GetAllAsync<T>() where T : class
        //{
        //    return db.Set<T>().AsQueryable<T>();
        //}

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await db.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync<T>(string id) where T : class
        {
            return await db.Set<T>().FindAsync(id);
        }

    }
}
