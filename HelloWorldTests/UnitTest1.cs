using System;
using System.Net;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HelloWorldTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string res = Finance.GetQuote("TLS.AX");// = HelloWorld.h
            Assert.AreNotEqual(res, "");
             
        }
    }
}
