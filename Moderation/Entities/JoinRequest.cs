using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class JoinRequest
    {
        public Guid joinRequestID { get; set; }
        public Guid userId { get; set; }
        public Dictionary<string, string> messageResponse { get; set; }

        public JoinRequest(Guid userId, Dictionary<string, string> messageResponse)
        {
            this.userId = userId;
            this.messageResponse = messageResponse;
        }
    }
}
