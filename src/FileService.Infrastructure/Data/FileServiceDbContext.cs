using FileService.Domain.Entities;
using FileService.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Data;

public class FileServiceDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<App> Apps { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Domain.Entities.File> Files { get; set; }
}