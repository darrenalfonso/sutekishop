﻿using System;
using NUnit.Framework;
using Suteki.Common.Models;

// ReSharper disable InconsistentNaming

namespace Suteki.Shop.Tests.Models
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void UserAsString_should_return_user_email()
        {
            var order = new Order
            {
                User = new User
                {
                    Email = "mike@mike.com"
                }
            };

            order.UserAsString.ShouldEqual("mike@mike.com");
        }

        public static Order Create350GramOrder()
        {
            var order = new Order
            {
                Basket = BasketTests.Create350GramBasket(),
                UseCardHolderContact = true,
                CardContact = new Contact { Country = new Country
                    {
                       PostZone = new PostZone { Multiplier = 2.5M, FlatRate = new Money(10.00M), AskIfMaxWeight = false }
                    } },
                Email = "mike@mike.com",
                CreatedDate = new DateTime(2008, 10, 18),
                OrderStatus = new OrderStatus { Name = "Dispatched" }
            };
            return order;
        }

        public static Order Create450GramOrder()
        {
            var order = Create350GramOrder();

            // add one more item to make max weight band (weight now 450)
            order.Basket.BasketItems.Add(new BasketItem
            {
                Quantity = 1,
                Size = new Size
                {
                    Product = new Product { Weight = 100, Price = new Money(0M) }
                }
            });
            return order;
        }
    }
}
// ReSharper restore InconsistentNaming
