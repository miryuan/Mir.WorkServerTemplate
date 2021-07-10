using Microsoft.Extensions.Logging;
using System;

namespace Mir.WindowsServiceTemplate.DependencyInjection.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class FileLoggerOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Func<string, LogLevel, bool> Filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }
    }
}