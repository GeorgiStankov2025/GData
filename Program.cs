using GData.Data;
using GData.Exceptions;
using GData.Repositories.Articles;
using GData.Repositories.ArticlesComments;
using GData.Repositories.GroupChat;
using GData.Repositories.Groupchats;
using GData.Repositories.Posts;
using GData.Repositories.PostsComments;
using GData.Repositories.Users;
using GData.Services.Articles;
using GData.Services.ArticlesComments;
using GData.Services.Groupchats;
using GData.Services.Posts;
using GData.Services.PostsComments;
using GData.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<GDataDbContext>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<UserExceptionList>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();
builder.Services.AddScoped<IPostsService,PostsService>();
builder.Services.AddScoped<PostsExceptionList>();
builder.Services.AddScoped<IPostsCommentsRepository, PostsCommentsRepository>();
builder.Services.AddScoped<IPostsCommentsService, PostsCommentsService>();
builder.Services.AddScoped<PostCommentsExceptionList>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleServices,ArticleServices>();
builder.Services.AddScoped<ArticlesExceptionList>();
builder.Services.AddScoped<IArticlesCommentsRepository, ArticlesCommentsRepository>();
builder.Services.AddScoped<IArticlesCommentsServices, ArticlesCommentsServices>();
builder.Services.AddScoped<ArticleCommentsExceptionList>();
builder.Services.AddScoped<IGroupChatRepository, GroupChatRepository>();
builder.Services.AddScoped<IGroupChatsServices, GroupChatsServices>();
builder.Services.AddScoped<GroupChatExceptionList>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }});
});

var secret = builder.Configuration.GetValue<string>("AppSettings:Token");
var issuer = builder.Configuration.GetValue<string>("AppSettings:Issuer");
var audience = builder.Configuration.GetValue<string>("AppSettings:Audience");
var key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:Token"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateIssuer = true,
        ValidIssuer = issuer,

        ValidateAudience = true,
        ValidAudience = audience,

        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero

    };

});

builder.Services.AddProblemDetails(options => options.CustomizeProblemDetails = (context) =>
{

    var httpcontext = context.HttpContext;
    context.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpcontext.TraceIdentifier;
    context.ProblemDetails.Extensions["supportContact"] = "bitproductions2024@gmail.com";

    if (context.ProblemDetails.Status == StatusCodes.Status401Unauthorized)
    {

        context.ProblemDetails.Title = "Unauthorized access";
        context.ProblemDetails.Detail = "You are not authorized to access this resource";

    }
    else if (context.ProblemDetails.Status == StatusCodes.Status400BadRequest)
    {

        context.ProblemDetails.Title = "Bad request";
        context.ProblemDetails.Detail = "Your request could not be proccessed";

    }
    else if (context.ProblemDetails.Status == StatusCodes.Status404NotFound)
    {

        context.ProblemDetails.Title = "Resource not found";
        context.ProblemDetails.Detail = "The requested resource was not found";

    }
    else
    {

        context.ProblemDetails.Title = "An unexpected error occured";
        context.ProblemDetails.Detail = "An unexpected error occured!Try again later";

    }

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
