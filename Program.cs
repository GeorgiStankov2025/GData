using GData.Data;
using GData.Exceptions;
using GData.Repositories.Articles;
using GData.Repositories.ArticlesComments;
using GData.Repositories.ArticlesTags;
using GData.Repositories.GroupChat;
using GData.Repositories.Groupchats;
using GData.Repositories.GroupchatsMessages;
using GData.Repositories.Posts;
using GData.Repositories.PostsComments;
using GData.Repositories.Users;
using GData.Services.Articles;
using GData.Services.ArticlesComments;
using GData.Services.ArticlesTags;
using GData.Services.Groupchats;
using GData.Services.GroupchatsMessages;
using GData.Services.Posts;
using GData.Services.PostsComments;
using GData.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Services.AddScoped<IPostsRepository, PostsRepository>();
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IPostsCommentsRepository, PostsCommentsRepository>();
builder.Services.AddScoped<IPostsCommentsService, PostsCommentsService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleServices, ArticleServices>();
builder.Services.AddScoped<IArticlesCommentsRepository, ArticlesCommentsRepository>();
builder.Services.AddScoped<IArticlesCommentsServices, ArticlesCommentsServices>();
builder.Services.AddScoped<IGroupChatRepository, GroupChatRepository>();
builder.Services.AddScoped<IGroupChatsServices, GroupChatsServices>();
builder.Services.AddScoped<IGroupChatsMessagesRepository, GroupChatsMessagesRepository>();
builder.Services.AddScoped<IGroupchatsMessagesServices, GroupchatsMessagesServices>();
builder.Services.AddScoped<IArticlesTagsRepository, ArticlesTagsRepository>();
builder.Services.AddScoped<IArticleTagsServices, ArticleTagsServices>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(options => options.CustomizeProblemDetails = (context) =>
{
    var httpContext = context.HttpContext;
    context.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;
    context.ProblemDetails.Extensions["supportContact"] = "bitproductions2024@gmail.com";
});

builder.Services.AddOpenApi();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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
var key = Encoding.UTF8.GetBytes(secret!);

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

var app = builder.Build();


app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();