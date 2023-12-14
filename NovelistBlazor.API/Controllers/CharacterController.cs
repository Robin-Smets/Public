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
    public class CharacterController : ControllerBase
    {
        private readonly NovelistDbContext _context;
        private readonly DataFactory _dataFactory;

        public CharacterController(NovelistDbContext context, DataFactory dataFactory)
        {
            _context = context;
            _dataFactory = dataFactory;
        }

        // GET: api/Character
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            var characters = await _context.Set<Character>().ToListAsync();
            return characters.Select(n => (CharacterDTO) _dataFactory.CreateDTO<CharacterDTO, Character>(n)).ToList();
        }

        // GET: api/Character/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            var character = await _context.Set<Character>().FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return (CharacterDTO) _dataFactory.CreateDTO<CharacterDTO, Character>(character);
        }

        // POST: api/Character
        [HttpPost]
        public async Task<ActionResult<CharacterDTO>> PostCharacter(CharacterDTO characterDTO)
        {
            var character = _dataFactory.CreateEntity<Character, CharacterDTO>(characterDTO);
            _context.Set<Character>().Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.Id }, (CharacterDTO) _dataFactory.CreateDTO<CharacterDTO, Character>(character));
        }

        // PUT: api/Character/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterDTO characterDTO)
        {
            if (id != characterDTO.Id)
            {
                return BadRequest();
            }

            var character = await _context.Set<Character>().FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            character.Name = characterDTO.Name;
            character.Age = characterDTO.Age;
            character.Occupation = characterDTO.Occupation;
            character.RoleInStory = characterDTO.RoleInStory;
            character.PhysicalDescription = characterDTO.PhysicalDescription;
            character.PersonalityTraits = characterDTO.PersonalityTraits;
            character.Background = characterDTO.Background;
            character.GoalsAndMotivations = characterDTO.GoalsAndMotivations;
            character.CharacterArc = characterDTO.CharacterArc;
            character.NovelId = characterDTO.NovelId;

            _context.Entry(character).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<Character>().Any(e => e.Id == id))
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

        // DELETE: api/Character/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Set<Character>().FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Set<Character>().Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}