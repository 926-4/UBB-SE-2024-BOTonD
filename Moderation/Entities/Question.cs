using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Question
    {
        public Guid RequestId { get; set; }
        public string Text { get; set; }

        public string Answer {  get; set; }

        public Question(Guid requestId, string text, string answer)
        {
            RequestId = requestId;
            Text = text;
            Answer = answer;
        }
    }
}
