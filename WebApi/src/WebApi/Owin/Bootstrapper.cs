using Autofac;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.ModelBinding;
using WebApi.Auth;

namespace WebApi.Owin
{
	public sealed class Bootstrapper : AutofacNancyBootstrapper
	{
		private readonly IContainer _container;

		public Bootstrapper(IContainer container)
		{
			_container = container;

		}

        protected override ILifetimeScope GetApplicationContainer() => _container;

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

            
            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "MyRealm"));
            

            BindingConfig.Default.IgnoreErrors = true;
		}

    }
}