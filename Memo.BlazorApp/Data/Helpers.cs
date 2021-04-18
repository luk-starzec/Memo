using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Memo.BlazorApp.Data
{
    public static class Helpers
    {
        public static string GetUrl(string title)
            => HttpUtility.UrlEncode(title?.Trim().Replace(" ", "-").Replace(".", "-").ToLower());

        public static bool IsMobile(string userAgent)
        {
            var b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return b.IsMatch(userAgent);
        }
    }

}
