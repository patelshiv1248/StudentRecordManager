using Microsoft.EntityFrameworkCore;
using StudentRecordMVC.Models;

namespace StudentRecordMVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
}
