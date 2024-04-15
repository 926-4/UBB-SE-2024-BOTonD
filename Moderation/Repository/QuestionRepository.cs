using Moderation.GroupEntryForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class QuestionRepository : Repository<Question>
    {
        public QuestionRepository(Dictionary<Guid, Question> data) : base(data) { }
        public QuestionRepository() : base() { }

        //public IEnumerable<Question> GetQuestionsByGroup(Guid groupId)
        //{
        //    return data.Values.Where(q => q.GroupId == groupId);
        //}
    }
}
