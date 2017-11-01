using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tables
{
    public enum DataType
    {
        Text,
        Number,
        ResourceList,
        Function
    }
    
    public class MemoryTable : HashSet<Variable>
    {
        private static MemoryTable _instance = null;
        private MemoryTable()
        {
        }
        public static MemoryTable Instance
        {
            get
            {
                _instance = _instance ?? new MemoryTable();
                return _instance;
            }
        }
        public bool Define(Variable var)
        {
            return this.Add(var);
        }
        public Variable Lookup(string name)
        {
            return this.Where(v => v.Name == name).Single();
        }
        public void Update(string name, object value)
        {
            this.Where(v => v.Name == name).Single().Value = value;
        }
    }
}
