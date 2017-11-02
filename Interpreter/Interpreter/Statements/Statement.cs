using Interpreter.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter.Statements
{
    public abstract class Statement : IStatement
    {
        public event ExceptionThrownEventHandler ExceptionThrown;

        public IList<IStatement> Children { get; set; }
        protected MemoryTable _memory = null;
        protected IList<Token> Tokens { get; set; }
        public Statement(IList<Token> tokens, MemoryTable table)
        {
            Tokens = tokens;
            _memory = table;
        }
        protected virtual void OnExceptionThrown(RuntimeInterpreterExceptionEventArgs args)
        {
            ExceptionThrown?.Invoke(args);
        }
        //public abstract void Execute();
        public virtual void Execute()
        {
            if (this.Children != null)
                foreach (var child in Children)
                    child.Execute();
        }
    }
}
