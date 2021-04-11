using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Memo.DataAccess.Tests
{
    public class CheckExpiryDateAttributeTests
    {
        [Fact]
        public void IsValid__When_NullValue__Returns_True()
        {
            var target = new ValidationTarget(null);
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(target, context, results, true);

            Assert.True(actual);
        }

        [Fact]
        public void IsValid__When_DateEarlierThanToday__Returns_False()
        {
            var target = new ValidationTarget(DateTime.Today.AddDays(-1));
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(target, context, results, true);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void IsValid__When_TodayOrLaterDate__Returns_True(int daysAfterToday)
        {
            var target = new ValidationTarget(DateTime.Today.AddDays(daysAfterToday));
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(target, context, results, true);

            Assert.True(actual);
        }

        private class ValidationTarget
        {
            [CheckExpiryDate]
            public DateTime? TestDate { get; set; }

            public ValidationTarget(DateTime? testDate) => TestDate = testDate;
        }
    }
}
