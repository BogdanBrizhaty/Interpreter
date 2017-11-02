using Interpreter.Interpreter;
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
                            //var ttt = ;
                            if (obj.GetType() == typeof(List<int>))
                                foreach (var item in (List<int>)obj)
                                    Console.WriteLine(obj.ToString());

                            if (obj.GetType() == typeof(List<string>))
                                foreach (var item in (List<string>)obj)
                                    Console.WriteLine(obj.ToString());
                            else
                                Console.WriteLine(obj.ToString());
                            return 0;
                        }
                    },
                    {"EXISTS", obj =>
                        {
                            var param = (QueryParams)obj;
                            return Regex.IsMatch(param.Source, param.GetLookFor());
                        }
                    },
                    //{"ASC", obj =>
                    //    {
                    //        ((IList<string>)obj).OrderBy(s => s);
                    //        return 0;
                    //    }
                    //},
                    //{"DESC", obj =>
                    //    {
                    //        ((IList<string>)obj).OrderByDescending(s => s);
                    //        return 0;
                    //    }
                    //},
                    {"INDEXOF", obj =>
                        {
                            var param = (QueryParams)obj;
                            return Regex.Matches(param.Source, param.GetLookFor()).Cast<Match>().Min(m => m.Index);
                            //return param.Source.IndexOf(param.GetLookFor());
                        }
                    },
                    {"LASTINDEXOF", obj =>
                        {
                            var param = (QueryParams)obj;
                            return Regex.Matches(param.Source, param.GetLookFor()).Cast<Match>().Max(m => m.Index);
                            //return param.Source.LastIndexOf(param.GetLookFor());
                        }
                    },
                    {"COUNT", obj =>
                        {
                            var param = (QueryParams)obj;
                            return Regex.Matches(param.Source, param.GetLookFor()).Count;
                        }
                    },
                    {"SELECT", obj =>
                        {
                            var param = (QueryParams)obj;
                            return Regex.Matches(param.Source, param.GetLookFor()).Cast<Match>().Select(m => m.Value).ToList();
                        }
                    },
                    {"INDEXES", obj =>
                        {
                            var param = (QueryParams)obj;
                            return Regex.Matches(param.Source, param.GetLookFor()).Cast<Match>().Select(m => m.Index).ToList();
                        }
                    }
                };
                return _ops;
            }
        }
    }
}
