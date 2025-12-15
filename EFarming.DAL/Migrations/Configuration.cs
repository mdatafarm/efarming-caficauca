namespace EFarming.DAL.Migrations
{
    using EFarming.Common.Encription;
    using EFarming.Core.AdminModule.DepartmentAggregate;
    using EFarming.Core.AuthenticationModule.AutenticationAggregate;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<EFarming.DAL.UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFarming.DAL.UnitOfWork context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Role role1 = new Role { Id = Guid.Parse("f8462ebe-814e-4caa-ad8c-3a94cd84e70c"), RoleName = "Admin" };
            //Role role2 = new Role { Id = Guid.Parse("45db8c6d-70ae-4d39-8c9e-7213899eed09"), RoleName = "User" };

            //var salt = EncriptorFactory.CreateEncriptor().GenerateSalt();
            //var pass = EncriptorFactory.CreateEncriptor().HashPassword("123456", salt);

            //User user1 = new User
            //{
            //    Id = Guid.Parse("0cf85e52-072b-4777-85f7-30c650f34ca5"),
            //    Username = "admin",
            //    Email = "admin@ymail.com",
            //    FirstName = "Admin",
            //    Password = pass,
            //    Salt = salt,
            //    IsActive = true,
            //    CreateDate = DateTime.UtcNow,
            //    Roles = new List<Role>()
            //};
            //User user2 = new User { 
            //    Id = Guid.Parse("6b984569-036d-424a-8ba5-6a5e96e7175c"), Username = "user1", Email = "user1@ymail.com",
            //    FirstName = "User1",
            //    Password = pass,
            //    Salt = salt,
            //    IsActive = true,
            //    CreateDate = DateTime.UtcNow,
            //    Roles = new List<Role>()
            //};
            //user1.Roles.Add(role1);
            //user2.Roles.Add(role2);
            //context.Users.AddOrUpdate(user1);
            //context.Users.AddOrUpdate(user2);
            //context.SaveChanges();
        }
    }
}
