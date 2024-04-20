using Moderation.DbEndpoints;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class AwardRepository : Repository<Award>
    {
        public AwardRepository(Dictionary<Guid, Award> data) : base(data) { }
        public AwardRepository() : base() { }

        public override bool Add(Guid key, Award value)
        {
            AwardEndpoint.CreateAward(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return AwardEndpoint.ReadAwards().Exists(a => a.Id == key);
        }

        public override Award? Get(Guid key)
        {
            return AwardEndpoint.ReadAwards().Find(a => a.Id == key);
        }

        public override IEnumerable<Award> GetAll()
        {
            return AwardEndpoint.ReadAwards();
        }

        public override bool Remove(Guid key)
        {
            AwardEndpoint.DeleteAward(key);
            return true;
        }

        public override bool Update(Guid key, Award award)
        {
            AwardEndpoint.UpdateAward(award);
            return true;
        }
    }
}
