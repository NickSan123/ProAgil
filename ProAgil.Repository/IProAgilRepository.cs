using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();

         //Eventos
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool incrudePalestrantes);
         Task<Evento[]> GetAllEventoAsync(bool incrudePalestrantes);
         Task<Evento> GetEventoAsyncById(int EventoId, bool incrudePalestrantes);

         //Palestrantes
         Task<Palestrante[]> GetAllEPalestrantesAsyncByNome(string nome, bool incrudeEventos);
         Task<Palestrante[]> GetAllEPalestrantesAsync(bool incrudeEventos);
         Task<Palestrante> GetEPalestrantesAsyncById(int PalestranteID, bool incrudeEventos);
    }
}