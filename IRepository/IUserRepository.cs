using API.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace API.IRepository
{
    public interface IUserRepository
    {
        public Task<User> login(string email, string password);
        public Task<User> register(User user);
        public bool userExists(User user);
        public IQueryable<User> get();
    }
}
