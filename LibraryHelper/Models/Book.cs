using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHelper.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Year { get; set; }

        public string Isbn10 { get; set; }

        public string Isbn13 { get; set; }

        public string Edition { get; set; }

        public string BackLink { get; set; }
    }
}
