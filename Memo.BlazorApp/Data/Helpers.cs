using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memo.BlazorApp.Data
{
    public static class Helpers
    {
        public static string GetUrl(string title) => title?.Replace(" ", "-").Trim();

    }
}
