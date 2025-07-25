using System.Net.Http.Json;
using BlogApp.Core.Constants;
using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Models;

namespace BlogApp.Repositories;

public class CommentApiRepository(HttpClient httpClient) : ICommentApiRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Comment>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<Comment>>(ApiConstants.Comments))!;
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<Comment>($"{ApiConstants.Comments}/{id}"))!;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiConstants.Posts, comment);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<Comment>())!;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ApiConstants.Comments}/{comment.Id}", comment);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<Comment>())!;
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{ApiConstants.Comments}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return (await _httpClient.GetFromJsonAsync<List<Comment>>($"{ApiConstants.Comments}?postId={postId}"))!;
    }
}