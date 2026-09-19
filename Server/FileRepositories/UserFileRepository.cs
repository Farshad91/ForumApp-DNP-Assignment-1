using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    // Create users.json if it does not exist
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        // Read the JSON file
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<User>
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        // Find the highest existing ID
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;

        // Give the new user the next ID
        user.Id = maxId + 1;

        // Add the new user to the list
        users.Add(user);

        // Convert the updated list back to JSON
        usersAsJson = JsonSerializer.Serialize(users);

        // Save the JSON back into the file
        await File.WriteAllTextAsync(filePath, usersAsJson);

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        // Read the JSON file
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<User>
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        // Find the existing user with the same ID
        User? existingUser =
            users.FirstOrDefault(u => u.Id == user.Id);

        if (existingUser == null)
        {
            throw new Exception($"User with ID {user.Id} was not found.");
        }

        // Update the existing user's information
        existingUser.Username = user.Username;
        existingUser.Password = user.Password;

        // Convert the updated list back to JSON
        usersAsJson = JsonSerializer.Serialize(users);

        // Save the JSON back into the file
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        // Read the JSON file
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<User>
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        // Find the user with the matching ID
        User? userToDelete =
            users.FirstOrDefault(u => u.Id == id);

        if (userToDelete == null)
        {
            throw new Exception($"User with ID {id} was not found.");
        }

        // Remove the user
        users.Remove(userToDelete);

        // Convert the updated list back to JSON
        usersAsJson = JsonSerializer.Serialize(users);

        // Save the JSON back into the file
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        // Read the JSON file
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<User>
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        // Find the user with the matching ID
        User? user =
            users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            throw new Exception($"User with ID {id} was not found.");
        }

        return user;
    }

    public IQueryable<User> GetMany()
    {
        // Read the JSON file
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;

        // Convert JSON into a List<User>
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        // Return the list as IQueryable
        return users.AsQueryable();
    }
}