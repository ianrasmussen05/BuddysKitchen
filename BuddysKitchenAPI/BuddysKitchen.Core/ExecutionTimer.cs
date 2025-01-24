using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BuddysKitchen.Core
{
    public class ExecutionTimer : IDisposable
    {
        private Stopwatch Timer { get; }
        private string CallerMethod { get; }
        private string CallerClass { get; }
        public string Notes { get; }

        public ExecutionTimer(string className, string notes = "", [CallerMemberName] string methodName = "")
        {
            CallerClass = className;
            Notes = notes;
            CallerMethod = methodName;

            Timer = new Stopwatch();

            Timer.Start();
        }

        public void Dispose()
        {
            Timer.Stop();
            var message = $"{CallerClass}.{CallerMethod} executed in {Timer.ElapsedMilliseconds:N0} ms {Notes}";
            Console.WriteLine(message);
        }
    }
}
