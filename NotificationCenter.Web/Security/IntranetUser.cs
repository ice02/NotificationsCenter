using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Web.Security
{
    public class IntranetUser
    {
        public string NormalizedWindowsLogin { get; internal set; }
        public object Id { get; internal set; }
        public object UserName { get; internal set; }
    }
}
