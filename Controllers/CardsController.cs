using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCulturalBackEnd.AppDbContext;
using QRCulturalBackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QRCulturalBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards()
        {
            var cards = await _context.Cards.ToListAsync();

            var cardDtos = cards.Select(card => new CardDto
            {
                id = card.id,
                descricao = card.descricao,
                titulo = card.titulo,
                imagem = Convert.ToBase64String(card.imagem),
                monumentoId = card.monumentoId,
                monumento = card.monumento
            }).ToList();

            return Ok(cardDtos);
        }

    [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard([FromForm] CardInputModel card)
        {
            var monumento = await _context.Monumentos.FindAsync(card.monumentoId);
            if (monumento == null)
            {
                return BadRequest("Monumento não encontrado.");
            }

            var _card = new Card
            {
                descricao = card.descricao,
                titulo = card.titulo,
                imagem = await ConvertFileToByteArray(card.imagem),
                monumentoId = card.monumentoId,
                monumento = monumento
            };

            _context.Cards.Add(_card);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = _card.id }, _card);
        }

        [HttpPut]
        public async Task<IActionResult> PutCard([FromForm] CardInputModel card)
        {

            var _card = await _context.Cards.FindAsync(card.id);

            if (_card == null)
            {
                return NotFound("Card não encontrado.");
            }

            var monumento = await _context.Monumentos.FindAsync(card.monumentoId);
            if (monumento == null)
            {
                return BadRequest("Monumento não encontrado.");
            }

            _card.descricao = card.descricao;
            _card.titulo = card.titulo;

            // Se uma nova imagem foi fornecida, converte para byte[]
            if (card.imagem != null)
            {
                _card.imagem = await ConvertFileToByteArray(card.imagem);
            }

            _card.monumentoId = card.monumentoId;
            _card.monumento = monumento;

            try
            {
                _context.Entry(_card).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(card.id))
                {
                    return NotFound("Card não encontrado após tentativa de atualização.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.id == id);
        }


        private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
