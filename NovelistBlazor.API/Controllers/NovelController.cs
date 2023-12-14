using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelistBlazor.Common.DTO;
using NovelistBlazor.Common.Model;
using System.Linq;
using NovelistBlazor.API.Data;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NovelController : ControllerBase
    {
        private readonly NovelistDbContext _context;
        private readonly DataFactory _dataFactory;

        public NovelController(NovelistDbContext context, DataFactory dataFactory)
        {
            _context = context;
            _dataFactory = dataFactory;
        }

        // GET: api/Novel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NovelDTO>>> GetNovels()
        {
            var novels = await _context.Set<Novel>().ToListAsync();
            return novels.Select(n => (NovelDTO) _dataFactory.CreateDTO<NovelDTO, Novel>(n)).ToList();
        }

        // GET: api/Novel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NovelDTO>> GetNovel(int id)
        {
            var novel = await _context.Set<Novel>().FindAsync(id);

            if (novel == null)
            {
                return NotFound();
            }

            return (NovelDTO) _dataFactory.CreateDTO<NovelDTO, Novel>(novel);
        }

        // POST: api/Novel
        [HttpPost]
        public async Task<ActionResult<NovelDTO>> PostNovel(NovelDTO novelDTO)
        {
            var novel = _dataFactory.CreateEntity<Novel, NovelDTO>(novelDTO);
            _context.Set<Novel>().Add(novel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNovel", new { id = novel.Id }, (NovelDTO)  _dataFactory.CreateDTO<NovelDTO, Novel>(novel));
        }

        // PUT: api/Novel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNovel(int id, NovelDTO novelDTO)
        {
            // For unknown reason the id of the dto is 


            if (id != novelDTO.Id)
            {
                return BadRequest();
            }

            var novel = await _context.Set<Novel>().FindAsync(id);
            if (novel == null)
            {
                return NotFound();
            }

            novel.Name = novelDTO.Name;
            novel.Description = novelDTO.Description;
            novel.Abstract = novelDTO.Abstract;

            _context.Entry(novel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<Novel>().Any(e => e.Id == id))
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

        // DELETE: api/Novel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNovel(int id)
        {
            var novel = await _context.Set<Novel>().FindAsync(id);
            if (novel == null)
            {
                return NotFound();
            }

            _context.Set<Novel>().Remove(novel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
