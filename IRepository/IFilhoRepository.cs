using API.Models;

namespace API.IRepository
{
    public interface IFilhoRepository
    {
        public IQueryable<Filho> getAll();
        public Task<Filho> get(int id);
        public Task<Filho> post(Filho filho);
        public Task<Filho> put(int id , Filho filho);
        public Task<Filho> delete(int id);
    }
}
