using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;

const string connecctionstring = "Data Source=LAPTOP-563RGJKO\\sqlexpress;Initial Catalog=balta;Integrated Security=True;TrustServerCertificate=True";


var category = new Category();
category.Id = Guid.NewGuid();
category.Title = "Amazon AWS";
category.Url = "amazon";
category.Description = "Categoria destinada a serviços da AWS";
category.Order = 8;
category.Summary = "AWS Cloud";
category.Featured = false;

var insertSql = @"INSERT INTO 
        [Category] 
    VALUES(
        @Id,
        @Title, 
        @Url, 
        @Summary,
        @Order, 
        @Description, 
        @Featured)";


using (var connection = new SqlConnection(connecctionstring))
{
    var rows = connection.Execute(insertSql, new { 
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });
    Console.WriteLine($"{rows} linhas inseridas");

    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
};