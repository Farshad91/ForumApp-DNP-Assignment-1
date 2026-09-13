using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    // Reads post information and creates a new post
    public async Task CreatePostAsync()
    {
        Console.Write("Enter title: ");
        string? title = Console.ReadLine();

        Console.Write("Enter body: ");
        string? body = Console.ReadLine();

        Console.Write("Enter user ID: ");
        string? userIdInput = Console.ReadLine();

        // Check that the user ID is a number
        if (!int.TryParse(userIdInput, out int userId))
        {
            Console.WriteLine("User ID must be a number.");
            return;
        }

        Post post = new Post
        {
            Title = title ?? string.Empty,
            Body = body ?? string.Empty,
            UserId = userId
        };

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine($"Post created with ID: {createdPost.Id}");
    }
    
    
    
}