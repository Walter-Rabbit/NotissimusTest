using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotissimusTest.Core.Exceptions;
using NotissimusTest.Core.Models;

namespace NotissimusTest.Data;

public class NotissimusTestContext : DbContext
{
    public NotissimusTestContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        ConnectionString = configuration["ConnectionString"] ??
                           throw new ConfigurationException("There is no connection string in configuration.");
        Database.EnsureCreated();
    }

    public string ConnectionString { get; set; }
    public DbSet<Offer> Offers { get; set; }
}