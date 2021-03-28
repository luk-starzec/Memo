using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Memo.DataAccess.Models
{
    public class MemoEditModel
    {
        public string Url { get; set; }
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        public bool EnabledValidTo { get; set; }
        [CheckValidTo]
        public DateTime? ValidTo { get; set; }
    }
}
