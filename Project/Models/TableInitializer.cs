using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class TableInitializer : DropCreateDatabaseIfModelChanges<MyDatabaseEntities>
    {
        protected override void Seed(MyDatabaseEntities context)
        {
            var user = new List<User>
            {
                new User {FirstName = "Brizton", LastName = "Floyd", Email="bf@yahoo.com",Password ="please23"},
                new User {FirstName = "Alexia", LastName = "Diaz", Email="ad@gmail.com",Password ="lexie2010"}
            };
            foreach (var item in user)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();

            var order = new List<Order>
            {
                new Order {},
                new Order { }
            };

            foreach (var item in order)
            {
                context.Orders.Add(item);
            }
            context.SaveChanges();


        }
    }
}