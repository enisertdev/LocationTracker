namespace LocationTracker.Application.Interfaces.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashString);
    }
}
