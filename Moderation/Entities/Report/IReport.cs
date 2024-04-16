using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Report
{
    public interface IReport: IHasID
    {
        Guid UserId { get; set; }
        string Message { get; set; }
    }
}
