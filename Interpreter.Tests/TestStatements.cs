using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter.Tables;
using System.Collections.Generic;
using Interpreter.Interpreter.Statements;

namespace Interpreter.Tests
{
    [TestClass]
    public class TestStatements
    {
        MemoryTable _table = null;
        public TestStatements()
        {
            _table = MemoryTable.Instance;
        }
        [TestMethod]
        public void TestVariableDefining()
        {
            var tokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("\"value here\"")
            };
            var def = new DefineVariableStatement(tokens, _table);
            def.Execute();

            Assert.IsTrue(_table.Lookup("MyVar1") != null);
        }
        [TestMethod]
        public void TestOpList()
        {
            OpList.Operations["writeline"]("strtoprint");
            Assert.IsTrue((bool)OpList.Operations["exists"](new List<string>() { "abrakadabra", "bra" }));
        }
        [TestMethod]
        public void TestFunctionCall()
        {
            OpList.Operations["writeline"]("strtoprint");
        }
        [TestMethod]
        public void TestInvalidFunctionCall()
        {
            var tokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("\"value here\"")
            };
            var call = new FunctionCallStatement(tokens, _table);
            bool flag = false;
            call.ExceptionThrown += (args) =>
            {
                flag = true;
            };
            call.Execute();
            Assert.IsTrue(flag);
        }
    }
}
