using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Memo.DataAccess.Models
{
    public class MemoNewModel
    {
        [Required]
        public string Url { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(5, ErrorMessage = "Title should be at least 5 characters long")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text is required")]
        [MinLength(1, ErrorMessage = "Text can't be empty")]
        public string Text { get; set; }

        public bool EnabledExpires
        {
            get => enabledExpires;
            set => SetEnabledExpires(value);
        }

        [CheckExpiryDate]
        public DateTime? Expires { get; set; }

        [Required(ErrorMessage = "Pin is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Pin should be a number")]
        [MinLength(4, ErrorMessage = "Pin should be at least 4 characters long")]
        public string Pin { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Pin should be a number")]
        [Compare(nameof(Pin), ErrorMessage = "Fields Pin and Confirm must be the same")]
        public string ConfirmPin { get; set; }


        public List<ValidationResult> Validate()
        {
            var ctx = new ValidationContext(this);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(this, ctx, results, true);

            return results;
        }


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
