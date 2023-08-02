using Casgem_Schedule.CQRS.TodoList.CQRS.Handlers;
using Casgem_Schedule.CQRS.TodoList.DAL.Context;
using Casgem_Schedule.CQRS.TodoList.Entities;
using Casgem_Schedule.CQRS.TodoList.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casgem_Schedule.CQRS.TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarApiController : ControllerBase
    {
        private readonly Context _context;
        private readonly CreateCalendarCommandHandler _createCalendarCommandHandler;
        private readonly GetCalendarQueryHandler _getCalendarQueryHandler;

        public CalendarApiController(Context context, CreateCalendarCommandHandler createCalendarCommandHandler, GetCalendarQueryHandler getCalendarQueryHandler)
        {
            _context = context;
            _createCalendarCommandHandler = createCalendarCommandHandler;
            _getCalendarQueryHandler = getCalendarQueryHandler;
        }


        // GET: api/CalendarEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetEvents([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return _getCalendarQueryHandler.Handle(start, end);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendarEvent(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            return calendar;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarEvent(int id, Calendar calendar)
        {
            if (id != calendar.Id)
            {
                return BadRequest();
            }

            _context.Entry(calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarEventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendarEvent([FromBody] Calendar calendar)
        {
            _createCalendarCommandHandler.Handle(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendarEvent", new { id = calendar.Id }, calendar);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarEvent(int id)
        {
            var calendarEvent = await _context.Calendars.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            _context.Calendars.Remove(calendarEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarEventExists(int id)
        {
            return _context.Calendars.Any(e => e.Id == id);
        }

        // PUT: api/Events/5/move
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveEvent([FromRoute] int id, [FromBody] EventMoveParams param)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Calendars.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            @event.Start = param.Start;
            @event.End = param.End;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarEventExists(id))
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

        // PUT: api/Events/5/color
        [HttpPut("{id}/color")]
        public async Task<IActionResult> SetEventColor([FromRoute] int id, [FromBody] EventColorParams param)
        {
            if(id == 0)
            id++;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Calendars.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            @event.Color = param.Color;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarEventExists(id))
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
    }
}
