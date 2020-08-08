using System.Collections.Generic;

namespace DevTools.Domain.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; }



        public static readonly int Admin = 1;

        public static readonly int Supporter = 2;

        public static readonly int User = 3;
    }
}