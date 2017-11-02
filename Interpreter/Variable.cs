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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            var other = (Variable)obj;
            return (this.Type.CompareTo(other.Type) +
                this.Name.CompareTo(other.Name)) == 0 &&
                this.Value == other.Value;
            //return base.Equals(obj);MyBoolVar
        }
    }
}
