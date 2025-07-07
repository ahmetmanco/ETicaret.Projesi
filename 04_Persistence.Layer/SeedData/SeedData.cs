using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Domain.Layer.Entities;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using _04_Persistence.Layer.Context;
using _01_Domain.Layer.Enums;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace _03_Infrastructure.Layer.SeedData
{
    public static class SeedData
    {
        public async static Task Seed(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                AppDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                await SeedRoles(roleManager);
                await SeedAdminUser(userManager);
                //await SeedTestProducts(dbContext);
            }
        }
        private static async Task SeedRoles(RoleManager<AppRole> roleManager)
        {
            var roles = new[] { "Admin", "Customer", "Seller" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new AppRole(role));

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Role '{role}' creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        private static async Task SeedAdminUser(UserManager<AppUser> userManager)
        {
            if(await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = false,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin123."),
                    PhoneNumberConfirmed = false,
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    NormalizedEmail = "ADMIN@GMAİL.COM",
                    NormalizedUserName = "ADMIN@GMAİL.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    TwoFactorEnabled = false,
                    AddressId = null,
                    Address = null,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Orders = new List<Order>(),
                    Products = new List<Product>(),
                    Cinsiyet = null,
                    BirthDate = null,
                    
                };
                var result = await userManager.CreateAsync(user, "Admin123.");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        //private static async Task SeedTestProducts(AppDbContext dbContext)
        //{
        //    if (!await dbContext.Products.AnyAsync())
        //    {
        //        await dbContext.Products.AddRangeAsync(
        //            new Product { Name = "Test Ürün 1", Price = 100 },
        //            new Product { Name = "Test Ürün 2", Price = 200 }
        //        );

        //        await dbContext.SaveChangesAsync();
        //    }
        //}
        #region

        //        if (!context.Users.Any())
        //        {


        //            var adminId = Guid.NewGuid();
        //            var passwordHasheR = new PasswordHasher<AppUser>();

        //            AppUser admin = new AppUser
        //            {
        //                Unvan = "Admin",
        //                FirstName = "Admin",
        //                LastName = "Admin",
        //                UserName = "Admin",
        //                NormalizedUserName = "ADMIN",
        //                Email = "Admin@gmail.com",
        //                NormalizedEmail = "ADMIN@GMAIL.COM",
        //                ImagePath = "/images/noImage.png",
        //                CreatedDate = DateTime.Now,
        //                EmailConfirmed = true,
        //                LockoutEnabled = true,
        //                TwoFactorEnabled = false,
        //                PhoneNumberConfirmed = false,
        //                SecurityStamp = Guid.NewGuid().ToString("D"),
        //                HospitalId = 1,
        //                Rol = Rol.Admin,
        //                RolId = 2
        //            };

        //            var hashedAdmin = passwordHasheR.HashPassword(admin, "Test123.");
        //            admin.PasswordHash = hashedAdmin;

        //            var AdminStore = new UserStore<AppUser, IdentityRole<Guid>, AppDbContext, Guid>(context);
        //            await AdminStore.CreateAsync(admin);
        //            await AdminStore.AddToRoleAsync(admin, "ADMIN");
        //            await context.SaveChangesAsync();


        //            var hemsireId = Guid.NewGuid();
        //            var passwordHashHemsire = new PasswordHasher<AppUser>();
        //            AppUser hemsire = new AppUser
        //            {

        //                Unvan = "Hemsire",
        //                HospitalId = hospital.Id,
        //                FirstName = "Hemsire",
        //                LastName = "hemsire",
        //                UserName = "Hemsire",
        //                NormalizedUserName = "HEMSIRE",
        //                Email = "hemsire@gmail.com",
        //                NormalizedEmail = "HEMSIRE@GMAIL.COM",
        //                ImagePath = "/images/noImage.png",
        //                CreatedDate = DateTime.Now,
        //                EmailConfirmed = true,
        //                LockoutEnabled = true,
        //                TwoFactorEnabled = false,
        //                PhoneNumberConfirmed = false,
        //                SecurityStamp = Guid.NewGuid().ToString("D"),
        //                Rol = Rol.Hemsire,
        //                RolId = 4
        //                HospitalId = 1
        //            };

        //            var hashemsire = passwordHashHemsire.HashPassword(hemsire, "Test123.");
        //            hemsire.PasswordHash = hashemsire;

        //            var hemsireStore = new UserStore<AppUser, IdentityRole<Guid>, AppDbContext, Guid>(context);

        //            await hemsireStore.CreateAsync(hemsire);
        //            await hemsireStore.AddToRoleAsync(hemsire, "HEMSIRE");
        //            context.Hospitals.Update(hospital);
        //            await context.SaveChangesAsync();


        //            var personelId = Guid.NewGuid();
        //            var passwordHashPersonel = new PasswordHasher<AppUser>();
        //            AppUser personel = new AppUser
        //            {

        //                Id = personelId,
        //                Unvan = "Personel",
        //                HospitalId = hospital.Id,
        //                FirstName = "personel",
        //                LastName = "personel",
        //                UserName = "Personel",
        //                NormalizedUserName = "PERSONEL",
        //                Email = "personel@gmail.com",
        //                NormalizedEmail = "PERSONEL@GMAIL.COM",
        //                ImagePath = "/images/noImage.png",
        //                CreatedDate = DateTime.Now,
        //                EmailConfirmed = true,
        //                LockoutEnabled = true,
        //                TwoFactorEnabled = false,
        //                PhoneNumberConfirmed = false,
        //                SecurityStamp = Guid.NewGuid().ToString("D"),
        //                Rol = Rol.Personel,
        //                RolId = 7
        //                HospitalId = 1
        //            };

        //            var haspersonel = passwordHashPersonel.HashPassword(personel, "Test123.");
        //            personel.PasswordHash = haspersonel;

        //            var personelStore = new UserStore<AppUser, IdentityRole<Guid>, AppDbContext, Guid>(context);

        //            await personelStore.CreateAsync(personel);
        //            await personelStore.AddToRoleAsync(personel, "PERSONEL");
        //            context.Hospitals.Update(hospital);
        //            await context.SaveChangesAsync();
        //        }

        //    }
        //}
        #endregion
    }
}
