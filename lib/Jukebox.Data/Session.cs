using System;
using Raven.Client;
using Raven.Client.FileSystem;
using Raven.Client.Linq;

namespace Jukebox.Data
{
    public interface Session : IDisposable
    {
        void Store<TModel>(TModel model);
        TModel Find<TModel>(string id);
        void SaveChanges();
        void Delete(object model);
        IRavenQueryable<TModel> Query<TModel>();
    }

    class RavenSession : Session
    {
        private readonly IDocumentSession _session;
        private readonly IFilesStore _fileStore;

        public RavenSession(IDocumentSession session, IFilesStore fileStore)
        {
            _session = session;
            _fileStore = fileStore;
        }

        public void Dispose()
        {
            using (_session) { }
        }

        public void Store<TModel>(TModel model)
        {
            _session.Store(model);
        }

        public TModel Find<TModel>(string id)
        {
            return _session.Load<TModel>(id);
        }

        public void SaveChanges()
        {
            _session.SaveChanges();
        }

        public void Delete(object model)
        {
            _session.Delete(model);
        }

        public IRavenQueryable<TModel> Query<TModel>()
        {
            return _session.Query<TModel>();
        }
    }
}