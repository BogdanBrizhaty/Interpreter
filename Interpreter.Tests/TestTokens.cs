using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

/*
 * 
 * 
 * 
 */
namespace Interpreter.Tests
{
    [TestClass]
    public class TestTokens
    {
        [TestMethod]
        public void TestSingleSymbolTokenCreation()
        {
            var template = new Token(Token.TokenType.Semicolon, ";");
            var parsed = Token.GetToken(";");

            Assert.AreEqual(template, parsed);
        }
        [TestMethod]
        public void TestTextVariableDefine()
        {
            var expected = new List<Token>()
            {
                Token.GetToken("VAR"),
                Token.GetToken("varname"),
                Token.GetToken("="),
                Token.GetToken("\"valueee u u ueee\""),
                Token.GetToken(";")
            };

            var src = "   var  varname   = \"valueee u u ueee\";";

            var actual = new Parser.Parser().Parse(src).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestNumberVariableDefine()
        {
            var expected = new List<Token>()
            {
                Token.GetToken("VAR"),
                Token.GetToken("varname"),
                Token.GetToken("="),
                Token.GetToken("123"),
                Token.GetToken(";")
            };

            var src = "   var  varname   = 123;";

            var actual = new Parser.Parser().Parse(src).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryAssigning()
        {
            var expected = new List<Token>()
            {
                Token.GetToken("varname"),
                Token.GetToken("="),
                Token.GetToken("EXISTS"),
                Token.GetToken("\"template value\""),
                Token.GetToken("IN"),
                Token.GetToken("\"source string\""),
                Token.GetToken(";")
            };

            var src = "    varname   = exists \"template value\" in \"source string\";";

            var actual = new Parser.Parser().Parse(src).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestQueryWithParameters()
        {
            var expected = new List<Token>()
            {
                Token.GetToken("varname"),
                Token.GetToken("="),
                Token.GetToken("EXISTS"),
                Token.GetToken("\"template value\""),
                Token.GetToken("IN"),
                Token.GetToken("\"source string\""),
                Token.GetToken("WHERE"),
                Token.GetToken("STARTSWITH"),
                Token.GetToken("\"start string\""),
                Token.GetToken("ENDSWITH"),
                Token.GetToken("\"end string\""),
                Token.GetToken(";")
            };

            var src = "varname = exists \"template value\" in \"source string\" where startswith \"start string\" endswith \"end string\";";

            var actual = new Parser.Parser().Parse(src).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSelectionQueryWithParameters()
        {
            var expected = new List<Token>()
            {
                Token.GetToken("varname"),
                Token.GetToken("="),
                Token.GetToken("SELECT"),
                Token.GetToken("\"template value\""),
                Token.GetToken("FROM"),
                Token.GetToken("\"source string\""),
                Token.GetToken("WHERE"),
                Token.GetToken("STARTSWITH"),
                Token.GetToken("\"start string\""),
                Token.GetToken("ENDSWITH"),
                Token.GetToken("\"end string\""),
                Token.GetToken("ASC"),
                Token.GetToken(";")
            };

            var src = " varname = select  \"template value\" from \"source string\" where startswith \"start string\" endswith \"end string\" ASC;";

            var actual = new Parser.Parser().Parse(src).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
