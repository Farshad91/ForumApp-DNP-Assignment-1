using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class ViewPostsView
{
    private readonly IPostRepository postRepository;

    public ViewPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void ShowPosts()
    {
        IQueryable<Post> posts = postRepository.GetMany();

        foreach (Post post in posts)
        {
            Console.WriteLine($"ID: {post.Id} - Title: {post.Title}");
        }
    }
}