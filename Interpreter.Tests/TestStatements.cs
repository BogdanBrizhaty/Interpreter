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
            OpList.Operations["WRITELINE"]("strtoprint");
            Assert.IsTrue((bool)OpList.Operations["EXISTS"](new List<string>() { "abrakadabra", "bra" }));
        }
        [TestMethod]
        public void TestFunctionCall()
        {
            var tokens = new List<Token>()
            {
                Token.GetToken("WRITELINE"),
                Token.GetToken("\"value to print\"")
            };
            var call = new FunctionCallStatement(tokens, _table);
            bool flag = false;
            call.ExceptionThrown += (args) =>
            {
                flag = true;
            };
            call.Execute();
            Assert.IsFalse(flag);
        }
        [TestMethod]
        public void TestFunctionWithVariableParameterCall()
        {
            var tokens = new List<Token>()
            {
                Token.GetToken("WRITELINE"),
                Token.GetToken("\"value to print\"")
            };
            var call = new FunctionCallStatement(tokens, _table);
            bool flag = false;
            call.ExceptionThrown += (args) =>
            {
                flag = true;
            };
            call.Execute();
            Assert.IsFalse(flag);
        }
        [TestMethod]
        public void TestInvalidFunctionCall()
        {
            var defTokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("\"value here\"")
            };
            var def = new DefineVariableStatement(defTokens, _table);
            def.Execute();

            var tokens = new List<Token>()
            {
                Token.GetToken("WRITELINE"),
                Token.GetToken("MyVar1")
            };
            var call = new FunctionCallStatement(tokens, _table);
            bool flag = false;
            call.ExceptionThrown += (args) =>
            {
                flag = true;
            };
            call.Execute();
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void TestInvalidParameterFunctionCall()
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
