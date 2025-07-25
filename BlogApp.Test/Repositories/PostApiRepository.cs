using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Models;
using Moq;

namespace BlogApp.Test.Repositories;

public class PostApiRepository
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectProduct()
    {
        var mockRepo = new Mock<IPostApiRepository>();

        var expectedPost = new Post
        {
            Id = 1,
            Title = "Test",
            Body = "Test",
        };

        mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedPost);

        var result = await mockRepo.Object.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(expectedPost.Id, result.Id);
        Assert.Equal(expectedPost.Title, result.Title);
        Assert.Equal(expectedPost.Body, result.Body);
    }
}
