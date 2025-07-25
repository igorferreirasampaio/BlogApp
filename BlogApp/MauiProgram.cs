using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Interfaces.Services;
using BlogApp.Data;
using BlogApp.Repositories;
using BlogApp.Services;
using BlogApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace BlogApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") });

            builder.Services.AddSingleton<DatabaseContext>();

            builder.Services.AddTransient<IPostApiRepository, PostApiRepository>();
            builder.Services.AddSingleton<IPostDbRepository, PostDbRepository>();

            builder.Services.AddTransient<ICommentApiRepository, CommentApiRepository>();
            builder.Services.AddSingleton<ICommentDbRepository, CommentDbRepository>();

            builder.Services.AddTransient<IAlertService, AlertService>();
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<ICommentService, CommentService>();

            builder.Services.AddTransient<PostsViewModel>();
            builder.Services.AddTransient<PostDetailViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            Task.Run(async () =>
            {
                var dbContext = app.Services.GetRequiredService<DatabaseContext>();
                await dbContext.InitializeAsync().ConfigureAwait(false);
            }).Wait();

            return app;
        }
    }
}
