using SQLite;

namespace Fac.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
