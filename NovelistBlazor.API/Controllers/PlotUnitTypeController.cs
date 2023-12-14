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
    public class PlotUnitTypeController : ControllerBase
    {
        private readonly NovelistDbContext _context;
        private readonly DataFactory _dataFactory;

        public PlotUnitTypeController(NovelistDbContext context, DataFactory dataFactory)
        {
            _context = context;
            _dataFactory = dataFactory;
        }

        // GET: api/PlotUnitType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlotUnitTypeDTO>>> GetPlotUnitTypes()
        {
            var plotUnitTypes = await _context.Set<PlotUnitType>().ToListAsync();
            return plotUnitTypes.Select(n => (PlotUnitTypeDTO)_dataFactory.CreateDTO<PlotUnitTypeDTO, PlotUnitType>(n)).ToList();
        }

        // GET: api/PlotUnitType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlotUnitTypeDTO>> GetPlotUnitType(int id)
        {
            var plotUnitType = await _context.Set<PlotUnitType>().FindAsync(id);

            if (plotUnitType == null)
            {
                return NotFound();
            }

            return (PlotUnitTypeDTO)_dataFactory.CreateDTO<PlotUnitTypeDTO, PlotUnitType>(plotUnitType);
        }

        // POST: api/PlotUnitType
        [HttpPost]
        public async Task<ActionResult<PlotUnitTypeDTO>> PostPlotUnitType(PlotUnitTypeDTO plotUnitTypeDTO)
        {
            var plotUnitType = _dataFactory.CreateEntity<PlotUnitType, PlotUnitTypeDTO>(plotUnitTypeDTO);
            _context.Set<PlotUnitType>().Add(plotUnitType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlotUnitType", new { id = plotUnitType.Id }, (PlotUnitTypeDTO)_dataFactory.CreateDTO<PlotUnitTypeDTO, PlotUnitType>(plotUnitType));
        }

        // PUT: api/PlotUnitType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlotUnitType(int id, PlotUnitTypeDTO plotUnitTypeDTO)
        {
            if (id != plotUnitTypeDTO.Id)
            {
                return BadRequest();
            }

            var plotUnitType = await _context.Set<PlotUnitType>().FindAsync(id);
            if (plotUnitType == null)
            {
                return NotFound();
            }

            plotUnitType.Name = plotUnitTypeDTO.Name;

            _context.Entry(plotUnitType).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<PlotUnitType>().Any(e => e.Id == id))
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

        // DELETE: api/PlotUnitType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlotUnitType(int id)
        {
            var plotUnitType = await _context.Set<PlotUnitType>().FindAsync(id);
            if (plotUnitType == null)
            {
                return NotFound();
            }

            _context.Set<PlotUnitType>().Remove(plotUnitType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}