using MVCStartup.Models.DB.Entities;
using MVCStartup.Models.DB.Repositories;

namespace MVCStartup.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate nextSet)
        {
            _next = nextSet;
        }

        public async Task InvokeAsync(HttpContext context, IRequestRepository repo)
        {
            string logMessage = $"http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            LogConsole(logMessage);

            await LogFile(logMessage);

            await LogDb(logMessage, repo);

            //Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }

        /// <summary>
        /// Логгирование в консоль
        /// </summary>
        private void LogConsole(string logMessage)
        {
            Console.WriteLine($"[{DateTime.Now}]: New request to {logMessage}");
        }

        /// <summary>
        /// Логгирование в файл
        /// </summary>
        private async Task LogFile(string logMessage)
        {
            string logFilePath = Path.Combine("Logs", "RequestLog.txt");

            await File.AppendAllTextAsync(logFilePath, $"[{DateTime.Now}]: {logMessage}");
        }

        /// <summary>
        /// Логгирование в базу данных
        /// </summary>
        private async Task LogDb(string logMessage, IRequestRepository repo)
        {
            var newReqest = new Request()
            {
                Url = logMessage
            };

            await repo.AddRequest(newReqest);
        }
    }
}
