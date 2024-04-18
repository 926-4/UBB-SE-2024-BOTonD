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

    public class UserStatus(UserRestriction restriction, DateTime restrictionDate, string message = "")
    {
        public UserRestriction Restriction { get; set; } = restriction;
        public DateTime RestrictionDate { get; set; } = restrictionDate;
        public string Message { get; set; } = message;
    }
}