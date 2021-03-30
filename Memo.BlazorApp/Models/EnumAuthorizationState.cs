using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memo.BlazorApp.Models
{
    public enum EnumAuthorizationState
    {
        None,
        Checking,
        Denied,
        Granted
    }
}
