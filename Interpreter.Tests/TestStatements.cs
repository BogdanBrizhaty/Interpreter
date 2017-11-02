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
                Token.GetToken("="),
                Token.GetToken("\"value here\"")
            };
            var def = new DefineVariableStatement(tokens, _table);
            def.Execute();

            Assert.IsTrue(_table.Lookup("MyVar1") != null);
        }
        [TestMethod]
        public void TestBoolVariableDefining()
        {
            var tokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("="),
                Token.GetToken("true")
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

        [TestMethod]
        public void TestQueryStatement()
        {
            var defTokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("\"value here\"")
            };
            var def = new DefineVariableStatement(defTokens, _table);
            def.Execute();

            var defTokens2 = new List<Token>()
            {
                Token.GetToken("MyVar2"),
                Token.GetToken("\"value here\"")
            };
            var def2 = new DefineVariableStatement(defTokens2, _table);
            def2.Execute();

            var tokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("="),
                Token.GetToken("COUNT"),
                Token.GetToken("\"template\""),
                Token.GetToken("IN"),
                Token.GetToken("MyVar2")

            };
            var query = new QueryStatement(tokens, _table);
            bool flag = false;
            query.ExceptionThrown += (args) =>
            {
                flag = true;
            };
            query.Execute();
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void TestComplexQueryStatement()
        {
            var defTokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("FALSE")
            };
            var def = new DefineVariableStatement(defTokens, _table);
            //def.Execute();

            var defTokens2 = new List<Token>()
            {
                Token.GetToken("MyVar2"),
                Token.GetToken("\"abrakadabra\"")
            };
            var def2 = new DefineVariableStatement(defTokens2, _table);
            def2.Execute();

            var tokens = new List<Token>()
            {
                Token.GetToken("MyVar1"),
                Token.GetToken("="),
                Token.GetToken("SELECT"),
                Token.GetToken("\"r\""),
                Token.GetToken("IN"),
                Token.GetToken("MyVar2"),
                Token.GetToken("WHERE"),
                Token.GetToken("STARTSWITH"),
                Token.GetToken("\"b\""),
                Token.GetToken("ENDSWITH"),
                Token.GetToken("\"a\""),
                Token.GetToken("MATCH"),
                Token.GetToken("MIN")

            };
            var query = new QueryStatement(tokens, _table);
            bool flag = false;
            query.ExceptionThrown += (args) =>
            {
                flag = true;
            };
            query.Execute();

            tokens = new List<Token>()
            {
                Token.GetToken("WRITELINE"),
                Token.GetToken("MyVar1")
            };
            var call = new FunctionCallStatement(tokens, _table);
            call.Execute();
            Assert.IsFalse(flag);
        }
    }
}
