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
    public class PlotUnitController : ControllerBase
    {
        private readonly NovelistDbContext _context;
        private readonly DataFactory _dataFactory;

        public PlotUnitController(NovelistDbContext context, DataFactory dataFactory)
        {
            _context = context;
            _dataFactory = dataFactory;
        }

        // GET: api/PlotUnit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlotUnitDTO>>> GetPlotUnits()
        {
            var plotUnits = await _context.Set<PlotUnit>().ToListAsync();
            return plotUnits.Select(n => (PlotUnitDTO) _dataFactory.CreateDTO<PlotUnitDTO, PlotUnit>(n)).ToList();
        }

        // GET: api/PlotUnit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlotUnitDTO>> GetPlotUnit(int id)
        {
            var plotUnit = await _context.Set<PlotUnit>().FindAsync(id);

            if (plotUnit == null)
            {
                return NotFound();
            }

            return (PlotUnitDTO) _dataFactory.CreateDTO<PlotUnitDTO, PlotUnit>(plotUnit);
        }

        // POST: api/PlotUnit
        [HttpPost]
        public async Task<ActionResult<PlotUnitDTO>> PostPlotUnit(PlotUnitDTO plotUnitDTO)
        {
            var plotUnit = _dataFactory.CreateEntity<PlotUnit, PlotUnitDTO>(plotUnitDTO);
            plotUnit.NovelId = plotUnitDTO.NovelId;
            plotUnit.PlotUnitTypeId = plotUnitDTO.PlotUnitTypeId;
            _context.Set<PlotUnit>().Add(plotUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlotUnit", new { id = plotUnit.Id }, (PlotUnitDTO)_dataFactory.CreateDTO<PlotUnitDTO, PlotUnit>(plotUnit));
        }

        // PUT: api/PlotUnit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlotUnit(int id, PlotUnitDTO plotUnitDTO)
        {
            if (id != plotUnitDTO.Id)
            {
                return BadRequest();
            }

            var plotUnit = await _context.Set<PlotUnit>().FindAsync(id);
            if (plotUnit == null)
            {
                return NotFound();
            }

            plotUnit.Title = plotUnitDTO.Title;
            plotUnit.Description = plotUnitDTO.Description;
            plotUnit.Premise = plotUnitDTO.Premise;
            plotUnit.Location = plotUnitDTO.Location;
            plotUnit.PlotUnitTypeId = plotUnitDTO.PlotUnitTypeId;
            plotUnit.NovelId = plotUnitDTO.NovelId;

            _context.Entry(plotUnit).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<PlotUnit>().Any(e => e.Id == id))
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

        // DELETE: api/PlotUnit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlotUnit(int id)
        {
            var plotUnit = await _context.Set<PlotUnit>().FindAsync(id);
            if (plotUnit == null)
            {
                return NotFound();
            }

            _context.Set<PlotUnit>().Remove(plotUnit);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}