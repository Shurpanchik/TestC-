using Nancy;

namespace WebApi.Api
{
	public abstract class ApiModuleBase : NancyModule
	{
		protected ApiModuleBase(string path) : base($"/api{path}")
		{
		}
	}
}