using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Middleware;

namespace Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}