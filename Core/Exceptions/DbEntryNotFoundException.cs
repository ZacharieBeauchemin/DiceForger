namespace Core.Exceptions;
public class DbEntryNotFoundException: Exception {
    public DbEntryNotFoundException() {}

    public DbEntryNotFoundException(string message): base(message) {}

    public DbEntryNotFoundException(string message, Exception inner): base(message, inner) {}
}
