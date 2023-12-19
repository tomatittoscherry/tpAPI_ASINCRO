using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using tpAPI_ASINCRO.Domain.Request;
using tpAPI_ASINCRO.Domain.Response;
using tpAPI_ASINCRO.Repository;
using System.Threading.Tasks;

namespace tpAPI_ASINCRO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActoresController : Controller
    {
        /*private readonly IDBM_5Context _context; //inyección de dependencia
        public ActoresController(IDBM_5Context context)
        {
            _context = context;
        }*/

        private readonly IDBM_5Context _context;

        public ActoresController(IDBM_5Context context)
        {
            _context = context;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetActores([FromQuery] GetActoresRequest request) //ver cuales te queres saltear y cuales queres tomar
        {
            //Paginado => no hacer un select all
            int Skip = request.skip;
            int Take = request.take;

            //var result = _context.Directores.ToList(); //select * from Directores => select all, no se suele hacer un ToList para no traer toda la info de una
            //return Ok(new { Director = "algún nombre" });
            var result = await _context.Actores.Skip(Skip).Take(Take).ToListAsync();
            int count = await _context.Actores.CountAsync();

            var response = new GetActoresResponse()
            {
                Actores = result,
                Total = count,
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActorById([FromRoute] int id)
        {
            var result = _context.Actores.FirstOrDefault(f => f.IdActor == id); //es similar al Where

            if (result == null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new { Error = $"no se encontró el ID {id}" });
            }
        }

        [HttpGet("Actuaciones")]
        public IActionResult GetAcuacionesByActores([FromQuery] int idActor)
        {
            var result = _context.Actores.Where(w => w.IdActor == idActor).Include(i => i.Actuaciones).ToList();

            return Ok(result);
        }

}
}
