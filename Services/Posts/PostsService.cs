using GData.DTOs.PostDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.Posts;
using GData.Services.Users;

namespace GData.Services.Posts;

public class PostsService(IPostsRepository postsRepository, IAuthServices authServices) : IPostsService
{
    public async Task<Post> CreatePostService(Guid OwnerId, PostDTO request)
    {
        User owner = await authServices.GetUserByIdService(OwnerId);

        if (owner == null)
        {
            throw new NotFoundException("Owner does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new BadRequestException("No title has been provided for the post.");
        }

        if (request.Title.Length < 3)
        {
            throw new BadRequestException("Title needs to have more than 3 characters.");
        }

        if (owner.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Owner email is not verified.");
        }

        var post = new Post()
        {
            OwnerId = OwnerId,
            Title = request.Title,
            DateCreated = DateTime.UtcNow,
        };

        await postsRepository.CreatePost(post);
        return post;
    }

    public async Task<Post> DeletePostService(Guid ownerId, Guid Id)
    {
        var owner = await authServices.GetUserByIdService(ownerId);
        var post = await postsRepository.GetPostById(Id);

        if (owner is null)
        {
            throw new NotFoundException("Post owner does not exist.");
        }

        if (ownerId != owner.Id)
        {
            throw new ForbiddenException("Owner is not valid.");
        }

        if (post is null)
        {
            throw new NotFoundException("Post does not exist.");
        }

        if (owner.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Owner email is not verified.");
        }

        await postsRepository.DeletePost(post);
        return post;
    }

    public async Task<List<Post>> GetAllPosts()
    {
        return await postsRepository.GetAllPosts();
    }

    public async Task<List<Post>> GetAllPostsByUser(Guid ownerId)
    {
        List<Post> selectedPosts = new List<Post>();
        var posts = await GetAllPosts();

        foreach (var post in posts)
        {
            if (post.OwnerId == ownerId)
            {
                selectedPosts.Add(post);
            }
        }

        return selectedPosts;
    }

    public async Task<Post> GetPostById(Guid Id)
    {
        var post = await postsRepository.GetPostById(Id);

        if (post is null)
        {
            throw new NotFoundException("Post does not exist.");
        }

        return post;
    }

    public async Task<Post> UpdatePostService(Guid ownerId, PostDTO request, Guid Id)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new BadRequestException("No title has been provided for the post.");
        }

        if (request.Title.Length < 3)
        {
            throw new BadRequestException("Title needs to have more than 3 characters.");
        }

        var post = await GetPostById(Id);

        if (post.Owner is null)
        {
            throw new NotFoundException("Post owner does not exist.");
        }

        if (ownerId != post.OwnerId)
        {
            throw new ForbiddenException("Owner is not valid.");
        }

        if (post is null)
        {
            throw new NotFoundException("Post does not exist.");
        }

        if (post.Owner.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Owner email is not verified.");
        }

        await postsRepository.EditPost(request, post);
        return post;
    }
}