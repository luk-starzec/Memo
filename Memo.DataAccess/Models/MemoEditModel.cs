using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Memo.DataAccess.Models
{
    public class MemoEditModel
    {

        public string Url { get; set; }

        [Required(ErrorMessage = "Text is required")]
        [MinLength(1)]
        public string Text { get; set; }

        public bool EnabledExpires
        {
            get => enabledExpires;
            set => SetEnabledExpires(value);
        }

        [CheckExpiryDate]
        public DateTime? Expires { get; set; }


        private bool enabledExpires;

        private void SetEnabledExpires(bool value)
        {
            if (enabledExpires == value)
                return;

            enabledExpires = value;

            Expires = enabledExpires ? (DateTime?)DateTime.Today.AddDays(7) : null;
        }
    }
}
