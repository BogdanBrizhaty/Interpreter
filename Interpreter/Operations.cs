using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interpreter
{
    public delegate object Operation(object parameter);
    public class OpList : Dictionary<string, Operation>
    {
        private static Dictionary<string, Operation> _ops = null;
        public static Dictionary<string, Operation> Operations
        {
            get
            {
                _ops = _ops ?? new Dictionary<string, Operation>()
                {
                    {"WRITELINE", obj =>
                        {
                            //var type = obj.GetType();
                            var @string = (string)obj;
                            Console.WriteLine(@string);
                            return 0;
                        }
                    },
                    {"EXISTS", obj =>
                        {
                            var source = ((IList<string>)obj).First();
                            var lookFor = ((IList<string>)obj).Last();
                            return Regex.IsMatch(source, lookFor);
                        }

                    }
                };
                return _ops;
            }
        }
    }
}
