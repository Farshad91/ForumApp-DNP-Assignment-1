using Entities;
using RepositoryContracts;
using System.Text.Json;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    
    private readonly string filePath = "comments.json";
    
    // Create comments.json if it does not exist
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        
        
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 0;
        
        comment.Id = maxId + 1;
        
        comments.Add(comment);
        
        commentsAsJson = JsonSerializer.Serialize(comments);
        
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        
        
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        
        Comment? existingComment =
            comments.FirstOrDefault(c => c.Id == comment.Id);
        
        if (existingComment == null)
        {
            throw new Exception($"Comment with ID {comment.Id} was not found.");
        }

        // Update the existing comment
        existingComment.Body = comment.Body;
        existingComment.UserId = comment.UserId;
        existingComment.PostId = comment.PostId;

        // Convert the updated list back to JSON
        commentsAsJson = JsonSerializer.Serialize(comments);

        // Save the JSON back into the file
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    
    

    public async Task DeleteAsync(int id)
    {
        // Read the JSON file
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        // Convert JSON into a List<Comment>
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        // Find the comment with the matching ID
        Comment? commentToDelete =
            comments.FirstOrDefault(c => c.Id == id);

        // If the comment does not exist, stop
        if (commentToDelete == null)
        {
            throw new Exception($"Comment with ID {id} was not found.");
        }

        // Remove the comment from the list
        comments.Remove(commentToDelete);

        // Convert the updated list back to JSON
        commentsAsJson = JsonSerializer.Serialize(comments);

        // Save the updated JSON back to the file
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        
        Comment? comment =
            comments.FirstOrDefault(c => c.Id == id);

        if (comment == null)
        {
            throw new Exception($"Comment with ID {id} was not found.");
        }

        
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;

        // Convert JSON into a List<Comment>
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        // Return the list as IQueryable
        return comments.AsQueryable();
    }
}