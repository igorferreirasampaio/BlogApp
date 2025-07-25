using System.Text.Json.Serialization;

namespace BlogApp.Core.Models;

public class Post
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")] 
    public string? Title { get; set; }

    [JsonPropertyName("body")] 
    public string? Body { get; set; }
}