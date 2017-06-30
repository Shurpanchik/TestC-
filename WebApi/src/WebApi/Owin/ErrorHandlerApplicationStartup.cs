using System;
using Microsoft.Extensions.Logging;
using Nancy.Bootstrapper;

namespace WebApi.Owin
{
	public sealed class ErrorHandlerApplicationStartup : IApplicationStartup
	{
		private readonly ILogger<ErrorHandlerApplicationStartup> _logger;

		public ErrorHandlerApplicationStartup(ILogger<ErrorHandlerApplicationStartup> logger)
		{
			_logger = logger;
		}

		public void Initialize(IPipelines pipelines)
		{
			pipelines.OnError.AddItemToStartOfPipeline((__context, __exception) =>
			{
				_logger.LogError(getMessage(__exception));

				if (__exception is UnauthorizedAccessException)
				{
					return null;
				}

				return new
				{
					ErrorMessage = getMessage(__exception)
				};
			});
		}

		private string getMessage(Exception exception)
		{
			return $">{exception.Message}{Environment.NewLine}" + (exception.InnerException == null ? "" : getMessage(exception.InnerException));
		}
	}
}