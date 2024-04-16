using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Report
{
    public class PostReport(Guid userId, Guid reportId, string message) : IReport
    {
        public Guid UserId { get; set; } = userId;
        public Guid Id { get; set; } = reportId;
        public string Message { get; set; } = message;
    }
}
