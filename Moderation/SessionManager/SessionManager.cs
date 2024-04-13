using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Moderation.SessionManagerNamespace
{
    public class SessionManager(string user)
    {
        public string username { get; } = user;
        public DateTime LoginTime { get; } = DateTime.Now;
    }
}
