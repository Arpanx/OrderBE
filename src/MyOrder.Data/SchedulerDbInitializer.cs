﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Data
{
    public class SchedulerDbInitializer
    {        
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OrderContext(
               serviceProvider.GetRequiredService<DbContextOptions<OrderContext>>()))
            {
                // InitializeSchedules(context);
                // Look for any movies.
                if (context.Orders.Any())
                {
                    return;   // DB has been seeded
                }

                InitializeSchedules(context);
            }
        }

        private static void InitializeSchedules(OrderContext context)
        {
            if (!context.Orders.Any())
            {
                Orders user_01 = new Orders { Name = "Chris Sakellarios", Address = "Klovskaya strit 10", City = "Kiev" };

                Orders user_02 = new Orders { Name = "Charlene Campbell", Address = "ul. Pobedu 50", City = "Dnipro" };

                Orders user_03 = new Orders { Name = "Mattie Lyons", Address = "Astronomicheskaya, 10", City = "Kharkiv" };

                Orders user_04 = new Orders { Name = "Kelly Alvarez", Address = "Yubileynuy 8", City = "Avdeevka" };

                Orders user_05 = new Orders { Name = "Charlie Cox", Address = "Manhattan, A5", City = "NY" };

                Orders user_06 = new Orders { Name = "Megan	Fox", Address = "Bennelong Point, 1", City = "Sidney" };

                context.Orders.Add(user_01); context.Orders.Add(user_02);
                context.Orders.Add(user_03); context.Orders.Add(user_04);
                context.Orders.Add(user_05); context.Orders.Add(user_06);

                context.SaveChanges();
            }

            if(!context.Items.Any())
            {
                Item schedule_01 = new Item
                {
                    ProductName = "Product1",  // Title 
                    Description = "Nice product1",
                    Location = "Sklad1",
                    OrderId = 1,
                    Status = OrderStatus.Valid,
                    Type = OrderType.Retail,
                    TimeStart = DateTime.Now.AddHours(4),
                    TimeEnd = DateTime.Now.AddHours(6),
                };

                Item schedule_02 = new Item
                {
                    ProductName = "Product2",
                    Description = "Good product2",
                    Location = "Sklad2",
                    OrderId = 2,
                    Status = OrderStatus.Valid,
                    Type = OrderType.Social,
                    TimeStart = DateTime.Now.AddHours(3),
                    TimeEnd = DateTime.Now.AddHours(6),
                   
                };

                Item schedule_03 = new Item
                {
                    ProductName = "Product3",
                    Description = "Normal product3",
                    Location = "Sklad3",
                    OrderId = 3,
                    Status = OrderStatus.Valid,
                    Type = OrderType.DillerSilver,
                    TimeStart = DateTime.Now.AddHours(3),
                    TimeEnd = DateTime.Now.AddHours(6)                   
                };

                Item schedule_04 = new Item
                {
                    ProductName = "Product4",
                    Description = "bad product4",
                    Location = "Sklad4",
                    OrderId = 5,
                    Status = OrderStatus.Valid,
                    Type = OrderType.DillerGold,
                    TimeStart = DateTime.Now.AddHours(3),
                    TimeEnd = DateTime.Now.AddHours(6),
                 
                };

                Item schedule_05 = new Item
                {
                    ProductName = "Product5",
                    Description = "Nice product5",
                    Location = "Sklad5",
                    OrderId = 5,
                    Status = OrderStatus.Cancelled,
                    Type = OrderType.Social,
                    TimeStart = DateTime.Now.AddHours(5),
                    TimeEnd = DateTime.Now.AddHours(7),
                    
                };

                Item schedule_06 = new Item
                {
                    ProductName = "Product6",
                    Description = "Nice product6",
                    Location = "Sklad6",
                    OrderId = 3,
                    Status = OrderStatus.Cancelled,
                    Type = OrderType.Retail,
                    TimeStart = DateTime.Now.AddHours(22),
                    TimeEnd = DateTime.Now.AddHours(30)                   
                };

                Item schedule_07 = new Item
                {
                    ProductName = "Product7",
                    Description = "Nice product7",
                    Location = "Sklad7",
                    OrderId = 6,
                    Status = OrderStatus.Cancelled,
                    Type = OrderType.Retail,
                    TimeStart = DateTime.Now.AddHours(11),
                    TimeEnd = DateTime.Now.AddHours(13)                    
                };

                context.Items.Add(schedule_01); context.Items.Add(schedule_02);
                context.Items.Add(schedule_03); context.Items.Add(schedule_04);
                context.Items.Add(schedule_05); context.Items.Add(schedule_06);
            }

            context.SaveChanges();
        }
    }
}
