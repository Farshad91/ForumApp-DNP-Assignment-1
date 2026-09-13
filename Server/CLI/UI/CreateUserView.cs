//This class handles how to create a user

using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    // Reads user information and creates a new user
    public async Task CreateUserAsync()
    {
        Console.Write("Enter username: ");
        string? username = Console.ReadLine();

        Console.Write("Enter password: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username and password cannot be empty.");
            return;
        }

        User user = new User
        {
            Username = username,
            Password = password
        };

        User createdUser = await userRepository.AddAsync(user);

        Console.WriteLine($"User created with ID: {createdUser.Id}");
    }
    
}