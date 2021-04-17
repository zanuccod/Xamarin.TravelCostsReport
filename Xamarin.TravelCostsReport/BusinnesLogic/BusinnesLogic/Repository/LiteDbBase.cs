using System;
using System.IO;
using LiteDB;

namespace BusinnesLogic.Repository
{
    public class LiteDbBase : IDisposable
    {
        private const string databaseName = "dbLiteDb.db";
        protected readonly LiteDatabase db;

        protected LiteDbBase(string dbPath)
        {
            db = new LiteDatabase(dbPath ?? GetDatabasePath());
        }

        #region Public Methods

        public void Dispose()
        {
            db.Dispose();
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
