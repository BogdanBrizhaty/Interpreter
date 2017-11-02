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
            Children = new List<IStatement>();
            this.ExceptionThrown += Output.Log;
        }
        //public Statement(IList<Token> tokens, MemoryTable table)
        //{
        //    Tokens = tokens;
        //    _memory = table;
        //}
        protected virtual void OnExceptionThrown(RuntimeInterpreterExceptionEventArgs args)
        {
            ExceptionThrown?.Invoke(args);
            //this.Execute();
            return;
        }
        //public abstract void Execute();
        public virtual void Execute()
        {
            if (this.Children != null)
                foreach (var child in Children)
                    child.Execute();
        }
        public override bool Equals(object obj)
        {
            var th = this.Children.Select(c => c.GetType()).ToList();
            var oth = ((Statement)obj).Children.Select(c => c.GetType()).ToList();
            return th.SequenceEqual(oth);
            //return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
