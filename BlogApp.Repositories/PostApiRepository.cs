using System.Net.Http.Json;
using BlogApp.Core.Constants;
using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Models;

namespace BlogApp.Repositories;

public class PostApiRepository(HttpClient httpClient) : IPostApiRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Post>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<Post>>(ApiConstants.Posts))!;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<Post>($"{ApiConstants.Posts}/{id}"))!;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiConstants.Posts, post);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<Post>())!;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ApiConstants.Posts}/{post.Id}", post);
        response.EnsureSuccessStatusCode();
        
        return (await response.Content.ReadFromJsonAsync<Post>())!;
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{ApiConstants.Posts}/{id}");
        response.EnsureSuccessStatusCode();  
    }
}