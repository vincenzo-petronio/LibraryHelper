using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHelper.Models
{
    public class SearchService : ISearchService
    {
        /// <summary>
        /// Search for a book by the ISBN code.
        /// </summary>
        /// <param name="isbn">string ISBN10/13</param>
        /// <returns>Book</returns>
        public async Task<Book> SearchForIsbnAsync(string isbn)
        {
            Book book = new Book();

            try
            {
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }

            return book;
        }
    }
}
