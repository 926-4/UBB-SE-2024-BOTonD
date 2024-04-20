using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Award : IHasID
    {
        public Guid Id { get; set; }
        public enum AwardType
        {
            Bronze,
            Silver,
            Gold
        }
        public AwardType awardType { get; set; }
    } 
}
