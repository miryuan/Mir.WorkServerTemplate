using Mir.WindowsServiceTemplate.DependencyInjection.Logger;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging.File
{
    /// <summary>
    /// 
    /// </summary>
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerWriter _fileLoggerWriter;
        private readonly FileLoggerOptions _options = new FileLoggerOptions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FileLoggerProvider(FileLoggerOptions options)
        {
            _options = options;
            _fileLoggerWriter = new FileLoggerWriter(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string name)
        {
            return new FileLogger(name, _fileLoggerWriter);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _fileLoggerWriter.CancellationToken.Cancel();
            while (!_fileLoggerWriter.LogWriteDone)
            {
                Task.Delay(100).Wait();
            }
        }
    }
}