using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public Guid RequestId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
    }
}
