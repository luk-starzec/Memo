using System;

namespace Memo.DataAccess.Models
{
    internal class MemoRow
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public string Data { get; set; }
        public string IV { get; set; }
    }
}
