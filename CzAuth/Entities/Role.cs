using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzAuth.Entities
{
    public class Role: Entity<int>
    {
        public string Name { get; set; }

        public string Level { get; set; }

        public string Describle { get; set; }
    }
}
