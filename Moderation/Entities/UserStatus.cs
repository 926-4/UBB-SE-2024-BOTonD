using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public enum UserRestriction
    {
        None,
        Muted,
        Banned
    }

    public class UserStatus
    {
        public UserRestriction restriction { get; set; }
        public DateTime restrictionDate { get; set; }
        public string message { get; set; }

        public UserStatus(UserRestriction restriction, DateTime restrictionDate, string message)
        {
            this.restriction = restriction;
            this.restrictionDate = restrictionDate;
            this.message = message;
        }
    }
}
