using Moderation.DbEndpoints;
using Moderation.Entities.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class TextPostRepository : Repository<TextPost>
    {
        public TextPostRepository(Dictionary<Guid, TextPost> data) : base(data) { }
        public TextPostRepository() : base() { }

        public override bool Add(Guid key, TextPost value)
        {
            TextPostEndpoints.CreateTextPost(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return TextPostEndpoints.ReadAllTextPosts().Exists(a => a.Id == key);
        }

        public override TextPost? Get(Guid key)
        {
            return TextPostEndpoints.ReadAllTextPosts().Find(a => a.Id == key);
        }

        public override IEnumerable<TextPost> GetAll()
        {
            return TextPostEndpoints.ReadAllTextPosts();
        }

        public override bool Remove(Guid key)
        {
            TextPostEndpoints.DeleteTextPost(key);
            return true;
        }

        public override bool Update(Guid key, TextPost value)
        {
            TextPostEndpoints.UpdateTextPost(value);
            return true;
        }
    }
}
