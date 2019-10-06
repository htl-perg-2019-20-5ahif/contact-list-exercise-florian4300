using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList
{
    public class Contact
    {
        [Required]
        public Int32 id { get; set; }
        public String firstname { get; set; }

        public String lastname { get; set; }

        [Required]
        public String email { get; set; }
    }
}
