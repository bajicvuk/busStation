namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;
    using static WebApp.Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            if (!context.Users.Any(u => u.UserName == "control@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "control", UserName = "control@yahoo.com", Email = "control@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Control123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Controller");
            }
            

            if (!context.Prices.Any(u => u.ticketType == Enums.TicketType.TimeTicket))
            {
                var price = new Prices() { price = 5, ticketType = Enums.TicketType.TimeTicket };
                price.Version = 0;
                context.Prices.Add(price);
            }
           
            if (!context.Prices.Any(u => u.ticketType == Enums.TicketType.MonthlyTicket))
            {
                var price = new Prices() { price = 30, ticketType = Enums.TicketType.MonthlyTicket };
                price.Version = 0;
                context.Prices.Add(price);
            }
           
            if (!context.Prices.Any(u => u.ticketType == Enums.TicketType.AnnualTicket))
            {
                var price = new Prices() { price = 350, ticketType = Enums.TicketType.AnnualTicket};
                price.Version = 0;
                context.Prices.Add(price);
            }
           
            if (!context.Prices.Any(u => u.ticketType == Enums.TicketType.DailyTicket))
            {
                var price = new Prices() { price = 12, ticketType = Enums.TicketType.DailyTicket };
                price.Version = 0;
                context.Prices.Add(price);
            }
       

            if (!context.Discounts.Any(u => u.Type == Enums.UserType.regular))
            {
                var discount = new Discounts() { Type = Enums.UserType.regular, Discount = 0 };
                discount.Version = 0;
                context.Discounts.Add(discount);
            }
            if (!context.Discounts.Any(u => u.Type == Enums.UserType.retiree))
            {
                var discount = new Discounts() { Type = Enums.UserType.retiree, Discount = 0.2 };
                discount.Version = 0;
                context.Discounts.Add(discount);
            }
            if (!context.Discounts.Any(u => u.Type == Enums.UserType.student))
            {
                var discount = new Discounts() { Type = Enums.UserType.student, Discount = 0.5 };
                discount.Version = 0;
                context.Discounts.Add(discount);
            }

            if (!context.Lines.Any(u => u.Name == "1"))
            {
                var line = new Line() { Name = "1", LineType = Enums.LineTypes.Urban };
                context.Lines.Add(line);
            }
            
            if (!context.Lines.Any(u => u.Name == "2"))
            {
                var line = new Line() { Name = "2", LineType = Enums.LineTypes.Suburban };
                context.Lines.Add(line);
            }
           

           if (!context.Schedules.Any(u => u.Line.Name == "1" && u.Day == Day.WorkDay))
            {
                var schedule = new Schedule() { Day = Day.WorkDay, LineId = context.Lines.Where(u => u.Name == "1").Select(u=> u.Id).First(), Duration = TimeSpan.FromMinutes(58), Depatures = new System.Collections.Generic.List<Depature>() { new Depature() { DepatureTime = "07:30" }, new Depature() { DepatureTime = "08:15" }, new Depature() { DepatureTime = "10:00" }, new Depature() { DepatureTime = "13:26" }, new Depature() { DepatureTime = "15:15" } } };
                context.Schedules.Add(schedule);
            }
            if (!context.Schedules.Any(u => u.Line.Name == "1" && u.Day == Day.Saturday))
            {
                var schedule = new Schedule() { Day = Day.Saturday, LineId = context.Lines.Where(u => u.Name == "1").Select(u => u.Id).First(), Duration = TimeSpan.FromMinutes(58), Depatures = new System.Collections.Generic.List<Depature>() { new Depature() { DepatureTime = "09:00" }, new Depature() { DepatureTime = "12:00" }, new Depature() { DepatureTime = "17:00" }, new Depature() { DepatureTime = "19:00" }, new Depature() { DepatureTime = "20:00" } } };
                context.Schedules.Add(schedule);
            }
            if (!context.Schedules.Any(u => u.Line.Name == "1" && u.Day == Day.Sunday))
            {
                var schedule = new Schedule() { Day = Day.Sunday, LineId = context.Lines.Where(u => u.Name == "1").Select(u => u.Id).First(), Duration = TimeSpan.FromMinutes(58), Depatures = new System.Collections.Generic.List<Depature>() { new Depature() { DepatureTime = "10:00" }, new Depature() { DepatureTime = "13:15" }, new Depature() { DepatureTime = "17:30" }, new Depature() { DepatureTime = "19:30" }, new Depature() { DepatureTime = "21:00" } } };
                context.Schedules.Add(schedule);
            }

            if (!context.Schedules.Any(u => u.Line.Name == "2" && u.Day == Day.WorkDay))
            {
                var schedule = new Schedule() { Day = Day.WorkDay, LineId = context.Lines.Where(u => u.Name == "2").Select(u => u.Id).First(), Duration = TimeSpan.FromMinutes(35), Depatures = new System.Collections.Generic.List<Depature>() { new Depature() { DepatureTime = "13:18" }, new Depature() { DepatureTime = "15:22" }, new Depature() { DepatureTime = "18:15" }, new Depature() { DepatureTime = "20:22" }, new Depature() { DepatureTime = "21:11" } } };
                context.Schedules.Add(schedule);
            }
            if (!context.Schedules.Any(u => u.Line.Name == "2" && u.Day == Day.Saturday))
            {
                var schedule = new Schedule() { Day = Day.Saturday, LineId = context.Lines.Where(u => u.Name == "2").Select(u => u.Id).First(), Duration = TimeSpan.FromMinutes(35), Depatures = new System.Collections.Generic.List<Depature>() { new Depature() { DepatureTime = "13:18" }, new Depature() { DepatureTime = "16:22" }, new Depature() { DepatureTime = "19:15" }, new Depature() { DepatureTime = "21:22" }, new Depature() { DepatureTime = "22:11" } } };
                context.Schedules.Add(schedule);
            }
            if (!context.Schedules.Any(u => u.Line.Name == "2" && u.Day == Day.Sunday))
            {
                var schedule = new Schedule() { Day = Day.Sunday, LineId = context.Lines.Where(u => u.Name == "2").Select(u => u.Id).First(), Duration = TimeSpan.FromMinutes(35), Depatures = new System.Collections.Generic.List<Depature>() { new Depature() { DepatureTime = "13:18" }, new Depature() { DepatureTime = "15:30" }, new Depature() { DepatureTime = "20:15" }, new Depature() { DepatureTime = "22:22" }, new Depature() { DepatureTime = "23:11" } } };
                context.Schedules.Add(schedule);
            }
            


        }
    }
}