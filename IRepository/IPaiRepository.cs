using API.Models;

namespace API.IRepository
{
    public interface IPaiRepository
    {
        public IQueryable<Pai> getAll();
        public Task<Pai> get(int id);
        public Task<Pai> post(Pai pai);
        public Task<Pai> put(int id, Pai pai);
        public Task<Pai> delete(int id);
    }
}
