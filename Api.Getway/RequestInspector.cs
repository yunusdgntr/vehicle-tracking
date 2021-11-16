using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Api.Gateway
{
    public class RequestInspector
    {
        private readonly RequestDelegate _next;

        public RequestInspector(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Yunus***"+context.Request.Path);
            await _next(context);
        }
    }
}
