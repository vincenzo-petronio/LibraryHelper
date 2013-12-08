using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHelper.Models
{
    public interface ISearchService
    {
        Task<Book> SearchForIsbnAsync(string isbn);
    }
}
