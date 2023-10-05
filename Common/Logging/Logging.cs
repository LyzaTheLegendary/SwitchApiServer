using System.Text;

namespace Common.Logs {
    public static class Logging {
        private readonly static object errorLock = new();
        public static void LogError(string message) {
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
            lock (errorLock) // important lock, Don't remove!
#pragma warning disable IDE0071 // Simplify interpolation
                using (FileStream stream = File.Open(Path.Combine("logs",$"logs-{DateTime.Now.ToString("y-M-d-H")}.txt"), FileMode.OpenOrCreate))
                    stream.Write(Encoding.UTF8.GetBytes(message));
#pragma warning restore IDE0071 // Simplify interpolation
        }
    }
}
