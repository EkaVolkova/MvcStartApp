using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MvcStartApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ASPNetCore.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogRepository _blogRepository;

        /// <summary>
        /// конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env, IBlogRepository repo)
        {
            _next = next;
            _env = env;
            _blogRepository = repo;
        }

        /// <summary>
        /// Функция логирования в консоль
        /// </summary>
        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}";
            Console.WriteLine(logMessage);

        }

        /// <summary>
        /// Функция логирования в файл
        /// </summary>
        private async Task LogFile(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}\r\n";
            string logDir = Directory.GetCurrentDirectory() + "\\Logs";
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

            await File.AppendAllTextAsync(logFilePath, logMessage);
        }

        /// <summary>
        /// Задача логирования
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {

            LogConsole(context);
            await LogFile(context);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }

    }
}
