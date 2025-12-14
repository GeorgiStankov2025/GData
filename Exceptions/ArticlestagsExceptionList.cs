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

            throw new ArgumentNullException("The requested article does not exist or was deleted");

        }

        public Task<List<ArticleTag>> ArticleNotFoundForList()
        {

            throw new ArgumentNullException("The requested article does not exist or was deleted");

        }

    }
}
