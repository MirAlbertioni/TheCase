using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fac.iOS;
using Fac.Services;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSSqLite))]
namespace Fac.iOS
{
    public class IOSSqLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "Cases.sqlite";
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder  
            string libraryPath = Path.Combine(dbPath, "..", "Library"); // Library folder  
            var path = Path.Combine(libraryPath, dbName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}