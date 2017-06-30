using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses.Negotiation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi.Owin
{
	public sealed class JsonResponseProcessor : IResponseProcessor
	{
		private const string MediaType = "application/json";

		private readonly JsonSerializerSettings _camelCasejsonSerializerSettings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};

		public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings { get; } = new[] { new Tuple<string, MediaRange>("json", MediaType) };

		public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
		{
			var canProcess = new ProcessorMatch
			{
				ModelResult = MatchResult.NonExactMatch,
				RequestedContentTypeResult = requestedMediaRange.Matches(MediaType) ? MatchResult.ExactMatch : MatchResult.NoMatch
			};

			return canProcess;
		}

		public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
		{
			var response = (Response)JsonConvert.SerializeObject(model, Formatting.Indented, _camelCasejsonSerializerSettings);

			return response
				.WithContentType(MediaType)
				.WithStatusCode(HttpStatusCode.OK);
		}
	}
}