using GData.Entity;

namespace GData.Exceptions
{
    public class ArticleCommentsExceptionList
    {

        //CreateArticle

        public Task<ArticleComment> CreateArticleCommentAuthorDoesNotExist()
        {

            throw new ArgumentNullException("The article comment cannot be created because its author does not exist or was deleted!");
        }

        public Task<ArticleComment> CreateArticleCommentArticleDoesNotExist()
        {

            throw new ArgumentNullException("The article comment cannot be created because its article does not exist or was deleted!");

        }

        public Task<ArticleComment> NoContentHasBeenProvidedForArticleComment()
        {

            throw new FormatException("No data has been provided for the article comment!"); //Also used in edit articlecomment and delete articlecomment

        }

        public Task<ArticleComment> ContentNeedsToHaveMoreThanThreeChars()
        {

            throw new FormatException("The provided content needs to have three or more characters"); //Also used in edit articlecomment and delete articlecomment

        }

        public Task<ArticleComment> UnverifiedAuthor()
        {

            throw new UnauthorizedAccessException("Article comment author's email address is not verified!"); //Also used in edit articlecomment and delete articlecomment

        }

        //Edit ArticleComment

        public Task<ArticleComment> ArticleCommentDoesNotExist()
        {

            throw new ArgumentNullException("The article comment cannot be edited because it does not exist or was deleted!"); //Also used in delete article comment

        }

        public Task<ArticleComment> EditArticleCommentAuthorDoesNotExist()
        {

            throw new ArgumentNullException("The article comment cannot be edited because its author does not exist or was deleted!"); //Also used in delete article comment

        }

        public Task<ArticleComment> EditArticleCommentArticleDoesNotExist()
        {

            throw new ArgumentNullException("The article comment cannot be edited because its article does not exist or was deleted!"); //Also used in delete article comment

        }

        public Task<ArticleComment> AuthorNotValid()
        {

            throw new UnauthorizedAccessException("The user who is trying to edit the article comment is not it's author!"); //Also used in delete article comment

        }

        public Task<ArticleComment> ArticleNotValid()
        {

            throw new UnauthorizedAccessException("The edited comment does not belong to this article!"); //Also used in delete article comment

        }

    }
}
