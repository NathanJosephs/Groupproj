using Microsoft.EntityFrameworkCore;



namespace TekkenClub.Models
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<RegisterModel> RegisterModel { get; set; }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
        //this.EnsureSeedData();
        Database.EnsureCreated();
    }
}

}
