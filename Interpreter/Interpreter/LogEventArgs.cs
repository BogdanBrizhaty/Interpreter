using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter
{
    public class LogEventArgs : EventArgs
    {
        public string LogMessage { get; protected set; }
        public DateTime LogDate { get; private set; }
        public LogEventArgs(string msg)
        {
            LogDate = DateTime.Now;
            LogMessage = msg;
        }
        public override string ToString()
        {
            return String.Format("{ at {0:dd/MM/yyyy H:mm:ss zzz} msg: {1} }", LogDate, LogMessage);
        }
    }
}
