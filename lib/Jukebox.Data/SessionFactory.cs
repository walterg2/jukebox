using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.FileSystem;

namespace Jukebox.Data
{
    public interface SessionFactory
    {
        Session OpenSession();
    }

    public class RavenSessionFactory : SessionFactory
    {
        private readonly SessionContext _sessionContext;
        private static IDocumentStore _documentStore;
        private readonly IFilesStore _fileStore;

        public RavenSessionFactory(SessionContext sessionContext)
        {
            _sessionContext = sessionContext;
            const string ravenUrl = "http://localhost:8080";
            _documentStore = new DocumentStore { Url = ravenUrl, DefaultDatabase = "Jukebox" }.Initialize();
            _fileStore = new FilesStore { Url = ravenUrl, DefaultFileSystem = "Jukebox-Music" }.Initialize();
        }

        public Session OpenSession()
        {
            return _sessionContext.CurrentSession = new RavenSession(_documentStore.OpenSession(), _fileStore);
        }
    }

    public class SessionContext
    {
        [ThreadStatic]
        private static Session _session;

        public Session CurrentSession
        {
            get { return _session; }
            set { _session = value; }
        }
    }
}
