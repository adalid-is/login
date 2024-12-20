using BCrypt.Net; // Asegúrate de instalar el paquete NuGet BCrypt.Net-Next
public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        // Generar un hash seguro usando bcrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        // Verificar si la contraseña proporcionada coincide con el hash almacenado
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}