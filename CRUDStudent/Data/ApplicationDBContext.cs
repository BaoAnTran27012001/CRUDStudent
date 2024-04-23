using CRUDStudent.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDStudent.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) {

        }
        public DbSet<Student> Students { get; set; }
    }
}
