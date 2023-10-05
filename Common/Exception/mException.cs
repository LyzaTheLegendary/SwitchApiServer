using Common.Logs;
using System.Diagnostics;
using System.Reflection;

namespace Common.Exceptions {
    /// <summary>
    /// This exception immediately logs / notifies ( when appropiate ) when this exception is called
    /// </summary>
#pragma warning disable IDE1006 // Naming Styles
    public class mException : Exception {
#pragma warning restore IDE1006 // Naming Styles
        public mException(string? message) : base(message) {
            StackTrace stacktrace = new(1, true);
            StackFrame? stackframe = stacktrace.GetFrame(1);

            if (stackframe == null) {
                // handle unknown stack frame here?
                // Important? Probably anonymous function or deeply scoped multithreaded execution!
                return;
            }
            TimeSpan span = (DateTime.Now - DateTime.Now);

            string fError = $"[{span.Hours}:{span.Minutes}:{span.Seconds}]";
            MethodBase? method = stackframe.GetMethod();

            if (method.ReflectedType == null || method == null) {
                fError += " unknownMethod";
            }
            else {
                fError += $" {method.ReflectedType.Name}::{method.Name}() line: [{stackframe.GetFileLineNumber()}]";
            }
            fError += "\r\n";
            DisplayMessage(fError);
            LogError(fError);
        }

        public virtual void DisplayMessage(string fError) {
            Console.WriteLine(fError);// TODO: use TUI to log errors and exceptions
        }
        public virtual void LogError(string fError) 
            => Logging.LogError(fError);
        
    }
}
