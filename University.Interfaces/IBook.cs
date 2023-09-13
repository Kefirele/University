using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Interfaces
{
    public interface IBook
    {
        long BookId { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Publisher { get; set; }
        DateTime? PublicationDate { get; set; }
        string Isbn { get; set; }
        string Genre { get; set; }
        string Description { get; set; }
    }
}
