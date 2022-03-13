using JwtandInterface2.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtandInterface2.Controllers.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<UserDto> UserDtos { get; set; }
        public DbSet<UserInfoDto> UserInfos { get; set; }
        public DbSet<RoleDto> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleDto>().ToTable("Roles");
            modelBuilder.Entity<UserDto>().ToTable("Users");
        }
    }
}
