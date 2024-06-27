using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;
using System.Data;

const string connecctionstring = "Data Source=LAPTOP-563RGJKO\\sqlexpress;Initial Catalog=balta;Integrated Security=True;TrustServerCertificate=True";




using (var connection = new SqlConnection(connecctionstring))
{
    //UpdateCategory(connection);
    //CreateManyCategories(connection);
    //LisCategories(connection);
    //CreateCategory(connection);
    //ExecuteProcedure(connection);
    //ExecuteReadProcedure(connection);
    ExecuteScalar(connection);
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

static void CreateManyCategories(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Description = "Categoria destinada a serviços da AWS";
    category.Order = 8;
    category.Summary = "AWS Cloud";
    category.Featured = false;

    var category2 = new Category();
    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria Nova";
    category2.Url = "categoria nova";
    category2.Description = "Categoria nova";
    category2.Order = 9;
    category2.Summary = "Categoria";
    category2.Featured = true;

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

    var rows = connection.Execute(insertSql, new[]
    {
        new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },
        new
        {
            category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        }
    });

    Console.WriteLine($"{rows} linhas inseridas");
}

// Procedure que ñ retorna informações por enquanto
static void ExecuteProcedure(SqlConnection connection)
{
    var procedure = "[spDeleteStudent]"; 

    var pars = new { StudentId = "584F93A9-E7CE-45E6-92FB-A2F8C3259E55" };

    // Onde inicia a Procedure
    var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

    Console.WriteLine($"{affectedRows} linhas afetadas");

}

// Procedure de leitura
static void ExecuteReadProcedure(SqlConnection connection)
{
    var procedure = "[spGetCoursesByCategory]";

    var pars = new { CategoryId = "5B4CE823-CD82-4F2F-81E9-FEE34A62902E" };

    // Array dynamic
    var courses = connection.Query<Category>(
        procedure, 
        pars, 
        commandType: CommandType.StoredProcedure);

    foreach ( var item in courses)
    {
        Console.WriteLine(item.Id);
    }
}

static void ExecuteScalar(SqlConnection connection)
{
    var category = new Category();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Description = "Categoria destinada a serviços da AWS";
    category.Order = 8;
    category.Summary = "AWS Cloud";
    category.Featured = false;

    var insertSql = @"INSERT INTO 
                            [Category] 
                      OUTPUT inserted.[Id]
                      VALUES(
                            NEWID(),
                            @Title, 
                            @Url, 
                            @Summary,
                            @Order, 
                            @Description, 
                            @Featured
    )";

    var id = connection.ExecuteScalar<Guid>(insertSql, new
    {
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });

    Console.WriteLine($"A categoria inserida foi: {id}");
}

