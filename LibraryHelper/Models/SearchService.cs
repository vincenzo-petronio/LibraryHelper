using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryHelper.Models
{
    public class SearchService : ISearchService
    {
        private const string FindByAuthorTitleIsbn = @"https://www.goodreads.com/search.xml?key={0}&q={1}";

        /// <summary>
        /// Search for a book by the ISBN code.
        /// </summary>
        /// <param name="isbn">string ISBN10/13</param>
        /// <returns>Book</returns>
        public async Task<Book> SearchForIsbnAsync(string isbn)
        {
            Book book = new Book();

            // TODO purge chars from isbn and check if isbn length == 10 OR 13

            try
            {
                string key = System.Configuration.ConfigurationManager.AppSettings.Get("API_KEY_GOODREADS");
                App.logger.Debug("DEV KEY: \t" + key);
                string url = string.Format(FindByAuthorTitleIsbn, key, isbn);
                await GetXmlResponse(url);
                
                // TODO Parser
            }
            catch (Exception e)
            {
                // TODO
            }

            return book;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<XDocument> GetXmlResponse(string url)
        {
            string result = string.Empty;
            try
            {
                var httpWebReq = (HttpWebRequest)WebRequest.Create(url);
                httpWebReq.Method = "GET";

                using (WebResponse webRes = await httpWebReq.GetResponseAsync())
                {
                    using (StreamReader resStream = new StreamReader(webRes.GetResponseStream()))
                    {
                        result = await resStream.ReadToEndAsync();
                        App.logger.Debug(result);
                    }
                }
            }
            catch (Exception e)
            {
                App.logger.Error(e.ToString());
            }

            return XDocument.Load(result);
        }
    }
}
