using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    // Create posts.json if it does not exist
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        // Read the JSON file
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<Post>
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        // Find the highest existing ID
        int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 0;

        // Give the new post the next ID
        post.Id = maxId + 1;

        // Add the new post
        posts.Add(post);

        // Convert the updated list back to JSON
        postsAsJson = JsonSerializer.Serialize(posts);

        // Save it back into posts.json
        await File.WriteAllTextAsync(filePath, postsAsJson);

        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        // Read the JSON file
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<Post>
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        // Find the post with the same ID
        Post? existingPost =
            posts.FirstOrDefault(p => p.Id == post.Id);

        if (existingPost == null)
        {
            throw new Exception($"Post with ID {post.Id} was not found.");
        }

        // Update the existing post
        existingPost.Title = post.Title;
        existingPost.Body = post.Body;
        existingPost.UserId = post.UserId;

        // Convert back to JSON
        postsAsJson = JsonSerializer.Serialize(posts);

        // Save the updated JSON
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        // Read the JSON file
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<Post>
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        // Find the post with the matching ID
        Post? postToDelete =
            posts.FirstOrDefault(p => p.Id == id);

        if (postToDelete == null)
        {
            throw new Exception($"Post with ID {id} was not found.");
        }

        // Remove the post
        posts.Remove(postToDelete);

        // Convert the updated list back to JSON
        postsAsJson = JsonSerializer.Serialize(posts);

        // Save the updated JSON
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        // Read the JSON file
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<Post>
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        // Find the post with the matching ID
        Post? post =
            posts.FirstOrDefault(p => p.Id == id);

        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found.");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        // Read the JSON file
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;

        // Convert JSON into a List<Post>
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        // Return the list as IQueryable
        return posts.AsQueryable();
    }
}