using Moderation.DbEndpoints;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class JoinRequestAnswerForOneQuestionRepository : Repository<JoinRequestAnswerToOneQuestion>
    {
        public JoinRequestAnswerForOneQuestionRepository(Dictionary<Guid, JoinRequestAnswerToOneQuestion> data) : base(data) { }
        public JoinRequestAnswerForOneQuestionRepository() : base() { }

        public override bool Add(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            JoinRequestAnswerForOneQuestionEndpoints.CreateQuestion(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return JoinRequestAnswerForOneQuestionEndpoints.ReadQuestion().Exists(a => a.Id == key);
        }

        public override JoinRequestAnswerToOneQuestion? Get(Guid key)
        {
            return JoinRequestAnswerForOneQuestionEndpoints.ReadQuestion().Find(a => a.Id == key);
        }

        public override IEnumerable<JoinRequestAnswerToOneQuestion> GetAll()
        {
            return JoinRequestAnswerForOneQuestionEndpoints.ReadQuestion();
        }

        public override bool Remove(Guid key)
        {
            throw new Exception("Remove needs more than just the id");
        }
        public static bool Remove(JoinRequestAnswerToOneQuestion question)
        {
            JoinRequestAnswerForOneQuestionEndpoints.DeleteQuestion(question);
            return true;
        }

        public override bool Update(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            JoinRequestAnswerForOneQuestionEndpoints.UpdateQuestion(value);
            return true;
        }
    }
}
