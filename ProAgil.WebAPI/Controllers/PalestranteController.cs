using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        public readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllEPalestrantesAsync(true);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou "+ ex +"\n ");
            }
        }
        [HttpGet("{PalestranteID}")]
        public async Task<IActionResult> Get(int PalestranteID)
        {
            try
            {
                var results = await _repo.GetEventoAsyncById(PalestranteID,true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
        }
        [HttpGet("getByNome/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var results = await _repo.GetAllEPalestrantesAsyncByNome(nome,true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou "+ ex);
            }
        }
        //put
        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteID, Palestrante model)
        {
            try
            {
                var palestrante = await _repo.GetEPalestrantesAsyncById(PalestranteID, true);
                
                if(palestrante == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou "+ ex);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteID)
        {
            try
            {
                var palestrante = await _repo.GetEPalestrantesAsyncById(PalestranteID, false);
                _repo.Update(palestrante);
                if(palestrante == null) return NotFound();

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou "+ ex);
            }
        }
    }
}