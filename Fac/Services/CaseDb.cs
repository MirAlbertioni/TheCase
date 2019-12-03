using Fac.Models;
using Fac.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Fac.Services
{
    public class CaseDb
    {
        private SQLiteConnection conn;

        //CREATE  
        public CaseDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<CaseSummary>();
        }

        public IEnumerable<CaseSummary> GetCasesFromDb()
        {
            var members = (from mem in conn.Table<CaseSummary>() select mem);
            return members.ToList();
        }

        public void AddCasesToDb(List<CaseSummary> items)
        {
            conn.InsertAll(items);
        }

        public void AddCaseTodb(CaseSummary member)
        {
            conn.Insert(member);
        }

    }
}
