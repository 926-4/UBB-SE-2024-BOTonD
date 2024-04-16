using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class ReportRepository : Repository<Role>
    {
        public ReportRepository(Dictionary<Guid, Role> data) : base(data) { }
        public ReportRepository() : base() { }

        //public IEnumerable<Report> GetReportsByGroup(Guid groupId)
        //{
        //    return data.Values.Where(q => q.GroupId == groupId);
        //}
    }
}
