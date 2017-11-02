using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Tables;

namespace Interpreter.Interpreter.Statements
{
    public class RootStatement : Statement
    {
        public RootStatement(MemoryTable table) : base(null, table)
        {

        }
        //public void AppendChild(IStatement child)
        //{
        //    this.Children.Add(child);
        //}
    }
}
