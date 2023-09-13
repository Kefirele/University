using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Interfaces
{
    public interface ILibrary
    {
        long LibraryId { get; set; }
        string Name { get; set; }
        string Adress { get; set; }
        int? NumberOfFloors { get; set; }
        int? NumberOfRooms { get; set; }
        string Description { get; set; }
        string Librarian { get; set; }
        bool IsSelected { get; set; }
    }
}
