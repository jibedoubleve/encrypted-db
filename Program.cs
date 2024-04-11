using EncryptedDb;
using Microsoft.Data.Sqlite;
using Spectre.Console;

var figlet = new FigletText("ENCRYPTED SQLITE")
             .LeftJustified()
             .Color(Color.Red);
AnsiConsole.Write(figlet);

const string database = "/Users/jbwautier/Documents/Projects/_test/encrypted-db/database.sqlite";
const string password = "password";
AnsiConsole.MarkupLine($"Connect to database [Red]{database}[/]");

try
{
    if(File.Exists(database))File.Delete(database);
    var connectionString = new SqliteConnectionStringBuilder()
    {
        DataSource = database,
        Mode = SqliteOpenMode.ReadWriteCreate, 
        Password = password
    };
        
    connectionString.Print();

    using (var db = new EncryptedDatabase(connectionString.ToString()))
    {
        db.BuildDatabase()
          .GetUsers()
          .Print();
        
        db.ChangePassword("azerty");
        db.BuildDatabase()
          .GetUsers()
          .Print();
    }

    using (var db = new EncryptedDatabase(connectionString.ToString()))
    {
        db.GetUsers()
          .Print();
    }
    
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex);
    throw;
}
