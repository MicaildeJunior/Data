﻿using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;

const string connecctionstring = "Data Source=LAPTOP-563RGJKO\\sqlexpress;Initial Catalog=balta;Integrated Security=True;TrustServerCertificate=True";




using (var connection = new SqlConnection(connecctionstring))
{
    UpdateCategory(connection);
    LisCategories(connection);
    //CreateCategory(connection);

};

static void LisCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
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
                            @Featured
    )";

    var rows = connection.Execute(insertSql, new
    {
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });
    Console.WriteLine($"{rows} linhas inseridas");

}

static void UpdateCategory(SqlConnection connection)
{
    var updaterCategory = @"UPDATE 
                                [Category] 
                            SET 
                                [Title] = @Title 
                            WHERE [ID] = @id
    ";

    var rows = connection.Execute(updaterCategory, new
    {
        id = new Guid("AF3407AA-11AE-4621-A2EF-2028B85507C4"),
        title = "Frontend 2024"

    });

    Console.WriteLine($"{rows} registros atualizados");
}
        