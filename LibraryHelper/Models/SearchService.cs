using LibraryHelper.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            Book book = null;
            
            try
            {
                string key = System.Configuration.ConfigurationManager.AppSettings.Get("API_KEY_GOODREADS");
                App.logger.Debug("DEV KEY: \t" + key);
                string url = string.Format(FindByAuthorTitleIsbn, key, isbn);

                string xmlResponse = await GetXmlResponse(url);

                if (!string.IsNullOrEmpty(xmlResponse))
                {
                    book = XmlParserGoodreads.ParseResponseForIsbn(xmlResponse);
                }
                else
                {
                    App.logger.Info("No data from Web Service!");
                }
            }
            catch (ConfigurationErrorsException)
            {
                App.logger.Error("NO API KEY");
            }
            catch (Exception e)
            {
                App.logger.Error(e.Message);
            }

            return book;
        }

        /// <summary>
        /// Return response from Web Service.
        /// </summary>
        /// <param name="url">string</param>
        /// <returns>string</returns>
        private async Task<string> GetXmlResponse(string url)
        {
            string result = string.Empty;
            
            try
            {
                var httpWebReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                httpWebReq.Method = "GET";

                using (WebResponse webRes = await httpWebReq.GetResponseAsync())
                {
                    using (StreamReader resStream = new StreamReader(webRes.GetResponseStream()))
                    {
                        result = await resStream.ReadToEndAsync();
                        App.logger.Debug("\n\n" + result + "\n\n");
                    }
                }
            }
            catch (Exception e)
            {
                App.logger.Error(e.ToString());
            }

            return result;
        }
    }
}
