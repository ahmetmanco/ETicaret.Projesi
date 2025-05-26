using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting;

namespace _04_Persistence.Layer.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=mydatabase; Username=myuser; Password=mypass");

            return new AppDbContext(optionsBuilder.Options);

            //DI (Dependency Injection) sistemi çalışamadığında ortaya çıkar. Projeye bu classı ve içindeki kodları eklediğimizde sorun çözüldü, migration işlemi başarılı bir şekilde oluştu
        }
    }
}
