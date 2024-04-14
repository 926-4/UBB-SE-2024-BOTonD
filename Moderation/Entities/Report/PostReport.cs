using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Report
{
    public class PostReport : IReport
    {
        public Guid userId { get; set; }
        public Guid reportId { get; set; }
        public string message { get; set; }

        public PostReport(Guid userId, Guid reportId, string message)
        {
            this.userId = userId;
            this.reportId = reportId;
            this.message = message;
        }
    }
}
