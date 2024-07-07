using FileService.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Data
{
    public class FileServiceDbContext : IdentityDbContext<ApplicationUser>
    {
        public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}