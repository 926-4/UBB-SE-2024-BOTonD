using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.GroupRulesList
{
    public class Rule
    {
        public string Text { get; }
        public Guid Id { get; } = Guid.NewGuid();

        public Rule(string text)
        {
            Text = text;
        }
    }
}