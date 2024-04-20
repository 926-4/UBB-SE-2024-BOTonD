using Moderation.DbEndpoints;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class JoinRequestRepository : Repository<JoinRequest>
    {
        public JoinRequestRepository(Dictionary<Guid, JoinRequest> data) : base(data) { }
        public JoinRequestRepository() : base() { }

        public override bool Add(Guid key, JoinRequest value)
        {
            JoinRequestEndpoints.CreateJoinRequest(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return JoinRequestEndpoints.ReadAllJoinRequests().Exists(a => a.Id == key);
        }

        public override JoinRequest? Get(Guid key)
        {
            return JoinRequestEndpoints.ReadAllJoinRequests().Find(a => a.Id == key);
        }

        public override IEnumerable<JoinRequest> GetAll()
        {
            return JoinRequestEndpoints.ReadAllJoinRequests();
        }

        public override bool Remove(Guid key)
        {
            JoinRequestEndpoints.DeleteJoinRequest(key);
            return true;
        }
    }
}
