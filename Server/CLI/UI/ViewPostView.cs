using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class ViewPostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ViewPostView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }
    
    // Shows one specific post
    public async Task ShowPostAsync()
    {
        Console.Write("Enter post ID: ");
        string? postIdInput = Console.ReadLine();

        if (!int.TryParse(postIdInput, out int postId))
        {
            Console.WriteLine("Post ID must be a number.");
            return;
        }
        Post post = await postRepository.GetSingleAsync(postId);

        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        
        IQueryable<Comment> comments = commentRepository
            .GetMany()
            .Where(comment => comment.PostId == postId);

        Console.WriteLine("Comments:");

        foreach (Comment comment in comments)
        {
            Console.WriteLine($"- {comment.Body}");
        }
    }
}