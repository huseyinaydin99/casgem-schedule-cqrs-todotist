namespace Casgem_Schedule.CQRS.TodoList.CQRS.Results
{
    public class GetCalendarQueryResult
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public string? Color { get; set; }
    }
}
