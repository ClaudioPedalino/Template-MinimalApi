public class EventStoreContext : DbContext
{
    private readonly DatabaseConfig _databaseConfig;

    public EventStoreContext() { }

    public EventStoreContext(DbContextOptions<EventStoreContext> options,
                             IOptionsMonitor<DatabaseConfig> databaseConfig) : base(options)
    {
        _databaseConfig = databaseConfig.CurrentValue;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        //optionsBuilder.UseNpgsql(_databaseConfig.PostgreSqlDb);
        optionsBuilder.UseNpgsql("Server=localhost;Database=EventStoreDb;Port=5432;User Id=postgres;Password=Temporal1");

        base.OnConfiguring(optionsBuilder);
    }

    public void SaveEvent(string message)
    {
        try
        {
            var newEvent = new Event(message) { Operation = "Operation Id" };
            Events.Add(newEvent);
            base.SaveChanges();
            ConsolePrinterHelper.PrintWithColor($" >> Event {message} succesfully sended", ConsoleColor.Yellow);
        }
        catch { }
    }


    public DbSet<Event> Events { get; set; }
}
