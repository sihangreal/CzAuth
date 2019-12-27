using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CzAuth.Entities
{
    [Table("User")]
    public class User: Entity<int>
    {
        [Key]
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
