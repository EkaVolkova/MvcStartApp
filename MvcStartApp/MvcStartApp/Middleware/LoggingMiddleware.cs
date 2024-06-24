using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MvcStartApp.Models.Db;
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
        private readonly IRequestRepository _requestRepository;

        /// <summary>
        /// конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env, IBlogRepository blogRepository, IRequestRepository requestRepository)
        {
            _next = next;
            _env = env;
            _blogRepository = blogRepository;
            _requestRepository = requestRepository;
        }

        /// <summary>
        /// Функция логирования в консоль
        /// </summary>
        private void LogConsole(Request request)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{request.Date}]: New request to http://{request.Url}";
            Console.WriteLine(logMessage);

        }

        /// <summary>
        /// Функция логирования в файл
        /// </summary>
        private async Task LogFile(Request request)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{request.Date}]: New request to http://{request.Url}\r\n";
            string logDir = Directory.GetCurrentDirectory() + "\\Logs";
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

            await File.AppendAllTextAsync(logFilePath, logMessage);
        }

        /// <summary>
        /// Функция логирования в БД
        /// </summary>
        private async Task LogDb(Request request)
        {

            await _requestRepository.AddRequest(request);
        }

        /// <summary>
        /// Задача логирования
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var request = new Request()
            {
                Id = new Guid(),
                Date = DateTime.Now,
                Url = $"http://{context.Request.Host.Value + context.Request.Path}"
            };

            LogConsole(request);
            await LogFile(request);
            await LogDb(request);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }

    }
}
