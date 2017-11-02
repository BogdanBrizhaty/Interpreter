using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter
{
    public class QueryParams
    {
        public string Source { get; protected set; }
        public string Template { get; protected set; }
        private string _startsWith;
        private string _endsWith;
        // max or min
        private string _matchSize;

        public QueryParams(string template, string source)
        {
            Template = template;
            Source = source;
        }
        public QueryParams(string template, string source,
            string starts, string ends, string size) : this(template, source)
        {
            _startsWith = starts;
            _endsWith = ends;
            _matchSize = size;
        }
        public string GetLookFor()
        {
            var regex = @"";

            if (_startsWith != "")
            {
                regex += _startsWith;
                if (_matchSize == "MAX")
                    regex += @".*";
                if (_matchSize == "MIN")
                    regex += @".*?";
            }

            regex += Template;

            if (_endsWith != "")
            {
                if (_matchSize == "MAX")
                    regex += @".*";
                if (_matchSize == "MIN")
                    regex += @".*?";
                regex += _endsWith;
            }

            return regex;
        }
        public override bool Equals(object obj)
        {
            var other = (QueryParams)obj;
            return (this.Template.CompareTo(other.Template) +
                this.Source.CompareTo(other.Source) +
                this._startsWith.CompareTo(other._startsWith) +
                this._endsWith.CompareTo(other._endsWith) +
                this._matchSize.CompareTo(other._matchSize)
                ) == 0;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
