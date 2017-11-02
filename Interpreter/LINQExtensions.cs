using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public static class LINQExtensions
    {
        public static string ConvertIntoLine(this IEnumerable<Token> source)
        {
            return String.Join(" ", source.Select(t => t.Value).ToList());
        }
    }
}
