using Memo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Memo.DataAccess.Tests
{
    public class MemoNewModelTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("  ", false)]
        [InlineData(null, false)]
        [InlineData("abc", false)]
        [InlineData("title", true)]
        [InlineData("My title", true)]
        [InlineData("Lorem, ipsum dolor sit amet consectetur adipisicing elit. Aut quam, ipsa maxime adipisci natus", true)]
        [InlineData("Lorem, ipsum dolor sit amet consectetur adipisicing elit. Aut quam, ipsa maxime adipisci natus expedi", false)]
        public void Title_Validation(string title, bool expected)
        {
            var model = GetModel();
            model.Title = title;

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("  ", false)]
        [InlineData(null, false)]
        [InlineData("a", true)]
        [InlineData("test", true)]
        public void Text_Validation(string text, bool expected)
        {
            var model = GetModel();
            model.Text = text;

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
            var model = GetModel();
            model.Expires = daysAfterToday.HasValue ? DateTime.Today.AddDays(daysAfterToday.Value) : null;

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("000", false)]
        [InlineData("0000", true)]
        [InlineData("aaaa", false)]
        [InlineData("00000", true)]
        public void Pin_Validation(string pin, bool expected)
        {
            var model = GetModel();
            model.Pin = pin;
            model.ConfirmPin = pin;

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0000", "0001", false)]
        [InlineData("0000", "0000", true)]
        public void Confirm_Validation(string pin, string confirm, bool expected)
        {
            var model = GetModel();
            model.Pin = pin;
            model.ConfirmPin = confirm;

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var actual = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expected, actual);
        }


        MemoNewModel GetModel() =>
            new MemoNewModel
            {
                Url = "my-test",
                Title = "My test",
                Text = "This is test",
                Pin = "0000",
                ConfirmPin = "0000",
            };
    }
}
