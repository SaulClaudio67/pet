var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("/api/health/live", () => Results.Ok(new { status = "live" }));
app.MapGet("/api/health/ready", async (IConfiguration configuration, CancellationToken cancellationToken) =>
{
    var connectionString = configuration.GetConnectionString("Postgres");
    if (string.IsNullOrWhiteSpace(connectionString)) return Results.Problem(title: "Database is not configured", statusCode: StatusCodes.Status503ServiceUnavailable);
    try
    {
        await using var connection = new Npgsql.NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new Npgsql.NpgsqlCommand("SELECT 1", connection);
        await command.ExecuteScalarAsync(cancellationToken);
        return Results.Ok(new { status = "ready" });
    }
    catch (Npgsql.NpgsqlException) { return Results.Problem(title: "Database is unavailable", statusCode: StatusCodes.Status503ServiceUnavailable); }
});
app.MapMethods("/api/{**path}", ["GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS", "HEAD"], () => Results.NotFound());
app.MapFallbackToFile("index.html");
app.Run();
public partial class Program { }
