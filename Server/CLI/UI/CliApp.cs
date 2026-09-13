using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    private readonly CreateUserView createUserView;
    private readonly CreatePostView createPostView;
    private readonly AddCommentView addCommentView;
    private readonly ViewPostsView viewPostsView;
    private readonly ViewPostView viewPostView;

    public CliApp(
        IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;

        this.createUserView = new CreateUserView(userRepository);
        this.createPostView = new CreatePostView(postRepository);
        this.addCommentView = new AddCommentView(commentRepository);
        this.viewPostsView = new ViewPostsView(postRepository);
        this.viewPostView = new ViewPostView(
            postRepository,
            commentRepository
        );
    }


    // Starts the command line application

    // Starts the command line application
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("=== Forum App ===");
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. Create post");
            Console.WriteLine("3. Add comment");
            Console.WriteLine("4. View posts");
            Console.WriteLine("5. View specific post");
            Console.WriteLine("0. Exit");

            Console.Write("Choose an option: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await createUserView.CreateUserAsync();
                    break;

                case "2":
                    await createPostView.CreatePostAsync();
                    break;

                case "3":
                    await addCommentView.AddCommentAsync();
                    break;

                case "4":
                    viewPostsView.ShowPosts();
                    break;

                case "5":
                    await viewPostView.ShowPostAsync();
                    break;

                case "0":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            Console.WriteLine();
        }
    }
}