using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class ReportRepository : Repository<PostReport>
    {
        public ReportRepository(Dictionary<Guid, PostReport> data) : base(data) { }
        public ReportRepository() : base() { }

        public override bool Add(Guid key, PostReport value)
        {
            ReportEndpoint.CreatePostReport(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return ReportEndpoint.ReadAllPostReports().Exists(r => r.Id == key);
        }

        public override PostReport? Get(Guid key)
        {
            return ReportEndpoint.ReadAllPostReports().Find(r => r.Id == key);
        }

        public override IEnumerable<PostReport> GetAll()
        {
            return ReportEndpoint.ReadAllPostReports();
        }

        public override bool Remove(Guid key)
        {
            ReportEndpoint.DeletePostReport(key);
            return true;
        }

        public override bool Update(Guid key, PostReport value)
        {
            ReportEndpoint.UpdatePostReport(key, value);
            return true;
        }
    }
}