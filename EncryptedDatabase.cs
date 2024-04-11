using Dapper;
using Microsoft.Data.Sqlite;

namespace EncryptedDb;

public class EncryptedDatabase : IDisposable, IAsyncDisposable
{
    private readonly SqliteConnection _connection;

    public EncryptedDatabase(string connectionString)
    {
        _connection =new SqliteConnection(connectionString);
    }

    public void ChangePassword(string newPassword)
    {
       var quotedNewPassword= _connection.ExecuteScalar("select quote(@newPassword)", new { newPassword});
        _connection.Execute("PRAGMA rekey = " + quotedNewPassword);
    }
    public EncryptedDatabase BuildDatabase()
    {
        const string ddl = """
                           drop table if exists User;
                           create table User(
                            id   integer primary key AUTOINCREMENT,
                            name text
                           );
                           insert into User(name) values('Robert');
                           insert into User(name) values('Albert');
                           insert into User(name) values('Lucie');
                           insert into User(name) values('Bernadette');
                           """;
        _connection.Execute(ddl);
        return this;
    }

    public IEnumerable<User> GetUsers()
    {
        const string sql = "select id, name from User";
        return _connection.Query<User>(sql);
    }

    public void Dispose() { _connection.Dispose(); }
    public async ValueTask DisposeAsync() { await _connection.DisposeAsync(); }
}