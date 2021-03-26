using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Memo.DataAccess.Models
{
    public class MemoModel
    {
        public string Url => GetUrl();

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Message { get; set; }

        public DateTime Created { get; set; }

        public bool EnabledValidTo { get; set; }
        public DateTime? ValidTo { get; set; }

        [RegularExpression("([0-9]+)")]
        [MinLength(4)]
        public string Pin { get; set; }

        [RegularExpression("([0-9]+)")]
        [Compare(nameof(Pin), ErrorMessage = "Fields Pin and Confirm must be the same")]
        public string ConfirmPin { get; set; }

        public List<ValidationResult> Validate()
        {
            var ctx = new ValidationContext(this);
            var results = new List<ValidationResult>();

            //if (!Validator.TryValidateObject(this, ctx, results, true))
            //{
            //    return results;
            //}

            Validator.TryValidateObject(this, ctx, results, true);

            return results;
        }

        private string GetUrl() => Title?.Replace(" ", "-").Trim();

    }
}
