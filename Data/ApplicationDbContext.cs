using Microsoft.EntityFrameworkCore;
using associatetool.Model;

namespace associatetool.Data{

    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

        }
        public DbSet<Tag_Address> Tag_Address{ get; set; } 
        //public DbSet<ToolTag> ToolTag{ get; set; } 
        
    }
}