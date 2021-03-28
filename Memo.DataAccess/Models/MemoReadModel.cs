using System;
using System.Collections.Generic;
using System.Text;

namespace Memo.DataAccess.Models
{
    public class MemoReadModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string EncryptedData { get; set; }
        public string IV { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
