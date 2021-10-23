public class Event
{
    public Event(string message)
    {
        Message = message;
        Trace = Guid.NewGuid().ToString();
        Date = DateTimeHelper.GetSystemDate();
    }

    [Key]
    public string Trace { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public string Operation { get; set; }
}
