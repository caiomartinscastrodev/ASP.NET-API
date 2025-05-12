using API.Context;
using API.IRepository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class FilhoRepository : IFilhoRepository
    {
        private readonly AppDbContext _context;

        public FilhoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Filho> getAll()
        {
            return this._context.Filho.Include(x => x.Pai).AsQueryable();
        }

        public async Task<Filho> get(int id)
        {
            return await this._context.Filho.Include(x => x.Pai).FirstOrDefaultAsync(x => x.FilhoId == id);
        }

        public async Task<Filho> post(Filho Filho)
        {
            if (Filho is null)
            {
                throw new NullReferenceException();
            }
            this._context.Filho.Add(Filho);
            await this._context.SaveChangesAsync();

            return Filho;
        }

        public async Task<Filho> put(int id, Filho Filho)
        {
            if (id != Filho.FilhoId)
            {
                throw new AccessViolationException();
            }

            this._context.Entry(Filho).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            return Filho;
        }

        public async Task<Filho> delete(int id)
        {
            Filho Filho = await this._context.Filho.FirstOrDefaultAsync(x => x.FilhoId == id);
            if (Filho is null)
            {
                throw new NullReferenceException();
            }
            this._context.Filho.Remove(Filho);
            await this._context.SaveChangesAsync();
            return Filho;
        }
    }
}
