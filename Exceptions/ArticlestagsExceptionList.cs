using GData.Entity;
using System;
using System.Threading.Tasks;

namespace GData.Exceptions
{
    public class ArticlestagsExceptionList
    {
      
        public Task<ArticleTag> NoDataProvidedForArticleTag()
        {

            throw new FormatException("No data has been provided for article tag data.");

        }

        public Task<ArticleTag> ArticleTagNotFound()
        {

            throw new ArgumentNullException("The requested article tag does not exist or was deleted");

        }

        public Task<ArticleTag> ArticleNotFound()
        {

            throw new ArgumentNullException("The requested article or post does not exist or was deleted");

        }

        public Task<List<ArticleTag>> ArticleNotFoundForList()
        {

            throw new ArgumentNullException("The requested article or post does not exist or was deleted");

        }

        public Task<ArticleTag> InvalidPostEditor()
        {

            throw new UnauthorizedAccessException("The user, who is trying to edit the post is not it's owner");

        }

        public Task<List<ArticleTag>> InvalidPostEditorList()
        {

            throw new UnauthorizedAccessException("The user, who is trying to edit the post is not it's owner");

        }

    }
}
