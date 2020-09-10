using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.Utilities.Security;
using Xunit;

namespace TaskManagerApp.Core.Tests
{
    public class SecurityTests
    {
        [Fact]
        public void Should_Throw_Exception_Hashed_For_Empty_Password()
        {
            string password = "";
            Assert.Throws<ArgumentException>(() =>
            {
                HashingHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            });
        }

        [Fact]
        public void Should_Throw_Exception_Hashed_For_Null_Password()
        {
            string password = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                HashingHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            });
        }

        [Fact]
        public void Should_Verify()
        {
            string password = "11111111";
            HashingHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            bool result = HashingHelper.VerifyPasswordHash(password, hash, salt);
            Assert.True(result);
        }

        [Fact]
        public void Should_Not_Verify()
        {
            string truePassword = "11111111";
            string wrongPassword = truePassword + "1";
            HashingHelper.CreatePasswordHash(truePassword, out byte[] hash, out byte[] salt);
            bool result = HashingHelper.VerifyPasswordHash(wrongPassword, hash, salt);
            Assert.False(result);
        }

        [Fact]
        public void Should_Not_Verify_For_Null_Parameters()
        {
            string password = "11111111";
            byte[] hash = null;
            byte[] salt = null;
            
            bool result = HashingHelper.VerifyPasswordHash(password, hash, salt);
            Assert.False(result);
        }

    }
}
