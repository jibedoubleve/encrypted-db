using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace EncryptedDb;

public static class PrintHelper
{
    public static void Print(this SqliteConnectionStringBuilder builder)
    {
        var connectionString = builder.ToString();
        AnsiConsole.MarkupLine($"Connection string: [yellow]{connectionString}[/]");
    }
    public static void Print(this IEnumerable<User> users)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var user in users)
        {
            table.AddRow($"{user.Id}", user.Name);
        }
    
        AnsiConsole.Write(table);
    }
}