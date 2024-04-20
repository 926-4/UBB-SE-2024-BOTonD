using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class JoinRequest : IHasID
    {
        public Guid Id { get; set; }
        public Guid userId { get; set; }
        //public Dictionary<string, string> messageResponse { get; set; }

        public JoinRequest(Guid userId)
        {
            Id=Guid.NewGuid();
            this.userId = userId;
            //this.messageResponse = messageResponse;
        }
        public JoinRequest(Guid id, Guid userId) 
        {
            this.Id=id;
            this.userId = userId;
        }
    }
}
