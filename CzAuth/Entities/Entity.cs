using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzAuth.Entities
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }

        public int IsDeleted { get; set; }
    }
}
