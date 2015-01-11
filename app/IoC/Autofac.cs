using Autofac;
using Autofac.Features.ResolveAnything;
using Jukebox.Data;

namespace Jukebox.IoC
{
    public class Autofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RavenSessionFactory>().As<SessionFactory>().SingleInstance();
        }

        private static IContainer _configuration;
        public static IContainer Configuration
        {
            get
            {
                if (null != _configuration)
                    return _configuration;

                var builder = new ContainerBuilder();
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                builder.RegisterModule(new Autofac());
                return _configuration = builder.Build();
            }
        }
    }
}