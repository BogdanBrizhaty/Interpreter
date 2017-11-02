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
        Boolean,
        ResourceList,
        Function
    }
    
    public class MemoryTable
    {
        private HashSet<Variable> _table = null;
        private static MemoryTable _instance = null;
        private MemoryTable()
        {
            _table = new HashSet<Variable>();
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
            return _table.Add(var);
        }
        public Variable Lookup(string name)
        {
            return _table.Where(v => v.Name == name).Single();
        }
        public void Update(string name, object value)
        {
            _table.Where(v => v.Name == name).Single().Value = value;
        }
        public void Clear()
        {
            this._table.Clear();
        }
    }
}
