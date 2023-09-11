﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Models
{
    public class FacultyMember
    {
        public long FacultyId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; } = null;
        public string Gender { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string OfficeRoomNumber { get; set; } = string.Empty;
        public virtual ICollection<Subject>? Subjects { get; set; } = null;
    }
}
