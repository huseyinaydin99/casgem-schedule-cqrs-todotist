using Casgem_Schedule.CQRS.TodoList.Entities;
using Microsoft.EntityFrameworkCore;

namespace Casgem_Schedule.CQRS.TodoList.DAL.Context
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-13123BI; Initial Catalog = CasgemTodoListDb; Integrated Security = true;");
        }

        public DbSet<Calendar> Calendars { get; set; }
    }
}