using System;
using System.IO;
using LiteDB;
using Serilog;

namespace BusinnesLogic.Repository
{
    public class LiteDbBase : IDisposable
    {
        private const string databaseName = "dbLiteDb.db";
        protected readonly LiteDatabase db;
        private bool _disposed = false;

        protected LiteDbBase(string dbPath)
        {
            var databaseFullPath = dbPath ?? GetDatabasePath();

            Log.Debug("<{Method}> database located in <{databasePath}>",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                databaseFullPath);

            db = new LiteDatabase(databaseFullPath);
        }

        #region Public Methods

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                db.Dispose();
            }

            _disposed = true;
        }

        #endregion

        #region Private Methods

        private string GetDatabasePath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            return Path.Combine(documentsPath, databaseName);
        }

        #endregion
    }
}
