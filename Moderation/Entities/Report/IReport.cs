using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Report
{
    public interface IReport
    {
        Guid userId { get; set; }
        Guid reportId { get; set; }
        string message { get; set; }
    }
}
