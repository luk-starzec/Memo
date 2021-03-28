using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Memo.BlazorApp.Data
{
    public static class Helpers
    {
        public static string GetUrl(string title)
            => HttpUtility.UrlEncode(title?.Trim().Replace(" ", "-").ToLower());
    }
}
