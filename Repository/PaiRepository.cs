using API.Context;
using API.IRepository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PaiRepository : IPaiRepository
    {

        private readonly AppDbContext _context;

        public PaiRepository(AppDbContext context) 
        { 
            this._context = context;
        }

        public IQueryable<Pai> getAll()
        {
            return this._context.Pai.Include(x => x.Filho).AsQueryable();
        }

        public async Task<Pai> get(int id) 
        {
            return await this._context.Pai.Include(x => x.Filho).FirstOrDefaultAsync(x => x.PaiId == id);
        }

        public async Task<Pai> post(Pai pai)
        {
            if (pai is null)
            {
                throw new NullReferenceException();
            }
            this._context.Pai.Add(pai);
            await this._context.SaveChangesAsync();

            return pai;
        }

        public async Task<Pai> put(int id , Pai pai)
        {
            if (id != pai.PaiId)
            {
                throw new AccessViolationException();
            }

            this._context.Entry(pai).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            return pai;
        }

        public async Task<Pai> delete(int id)
        {
            Pai pai = await this._context.Pai.FirstOrDefaultAsync(x => x.PaiId == id);
            if (pai is null)
            {
                throw new NullReferenceException();
            }
            this._context.Pai.Remove(pai);
            await this._context.SaveChangesAsync();
            return pai;
        }
    }
}
