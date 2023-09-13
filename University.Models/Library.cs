using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Interfaces;

namespace University.Models
{
    public class Library : ILibrary
    {
        public long LibraryId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public int? NumberOfFloors { get; set; } = null;
        public int? NumberOfRooms { get; set; } = null;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Subject>? Facilities { get; set; } = null;
        public string Librarian { get; set; } = string.Empty;
        public virtual ICollection<Book>? Books { get; set; } = null;
        public bool IsSelected { get; set; } = false;
    }
}
