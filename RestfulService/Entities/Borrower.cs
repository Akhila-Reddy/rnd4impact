using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Entities
{
    public class Borrower
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // Additional fields like contact info, address, etc., can be added here
    }
}

