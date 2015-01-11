using System;
using System.Collections.Generic;
using System.IO;
using Raven.Client;
using Raven.Client.FileSystem;
using Raven.Client.Linq;
using Raven.Json.Linq;

namespace Jukebox.Data
{
    public interface Session : IDisposable
    {
        void Store<TModel>(TModel model);
        TModel Find<TModel>(string id);
        void SaveChanges();
        void Delete(object model);
        IRavenQueryable<TModel> Query<TModel>();
        string AddAttachment(string id, string filename, Stream attachment, params KeyValuePair<string, object>[] metadata);
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

        public string AddAttachment(string id, string filename, Stream attachment, params KeyValuePair<string, object>[] metadata)
        {
            using (var fileSession = _fileStore.OpenAsyncSession())
            {
                var ravenMetadata = new RavenJObject();
                foreach (var x in metadata)
                {
                    ravenMetadata.Add(x.Key, RavenJToken.FromObject(x.Value));
                }

                var path = string.Format("{0}/{1}", id, filename);
                fileSession.RegisterUpload(path, attachment, ravenMetadata);
                var task = fileSession.SaveChangesAsync();
                task.Wait();

                if (task.IsFaulted)
                    throw task.Exception;

                return path;
            }
        }
    }
}