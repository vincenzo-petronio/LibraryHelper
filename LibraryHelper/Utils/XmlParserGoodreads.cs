using LibraryHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryHelper.Utils
{
    /// <summary>
    /// Parser for GoodReads XML responses.
    /// </summary>
    public class XmlParserGoodreads
    {
        public static Book ParseResponseForIsbn(string response)
        {
            Book book;
            XDocument xdoc = XDocument.Parse(response);

            book = (from b in xdoc.Descendants("work")
                   select new Book()
                   {
                       Author = b.Descendants("best_book").First()
                                .Descendants("author").First()
                                .Element("name") != null 
                                ? 
                                b.Descendants("best_book").First()
                                .Descendants("author").First()
                                .Element("name").Value 
                                : 
                                string.Empty,
                       ////Edition = b.Element("") != null ? b.Element("").Value : string.Empty,
                       ////Isbn10 = b.Element("") != null ? b.Element("").Value : string.Empty,
                       ////Isbn13 = b.Element("") != null ? b.Element("").Value : string.Empty,
                       ////Publisher = 
                       Title = b.Descendants("best_book").First()
                                .Element("title") != null 
                                ?
                                b.Descendants("best_book").First()
                                .Element("title").Value 
                                : 
                                string.Empty,
                       Year = b.Element("original_publication_year") != null 
                                ? 
                                b.Element("original_publication_year").Value 
                                : 
                                string.Empty,
                   }).First();

            return book;
        }
    }
}
