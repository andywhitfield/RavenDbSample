using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Imports.Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Reflection;

namespace RavenDbSample.App_Start
{
    public class RavenDBNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDocumentStore>()
                .ToMethod(context =>
                {
                    Trace.TraceInformation("Creating new RavenDB document store...");
                    var documentStore = new EmbeddableDocumentStore { DataDirectory = "~/App_Data/RavenDb", UseEmbeddedHttpServer = false };
                    documentStore.Initialize();
                    documentStore.Conventions.JsonContractResolver = new DefaultContractResolver(true)
                    {
                        DefaultMembersSearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                    };
                    Trace.TraceInformation("Successfully created RavenDB document store in the App_Data/RavenDb directory.");
                    return documentStore;
                })
                .InSingletonScope();

            Bind<IDocumentSession>()
                .ToMethod(context => context.Kernel.Get<IDocumentStore>().OpenSession())
                .InRequestScope()
                .OnDeactivation((context, session) =>
                {
                    Trace.TraceInformation("Request completed, saving RavenDb session changes.");
                    session.SaveChanges();
                    session.Dispose();
                });
        }
    }
}