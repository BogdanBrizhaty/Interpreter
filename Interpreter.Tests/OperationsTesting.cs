using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Interpreter.Interpreter;

namespace Interpreter.Tests
{
    [TestClass]
    public class OperationsTesting
    {
        string source = String.Empty;
        string lookFor = String.Empty;
        public OperationsTesting()
        {
            source = "abrakadabra";
            lookFor = "bra";
        }
        [TestMethod]
        public void TestIndexOf()
        {
            var expectedIndex = (source).IndexOf(lookFor);
            var actualIndex = (int)OpList.Operations["INDEXOF"](new QueryParams(lookFor, source));
            Assert.AreEqual(expectedIndex, actualIndex);
        }
        [TestMethod]
        public void TestLastIndexOf()
        {
            var expectedIndex = (source).LastIndexOf(lookFor);
            var actualIndex = (int)OpList.Operations["LASTINDEXOF"](new QueryParams(lookFor, source));
            Assert.AreEqual(expectedIndex, actualIndex);
        }
        [TestMethod]
        public void TestExists()
        {
            var expected = (source).Contains(lookFor);
            var actual = (bool)OpList.Operations["EXISTS"](new QueryParams(lookFor, source));
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCount()
        {
            var actual = (int)OpList.Operations["COUNT"](new QueryParams(lookFor, source));
            Assert.AreEqual(2, actual);
        }
        [TestMethod]
        public void TestSelect()
        {
            var expected = new List<string>()
            {
                "brakadabra",
                "bra"
            };
            lookFor = "br";
            var actual = (List<string>)OpList.Operations["SELECT"](new QueryParams(lookFor, source, "", "a", "MIN"));
            Assert.AreEqual(2, 2);
        }
        [TestMethod]
        public void TestIndexesSelect()
        {
            var expected = new List<int>()
            {
                1,
                8
            };
            var actual = (List<int>)OpList.Operations["INDEXES"](new QueryParams(lookFor, source));
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
