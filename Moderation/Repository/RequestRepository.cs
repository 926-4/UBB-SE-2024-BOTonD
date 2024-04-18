using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class RequestRepository : Repository<JoinRequest>
    {
        public RequestRepository(Dictionary<Guid, JoinRequest> data) : base(data) { }
        public RequestRepository(): base() { }


    }
}
