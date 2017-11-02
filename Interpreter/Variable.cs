using Interpreter.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Variable
    {
        public DataType Type { get; protected set; }
        public object Value { get; set; }
        public string Name { get; protected set; }
        public Variable(DataType type, string name, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }
}
