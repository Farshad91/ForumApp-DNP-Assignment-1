using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class AddCommentView
{
    private readonly ICommentRepository commentRepository;

    public AddCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    
    // Reads comment information and creates a new comment
    public async Task AddCommentAsync()
    {
        Console.Write("Enter comment: ");
        string? body = Console.ReadLine();

        Console.Write("Enter user ID: ");
        string? userIdInput = Console.ReadLine();

        Console.Write("Enter post ID: ");
        string? postIdInput = Console.ReadLine();

        if (!int.TryParse(userIdInput, out int userId))
        {
            Console.WriteLine("User ID must be a number.");
            return;
        }

        if (!int.TryParse(postIdInput, out int postId))
        {
            Console.WriteLine("Post ID must be a number.");
            return;
        }

        Comment comment = new Comment
        {
            Body = body ?? string.Empty,
            UserId = userId,
            PostId = postId
        };

        Comment createdComment = await commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment created with ID: {createdComment.Id}");
    }
    
}