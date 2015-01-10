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

        public static IContainer Configuration
        {
            get
            {
                var builder = new ContainerBuilder();
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                builder.RegisterModule(new Autofac());
                return builder.Build();
            }
        }
    }
}