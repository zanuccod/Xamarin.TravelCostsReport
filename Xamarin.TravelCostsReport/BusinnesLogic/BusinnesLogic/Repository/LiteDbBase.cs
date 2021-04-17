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

        protected LiteDbBase(string dbPath)
        {
            var databaseFullPath = dbPath ?? GetDatabasePath();

            Log.Debug("<{Method}> database located in <{databasePath}>",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                databaseFullPath);

            db = new LiteDatabase(databaseFullPath);
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
