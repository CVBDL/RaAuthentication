using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAAuthentication.JWTAuthentication;

namespace RAAuthentication.Tests
{
    [TestClass()]
    public class JWTAuthenticationTest
    {
        [TestMethod()]
        public void DateTimeTest()
        {
            string token = JWTAuthenticate.Instance().GetToken("lliao2");

            bool valid = JWTAuthenticate.Instance().IsValid(token);
            Assert.IsTrue(valid);
        }
    }
}
