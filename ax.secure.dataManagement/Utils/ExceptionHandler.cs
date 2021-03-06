﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ax.secure.dataManagement.Utils
{
    /// <summary>
    /// Exception handler.
    /// </summary>
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke the specified context.
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="context">Context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError); }
        }

        /// <summary>
        /// Handles the exception async.
        /// </summary>
        /// <returns>The exception async.</returns>
        /// <param name="context">Context.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="statusCode">Status code.</param>
        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            _logger.LogError(exception.Message);
            string result = JsonConvert.SerializeObject(new { message = exception.Message, time = DateTime.UtcNow });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
