using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Award
    {
        public Guid awardId { get; set; }
        public enum AwardType
        {
            Bronze,
            Silver,
            Gold
        }
        public AwardType awardType { get; set; }
    } 
}
