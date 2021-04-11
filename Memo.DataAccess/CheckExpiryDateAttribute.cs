using System;
using System.ComponentModel.DataAnnotations;

namespace Memo.DataAccess
{
    public class CheckExpiryDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = value as DateTime?;
            if (date is null)
                return ValidationResult.Success;

            return date >= DateTime.Today
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ?? "Date cannot be earlier than today", new[] { validationContext.MemberName });
        }
    }
}
