using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter.Interpreter;
using Interpreter.Interpreter.Statements;
using Interpreter.Tables;
using System.Collections;

namespace Interpreter.Tests
{
    /// <summary>
    /// Summary description for InstructionTreeBuilderTests
    /// </summary>
    [TestClass]
    public class InstructionTreeBuilderTests
    {
        IList<Token> tokens = null;
        public InstructionTreeBuilderTests()
        {
            tokens = new List<Token>()
            {
                Token.GetToken("VAR"),
                Token.GetToken("MyVar1"),
                Token.GetToken("="),
                Token.GetToken("\"abrakadabra\""),
                Token.GetToken(";"),

                Token.GetToken("WRITELINE"),
                Token.GetToken("\"MyVar1 value: \""),
                Token.GetToken(";"),

                Token.GetToken("WRITELINE"),
                Token.GetToken("MyVar1"),
                Token.GetToken(";"),

                Token.GetToken("VAR"),
                Token.GetToken("MyBoolVar"),
                Token.GetToken("="),
                Token.GetToken("TRUE"),
                Token.GetToken(";"),

                Token.GetToken("MyNotPredefinedVar1"),
                Token.GetToken("="),
                Token.GetToken("INDEXES"),
                Token.GetToken("\"r\""),
                Token.GetToken("IN"),
                Token.GetToken("MyVar1"),
                Token.GetToken("WHERE"),
                Token.GetToken("STARTSWITH"),
                Token.GetToken("\"b\""),
                Token.GetToken("ENDSWITH"),
                Token.GetToken("\"a\""),
                Token.GetToken("MATCH"),
                Token.GetToken("MIN"),
                Token.GetToken(";"),

                Token.GetToken("WRITELINE"),
                Token.GetToken("MyNotPredefinedVar1"),
                Token.GetToken(";"),
            };
        }
        [TestMethod]
        public void TestSimpleBuild()
        {
            var preBuildedTree = new RootStatement(MemoryTable.Instance);
            preBuildedTree.Children.Add(new DefineVariableStatement(new List<Token>(), MemoryTable.Instance));
            preBuildedTree.Children.Add(new FunctionCallStatement(new List<Token>(), MemoryTable.Instance));
            preBuildedTree.Children.Add(new FunctionCallStatement(new List<Token>(), MemoryTable.Instance));
            preBuildedTree.Children.Add(new DefineVariableStatement(new List<Token>(), MemoryTable.Instance));
            preBuildedTree.Children.Add(new QueryStatement(new List<Token>(), MemoryTable.Instance));
            preBuildedTree.Children.Add(new FunctionCallStatement(new List<Token>(), MemoryTable.Instance));

            var builded = InstructionTreeBuilder.Build(tokens);

            Assert.AreEqual(preBuildedTree, builded);
        }
        [TestMethod]
        public void TestExecutionBuild()
        {
            var table = MemoryTable.Instance;
            var builded = InstructionTreeBuilder.Build(tokens);
            builded.Execute();
            var expected = new Variable(DataType.Boolean, "MyBoolVar", true);
            var actual = table.Lookup("MyBoolVar");

            Assert.AreEqual(actual.Name, expected.Name);
        }
    }

}
