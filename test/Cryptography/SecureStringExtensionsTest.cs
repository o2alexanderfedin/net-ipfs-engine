﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;

namespace Ipfs.Engine.Cryptography
{
    [TestClass]
    public class SecureStringExtensionsTest
    {
        [TestMethod]
        public void UseBytes()
        {
            var secret = new SecureString();
            var expected = new char[] { 'a', 'b', 'c' };
            foreach (var c in expected) secret.AppendChar(c);
            secret.UseSecretBytes(bytes =>
            {
                Assert.AreEqual(expected.Length, bytes.Length);
                for (var i = 0; i < expected.Length; ++i)
                    Assert.AreEqual((int)expected[i], (int)bytes[i]);
            });
        }


    }
}
