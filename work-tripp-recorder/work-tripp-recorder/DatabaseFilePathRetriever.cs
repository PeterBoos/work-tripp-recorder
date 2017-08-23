using System;
using System.IO;

namespace work_tripp_recorder
{
    public static class DatabaseFilePathRetriever
    {
        public static string GetPath()
        {
            const string filename = "WTR_SQLite.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);
            return path;
        }
    }
}