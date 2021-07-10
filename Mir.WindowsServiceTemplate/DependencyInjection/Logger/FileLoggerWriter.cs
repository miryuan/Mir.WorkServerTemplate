using Mir.WindowsServiceTemplate.DependencyInjection.Logger;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging.File
{
    /// <summary>
    /// 
    /// </summary>
    public class FileLoggerWriter
    {
        private readonly string _logDir;
        private ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FileLoggerWriter(FileLoggerOptions options)
        {
            Options = options;
            _logDir = Options.Path ?? Path.Combine(AppContext.BaseDirectory, "logs");
            BeginAsyncQueueWriter();
        }

        /// <summary>
        /// 
        /// </summary>
        public CancellationTokenSource CancellationToken => new CancellationTokenSource();

        /// <summary>
        /// 
        /// </summary>
        public bool LogWriteDone => _queue.Count == 0;

        /// <summary>
        /// 
        /// </summary>
        public FileLoggerOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="name"></param>
        /// <param name="exception"></param>
        public void WriteLine(LogLevel level, string message, string name, Exception exception)
        {
            var logBuilder = new StringBuilder();

            logBuilder.AppendLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fffff}¡¾{level}¡¿");
            logBuilder.AppendLine($"{name}");
            logBuilder.AppendLine($"{message}");
            if (exception != null) logBuilder.AppendLine($"¡¾Exception¡¿:\t{exception}");
            logBuilder.AppendLine();
            _queue.Enqueue(logBuilder.ToString());
        }

        private void BeginAsyncQueueWriter()
        {
            Task.Run(() =>
            {
                CreateLogDir();
                var logBuilder = new StringBuilder();
                while (!CancellationToken.IsCancellationRequested || _queue.Count > 0)
                {
                    logBuilder.Clear();
                    string date = DateTime.Now.ToString("yyyy-MM-dd HH");
                    int nowCount = _queue.Count;

                    if (nowCount == 0)
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    nowCount = nowCount > 30 ? 30 : nowCount;

                    for (int i = 0; i < nowCount; i++)
                    {
                        _queue.TryDequeue(out string log);
                        logBuilder.Append(log);
                    }

                    try
                    {
                        WriteLog(date, logBuilder.ToString());
                    }
                    catch (DirectoryNotFoundException)
                    {
                        CreateLogDir();
                        WriteLog(date, logBuilder.ToString());
                    }
                    catch (Exception) { }
                }
            });
        }

        private void CreateLogDir()
        {
            if (!Directory.Exists(_logDir)) Directory.CreateDirectory(_logDir);
        }

        private void WriteLog(string date, string log)
        {
            System.IO.File.AppendAllText(Path.Combine(_logDir, $"{date}.log"), log);
        }
    }
}