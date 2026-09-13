using CLI.UI;  //Access to CliApp
using InMemoryRepositories; 
using RepositoryContracts;

// making the actual repository objects 
IUserRepository userRepository = new UserInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();

// Create CliApp and give it the three repositories
CliApp cliApp = new CliApp(
    userRepository,
    postRepository,
    commentRepository
);

{
// Starts the command line application
    await cliApp.StartAsync();
}
