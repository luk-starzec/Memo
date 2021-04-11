using Memo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Memo.DataAccess.Tests
{
    public class MemoEditModelTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("  ", false)]
        [InlineData(null, false)]
        [InlineData("a", true)]
        [InlineData("test", true)]
        public void Text_Validation(string text, bool expected)
        {
            var model = new MemoEditModel
            {
                Text = text
            };
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        public void Expires_Validation(int? daysAfterToday, bool expected)
        {
            var model = new MemoEditModel
            {
                Text = "test",
                Expires = daysAfterToday.HasValue ? DateTime.Today.AddDays(daysAfterToday.Value) : null,
            };
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expected, actual);
        }
    }
}
