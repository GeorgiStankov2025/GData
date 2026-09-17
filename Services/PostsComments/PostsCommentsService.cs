using GData.DTOs.PostsDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.PostsComments;
using GData.Services.Posts;
using GData.Services.Users;

namespace GData.Services.PostsComments;

public class PostsCommentsService(
    IPostsCommentsRepository postsCommentsRepository,
    IAuthServices authServices,
    IPostsService postsServices) : IPostsCommentsService
{
    public async Task<PostComment> CreatePostCommentService(Guid authorId, Guid postId, PostCommentsDTO request)
    {
        var author = await authServices.GetUserByIdService(authorId);
        var post = await postsServices.GetPostById(postId);

        if (author is null)
        {
            throw new NotFoundException("Post comment author does not exist.");
        }

        if (post is null)
        {
            throw new NotFoundException("Post does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.CommentContent))
        {
            throw new BadRequestException("No content has been provided for post comment.");
        }

        if (request.CommentContent.Length < 3)
        {
            throw new BadRequestException("Content needs to have more than 3 characters.");
        }

        if (author.IsEmailConfirmed is false)
        {
            throw new ForbiddenException("Author email is not verified.");
        }

        var postComment = new PostComment()
        {
            AuthorId = authorId,
            PostId = postId,
            Content = request.CommentContent,
            DateCreated = DateTime.UtcNow,
        };

        await postsCommentsRepository.CreatePostComment(postComment);
        return postComment;
    }

    public async Task<PostComment> EditPostCommentService(Guid authorId, Guid postId, Guid Id, PostCommentsDTO request)
    {
        var postComment = await GetPostCommentById(Id);
        var author = await authServices.GetUserByIdService(authorId);
        var post = await postsServices.GetPostById(postId);

        if (postComment is null)
        {
            throw new NotFoundException("Post comment does not exist.");
        }

        if (postComment.PostId != postId)
        {
            throw new BadRequestException("Post ID is not valid for this comment.");
        }

        if (postComment.AuthorId != authorId)
        {
            throw new ForbiddenException("Author is not valid.");
        }

        if (post is null)
        {
            throw new NotFoundException("Post does not exist.");
        }

        if (author is null)
        {
            throw new NotFoundException("Author does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.CommentContent))
        {
            throw new BadRequestException("No content has been provided for post comment.");
        }

        if (request.CommentContent.Length < 3)
        {
            throw new BadRequestException("Content needs to have more than 3 characters.");
        }

        if (author.IsEmailConfirmed is false)
        {
            throw new ForbiddenException("Author email is not verified.");
        }

        var result = await postsCommentsRepository.EditPostComment(request, postComment);
        return result;
    }

    public async Task<List<PostComment>> GetAllPostCommentsByUserInPostService(Guid postId, Guid authorId)
    {
        List<PostComment> selectedPostComments = new List<PostComment>();
        var postComments = await GetAllPostCommentsInPostService(postId);

        foreach (var postComment in postComments)
        {
            if (postComment.AuthorId == authorId && postComment.PostId == postId)
            {
                selectedPostComments.Add(postComment);
            }
        }

        return selectedPostComments;
    }

    public async Task<List<PostComment>> GetAllPostCommentsInPostService(Guid postId)
    {
        List<PostComment> selectedPostComments = new List<PostComment>();
        var postComments = await postsCommentsRepository.GetAllPostComments();

        foreach (var postComment in postComments)
        {
            if (postComment.PostId == postId)
            {
                selectedPostComments.Add(postComment);
            }
        }

        return selectedPostComments;
    }

    public async Task<List<PostComment>> GetAllPostCommentsService()
    {
        return await postsCommentsRepository.GetAllPostComments();
    }

    public async Task<PostComment> GetPostCommentById(Guid Id)
    {
        var postComment = await postsCommentsRepository.GetPostCommentById(Id);
        return postComment;
    }

    public async Task<PostComment> DeletePostCommentService(Guid authorId, Guid postId, Guid Id)
    {
        var author = await authServices.GetUserByIdService(authorId);
        var post = await postsServices.GetPostById(postId);
        var postComment = await GetPostCommentById(Id);

        if (postComment is null)
        {
            throw new NotFoundException("Post comment does not exist.");
        }

        if (postComment.PostId != postId)
        {
            throw new BadRequestException("Post ID is not valid for this comment.");
        }

        if (postComment.AuthorId != authorId)
        {
            throw new ForbiddenException("Author is not valid.");
        }

        if (post is null)
        {
            throw new NotFoundException("Post does not exist.");
        }

        if (author is null)
        {
            throw new NotFoundException("Author does not exist.");
        }

        await postsCommentsRepository.DeletePostComment(postComment);
        return postComment;
    }
}