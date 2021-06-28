using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        //Evento
        public async Task<Evento[]> GetAllEventoAsync(bool incrudePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

                if(incrudePalestrantes)
                {
                    query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrantes);
                }
                query = query.AsNoTracking().OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool incrudePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if(incrudePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Palestrantes);
            }
            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento)
            .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));
                query = query.Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int EventoId, bool incrudePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if(incrudePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Palestrantes);
            }
            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento)
            .Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }
        //Palestrante
        public async Task<Palestrante[]> GetAllEPalestrantesAsync(bool incrudeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if(incrudeEventos)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)
                .ThenInclude(e => e.Eventos);
            }
            query = query.AsNoTracking().OrderBy(c => c.Nome);

            return await query.ToArrayAsync();
        }
        public async Task<Palestrante> GetEPalestrantesAsyncById(int PalestranteID, bool incrudeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if(incrudeEventos)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)
                .ThenInclude(e => e.Eventos);
            }
            query = query.AsNoTracking().OrderBy(c => c.Nome)
            .Where(c => c.Id ==PalestranteID);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllEPalestrantesAsyncByNome(string nome, bool incrudeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if(incrudeEventos)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Eventos);
            }
            query = query.AsNoTracking().Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}