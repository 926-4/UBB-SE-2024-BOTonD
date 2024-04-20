using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class GroupRules : Repository<Model.Rule>
    {
        public GroupRules(Dictionary<Guid, Model.Rule> data) : base(data) { }
        public GroupRules() : base() { }

        //public IEnumerable<GroupRules> GetGroupRulesByGroup(Guid groupId)
        //{
        //    return data.Values.Where(q => q.GroupId == groupId);
        //}
    }
}
