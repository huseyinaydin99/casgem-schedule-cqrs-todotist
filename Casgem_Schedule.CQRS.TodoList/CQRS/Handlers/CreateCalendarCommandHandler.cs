using Casgem_Schedule.CQRS.TodoList.DAL.Context;
using Casgem_Schedule.CQRS.TodoList.Entities;

namespace Casgem_Schedule.CQRS.TodoList.CQRS.Handlers
{
    public class CreateCalendarCommandHandler
    {
        private readonly Context _context;

        public CreateCalendarCommandHandler(Context context)
        {
            _context = context;
        }

        public void Handle(Calendar calendar)
        {
            _context.Calendars.Add(new Calendar
            {
                Color = calendar.Color,
                End = calendar.End,
                Start = calendar.Start,
                Text = calendar.Text
            });
            _context.SaveChanges();
        }
    }
}
