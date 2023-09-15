using tetris_api;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Authentication;
using WebApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
                "BasicAuthentication", null);
        builder.Services.AddScoped<IUserService, UserService>();
        // Learn more about configuring Swagger/OpenAPI at
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<UserContext>(options =>
        {
            options
                .UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
                .ReplaceService<IHistoryRepository, CamelCaseHistoryContext>()
                .UseSnakeCaseNamingConvention();
        });

        builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "CorsPolicy", builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            }
        );

        var app = builder.Build();

        app.UseCors("CorsPolicy");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

public class CamelCaseHistoryContext : NpgsqlHistoryRepository
{
    public CamelCaseHistoryContext(HistoryRepositoryDependencies dependencies)
        : base(dependencies) { }

    protected override void
    ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        base.ConfigureTable(history);

        history.Property(h => h.MigrationId).HasColumnName("MigrationId");
        history.Property(h => h.ProductVersion).HasColumnName("ProductVersion");
    }
}
