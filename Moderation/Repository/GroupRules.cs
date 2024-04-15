using Moderation.GroupRulesView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class GroupRules : Repository<GroupRulesView.Rule>
    {
        public GroupRules(Dictionary<Guid, GroupRulesView.Rule> data) : base(data) { }
        public GroupRules() : base() { }

        //public IEnumerable<GroupRules> GetGroupRulesByGroup(Guid groupId)
        //{
        //    return data.Values.Where(q => q.GroupId == groupId);
        //}
    }
}
