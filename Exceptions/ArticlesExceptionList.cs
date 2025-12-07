using GData.Entity;

namespace GData.Exceptions
{
    public class ArticlesExceptionList
    {

        public Task<Article> ArticleNotFound()
        {

            throw new ArgumentNullException("The requested article was not found!");

        }

        //Create article

        public Task<Article> ArticleCreatorDoesNotExist()
        {

            throw new ArgumentNullException("The article cannot be created because its creator is deleted or does not exist!");

        }

        public Task<Article> NoDataProvidedForArticle()
        {

            throw new FormatException("The data submited in the request is insufficient!");

        }

        public Task<Article> BadDataFormat()
        {

            throw new FormatException("All fields need to contain at least 4 characters");

        }

        public Task<Article> InvalidUser()
        {

            throw new UnauthorizedAccessException("Only users with admin rights can edit articles!");

        }

        public Task<Article> UnverifiedUserEmail()
        {

            throw new UnauthorizedAccessException("The user editing the article has not confirmed his email!");

        }

        //Edit article

        public Task<Article> EditArticleCreatorDoesNotExist()
        {

            throw new ArgumentNullException("The article cannot be edited because its creator is deleted or does not exist!");

        }

    }
}
