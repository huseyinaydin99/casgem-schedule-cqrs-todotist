using Casgem_Schedule.CQRS.TodoList.DAL.Context;
using Casgem_Schedule.CQRS.TodoList.Entities;

namespace Casgem_Schedule.CQRS.TodoList.CQRS.Handlers
{
    public class GetCalendarQueryHandler
    {
        private readonly Context _context;

        public GetCalendarQueryHandler(Context context)
        {
            _context = context;
        }

        public List<Calendar> Handle(DateTime start, DateTime end)
        {
            var values = _context.Calendars
                   .Where(e => !((e.End <= start) || (e.Start >= end)))
                   .ToList();
            return values;
        }
    }
}
