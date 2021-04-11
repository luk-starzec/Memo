using Xunit;

namespace Memo.DataAccess.Tests
{
    public class AesHelperTests
    {
        [Fact]
        public void EncryptAndDecrypt_WhenSamePass_ReturnsOryginalMessage()
        {
            var message = "this is test data";
            var pass = "12345";

            var encrypted = AesHelper.Encrypt(message, pass);
            var actual = AesHelper.Decrypt(encrypted.data, encrypted.iv, pass);

            Assert.Equal(message, actual);
        }

        [Fact]
        public void EncryptAndDecrypt_WhenDifferentPass_ReturnsNull()
        {
            var message = "this is test data";
            var pass1 = "12345";
            var pass2 = "1234";

            var encrypted = AesHelper.Encrypt(message, pass1);
            var actual = AesHelper.Decrypt(encrypted.data, encrypted.iv, pass2);

            Assert.Null(actual);
        }
    }
}
