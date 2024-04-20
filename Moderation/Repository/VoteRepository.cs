using Moderation.DbEndpoints;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class VoteRepository : Repository<Vote>
    {
        public VoteRepository(Dictionary<Guid, Vote> data) : base(data) { }
        public VoteRepository() : base() { }

        public override bool Add(Guid key, Vote value)
        {
            VoteEndpoints.CreateVote(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return VoteEndpoints.ReadAllVotes().Exists(a => a.Id == key);
        }

        public override Vote? Get(Guid key)
        {
            return VoteEndpoints.ReadAllVotes().Find(a => a.Id == key);
        }

        public override IEnumerable<Vote> GetAll()
        {
            return VoteEndpoints.ReadAllVotes();
        }

        public override bool Remove(Guid key)
        {
            VoteEndpoints.DeleteVote(key);
            return true;
        }
    }
}
