
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Entities
{
    public class DbInitializer: IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CoffeeClubContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CoffeeClubContext>())
                {

                    //add sample coffees
                    if (!context.Coffees.Any())
                    {
                        var coffee1 = new Coffee()
                        { CoffeeId = 1, CoffeeName = "Cappuccino", CoffeePrice = 2.5, CountryOfOrigin = "Italy" };
                        context.Coffees.Add(coffee1);

                        var coffee2 = new Coffee()
                        { CoffeeId = 2, CoffeeName = "Latte", CoffeePrice = 2.5, CountryOfOrigin = "Colombia" };
                        context.Coffees.Add(coffee2);

                        var coffee3 = new Coffee()
                        { CoffeeId = 3, CoffeeName = "Espresso", CoffeePrice = 2.5, CountryOfOrigin = "Brazil" };
                        context.Coffees.Add(coffee3);

                        var coffee4 = new Coffee()
                        { CoffeeId = 4, CoffeeName = "Mocha", CoffeePrice = 2.5, CountryOfOrigin = "Malaysia" };
                        context.Coffees.Add(coffee4);

                        var coffee5 = new Coffee()
                        { CoffeeId = 5, CoffeeName = "Coffee", CoffeePrice = 2.5, CountryOfOrigin = "New Guinea" };
                        context.Coffees.Add(coffee5);

                        var coffee6 = new Coffee()
                        { CoffeeId = 6, CoffeeName = "Coffee", CoffeePrice = 2.5, CountryOfOrigin = "Bolivia" };
                        context.Coffees.Add(coffee6);

                        var coffee7 = new Coffee()
                        { CoffeeId = 7, CoffeeName = "Coffee", CoffeePrice = 2.5, CountryOfOrigin = "Brazil" };
                        context.Coffees.Add(coffee7);

                        var coffee8 = new Coffee()
                        { CoffeeId = 8, CoffeeName = "Coffee", CoffeePrice = 2.5, CountryOfOrigin = "Kenya" };
                        context.Coffees.Add(coffee8);

                        var coffee9 = new Coffee()
                        { CoffeeId = 9, CoffeeName = "Coffee", CoffeePrice = 2.5, CountryOfOrigin = "Bolivia" };
                        context.Coffees.Add(coffee9);

                        var coffee10 = new Coffee()
                        { CoffeeId = 10, CoffeeName = "Coffee", CoffeePrice = 2.5, CountryOfOrigin = "Brazil" };
                        context.Coffees.Add(coffee10);
                    }

                    // Add sample comments
                    if (!context.Comments.Any())
                    {
                        var comment1 = new Comments()
                        {
                            CommentId = 1,
                            Comment = "This is a comment",
                            CoffeeId = 1,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment1);

                        var comment2 = new Comments()
                        {
                            CommentId = 2,
                            Comment = "This is a comment",
                            CoffeeId = 2,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment2);

                        var comment3 = new Comments()
                        {
                            CommentId = 3,
                            Comment = "This is a comment",
                            CoffeeId = 3,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment3);

                        var comment4 = new Comments()
                        {
                            CommentId = 4,
                            Comment = "This is a comment",
                            CoffeeId = 4,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment4);

                        var comment5 = new Comments()
                        {
                            CommentId = 5,
                            Comment = "This is a comment",
                            CoffeeId = 5,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment5);

                        var comment6 = new Comments()
                        {
                            CommentId = 6,
                            Comment = "This is a comment",
                            CoffeeId = 6,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment6);

                        var comment7 = new Comments()
                        {
                            CommentId = 7,
                            Comment = "This is a comment",
                            CoffeeId = 7,
                            Rating = 5,
                            DateCreated = DateTime.Now
                        };
                        context.Comments.Add(comment7);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
