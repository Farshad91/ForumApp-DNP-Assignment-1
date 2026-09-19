using CLI.UI; // Access to CliApp
using FileRepositories;
using RepositoryContracts;

// making the actual repository objects
IUserRepository userRepository = new UserFileRepository();
IPostRepository postRepository = new PostFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();

// Create CliApp and give it the three repositories
CliApp cliApp = new CliApp(
    userRepository,
    postRepository,
    commentRepository
);

// Starts the command line application
await cliApp.StartAsync();
