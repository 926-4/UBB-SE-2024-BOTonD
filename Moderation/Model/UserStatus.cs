using Moderation.Model;

namespace Moderation.Entities
{

    public class UserStatus(UserRestriction restriction, DateTime restrictionDate, string message = "")
    {
        public UserRestriction Restriction { get; set; } = restriction;
        public DateTime RestrictionDate { get; set; } = restrictionDate;
        public string Message { get; set; } = message;
    }
}