using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext context;
        public DatingRepository(DataContext context)
        {
            this.context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
           return await this.context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id==id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this.context.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}