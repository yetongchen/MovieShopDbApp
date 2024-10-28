using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class UserRole
    {
        public required int UserId { get; set; }
        public required int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
