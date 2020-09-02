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
        public void Should_Verify()
        {
            string password = "11111111";
            byte[] hash;
            byte[] salt;
            HashingHelper.CreatePasswordHash(password, out hash, out salt);
            bool result = HashingHelper.VerifyPasswordHash(password, hash, salt);
            Assert.True(result);
        }

        [Fact]
        public void Should_Not_Verify()
        {
            string truePassword = "11111111";
            string wrongPassword = truePassword + "1";
            byte[] hash;
            byte[] salt;
            HashingHelper.CreatePasswordHash(truePassword, out hash, out salt);
            bool result = HashingHelper.VerifyPasswordHash(wrongPassword, hash, salt);
            Assert.False(result);
        }
    }
}
