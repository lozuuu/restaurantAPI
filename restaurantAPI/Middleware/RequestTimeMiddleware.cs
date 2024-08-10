using System.Diagnostics;
using System.Threading.Tasks;
namespace restaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopwatch;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
           await next.Invoke(context);
            _stopwatch?.Stop();
            var elapsemMiliseconds = _stopwatch.ElapsedMilliseconds;

            if(elapsemMiliseconds / 1000 > 4) 
            { 
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsemMiliseconds} ms";

                _logger.LogInformation(message);
            }
        }

        
    }
}
