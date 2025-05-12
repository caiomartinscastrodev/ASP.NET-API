using API.Context;
using API.IRepository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) 
        {
            this._context = context;
        }

        public async Task<User> login(string email , string password)
        {
            return await this._context.User.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
        
        public async Task<User> register(User user)
        {
            this._context.User.Add(user);
            this._context.SaveChanges();

            return user;
        }

        public bool userExists(User user)
        {
            return this._context.User.Any(x => x.Email == user.Email);
        }

        public IQueryable<User> get()
        {
            return this._context.User.AsQueryable();
        }
    }
}
