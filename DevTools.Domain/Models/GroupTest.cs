using System;
using System.Collections.Generic;

namespace DevTools.Domain.Models
{
    public class GroupTest
    {
        public GroupTest()
        {
            Tests = new HashSet<Test>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Test> Tests { get; }
    }
}