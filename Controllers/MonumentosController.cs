using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCulturalBackEnd.AppDbContext;
using QRCulturalBackEnd.Models;

namespace QRCulturalBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonumentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MonumentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monumento>>> GetMonumentos()
        {
            var monumentos = await _context.Monumentos.ToListAsync();

            var monumentosDto = monumentos.Select(monumento => new MonumentoDTO
            {
                id = monumento.id,
                contextoHistorico = monumento.contextoHistorico,
                endereco = monumento.endereco,
                horarioFuncionamento = monumento.horarioFuncionamento,
                entrada = monumento.entrada,
                carrosel1 = Convert.ToBase64String(monumento.carrosel1),
                carrosel2 = Convert.ToBase64String(monumento.carrosel2),
                carrosel3 = Convert.ToBase64String(monumento.carrosel3),
                imagemPrincipal = Convert.ToBase64String(monumento.imagemPrincipal),
                card = monumento.card
            }).ToList();

            return Ok(monumentosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Monumento>> GetMonumento(int id)
        {
            var monumento = await _context.Monumentos.Include(m => m.card).FirstOrDefaultAsync(m => m.id == id);

            if (monumento == null)
            {
                return NotFound();
            }

            return monumento;
        }

        [HttpPost]
        public async Task<ActionResult<Monumento>> PostMonumento([FromForm] MonumentoInputModel input)
        {
            var monumento = new Monumento
            {
                contextoHistorico = input.history,
                endereco = input.address,
                horarioFuncionamento = input.openingHours,
                entrada = input.entryFee,
                // Converte os IFormFile (imagens) para byte[]
                carrosel1 = await ConvertFileToByteArray(input.carousel1),
                carrosel2 = await ConvertFileToByteArray(input.carousel2),
                carrosel3 = await ConvertFileToByteArray(input.carousel3),
                imagemPrincipal = await ConvertFileToByteArray(input.image)
            };

            // Adicionar o monumento ao contexto do banco de dados
            _context.Monumentos.Add(monumento);
            await _context.SaveChangesAsync();

            // Retorna um ActionResult com um status 201 Created e o monumento criado
            return CreatedAtAction(nameof(GetMonumento), new { id = monumento.id }, monumento);
        }

        private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutMonumento([FromForm] MonumentoInputModel input)
        {
            var monumento = await _context.Monumentos.FindAsync(input.id);

            if (monumento == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades do monumento com base nos dados recebidos do input model
            monumento.contextoHistorico = input.history;
            monumento.endereco = input.address;
            monumento.horarioFuncionamento = input.openingHours;
            monumento.entrada = input.entryFee;

            // Se novas imagens foram fornecidas, converte e atualiza as propriedades correspondentes
            if (input.carousel1 != null)
            {
                monumento.carrosel1 = await ConvertFileToByteArray(input.carousel1);
            }

            if (input.carousel2 != null)
            {
                monumento.carrosel2 = await ConvertFileToByteArray(input.carousel2);
            }

            if (input.carousel3 != null)
            {
                monumento.carrosel3 = await ConvertFileToByteArray(input.carousel3);
            }

            if (input.image != null)
            {
                monumento.imagemPrincipal = await ConvertFileToByteArray(input.image);
            }

            try
            {
                _context.Entry(monumento).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonumentoExists(input.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonumento(int id)
        {
            var monumento = await _context.Monumentos.FindAsync(id);
            if (monumento == null)
            {
                return NotFound();
            }

            _context.Monumentos.Remove(monumento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonumentoExists(int id)
        {
            return _context.Monumentos.Any(e => e.id == id);
        }
    }
}
