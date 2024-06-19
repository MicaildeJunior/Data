using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;

const string connecctionstring = "Data Source=LAPTOP-563RGJKO\\sqlexpress;Initial Catalog=balta;Integrated Security=True;TrustServerCertificate=True";


var category = new Category();

var insertSql = "INSERT INTO [Category] VALUES(id, title, url, summary, order, description, featured)";


using (var connection = new SqlConnection(connecctionstring))
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
};