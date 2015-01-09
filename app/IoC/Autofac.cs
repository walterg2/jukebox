using Autofac;
using Autofac.Features.ResolveAnything;
using Jukebox.Data;
using Jukebox.Data.Repositories;
using Jukebox.Data.Repositories.Impl;

namespace Jukebox.IoC
{
    public class Autofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // NhDataModule equivalent
            builder.RegisterType<RavenSessionFactory>().As<SessionFactory>().SingleInstance();
            builder.RegisterType<ArtistRepositoryImpl>().As<ArtistRepository>();
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