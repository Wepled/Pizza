using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ConnectModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace DAL
{
    public static class DbInitializer
    {
        public static void Initialize(PizzaDbContext context)
        {
            if (context.PizzaTypes?.Count() != 0)
            {
                return;   // DB has been seeded
            }

            var pizzaTypes = new PizzaType[]
            {
                //1
                new PizzaType {Name = "Mafioso", Value = 15},
                //2
                new PizzaType {Name = "4Seasons", Value = 20},
                //3
                new PizzaType {Name = "Peetri", Value = 35},
                //4
                new PizzaType {Name = "Margarita", Value = 10},
                //5
                new PizzaType {Name = "Ksesha", Value = 666},
                //6
                new PizzaType {Name = "Morgenstern", Value = 666},
                //7
                new PizzaType {Name = "Sausage", Value = 4},
                //8
                new PizzaType {Name = "Indian", Value = 6},
                //9
                new PizzaType {Name = "Korean", Value = 10},
                //10
                new PizzaType {Name = "Meat Boy", Value = 30}
                
            };

            context.PizzaTypes?.AddRange(pizzaTypes);
            context.SaveChanges();

            var lisas = new Lisa[]
            {
                //1
                new Lisa { Name = "Cheese", Value = 1},
                //2
                new Lisa { Name = "Sauce", Value = 0.6},
                //3
                new Lisa { Name = "Rukola", Value = 0.6},
                //4
                new Lisa { Name = "Pineapple", Value = 0.6},
                //5
                new Lisa { Name = "Onion", Value = 0.6},
                //6
                new Lisa { Name = "Papriika", Value = 0.6},
                //7
                new Lisa { Name = "Brokkoli", Value = 0.6},
                //8
                new Lisa { Name = "Tomato", Value = 0.6},
                //9
                new Lisa { Name = "Salami", Value = 0.6},
                //10
                new Lisa { Name = "Meat", Value = 1},
                //11
                new Lisa { Name = "Chicken", Value = 0.6},
                //12
                new Lisa { Name = "BBQ Sauce", Value = 0.6},
                //13
                new Lisa { Name = "Red-Wine Sauce", Value = 1},
                //14
                new Lisa { Name = "Jalapeno", Value = 1},
                //15
                new Lisa { Name = "Olives", Value = 1},
                //16
                new Lisa { Name = "Mozzarella", Value = 2},
                //17
                new Lisa { Name = "Pepperoni", Value = 2},
                //18
                new Lisa { Name = "RoastBiff", Value = 2},
                //19
                new Lisa { Name = "Tex-mex sauce", Value = 0.6}
                
            };

            context.Lisas?.AddRange(lisas);
            context.SaveChanges();

            var pOrders = new POrder[]
            {
                new POrder(),
            };

            context.POrders?.AddRange(pOrders);
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order(),
            };

            context.Orders?.AddRange(orders);
            context.SaveChanges();

            var pizzaTypeLisa = new PizzaTypeLisaAssignment[]
            {

                new PizzaTypeLisaAssignment{PizzaTypeId = 1, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 1, LisaId = 2},
                new PizzaTypeLisaAssignment{PizzaTypeId = 1, LisaId = 3},
                new PizzaTypeLisaAssignment{PizzaTypeId = 1, LisaId = 4},
                new PizzaTypeLisaAssignment{PizzaTypeId = 1, LisaId = 5},
                new PizzaTypeLisaAssignment{PizzaTypeId = 1, LisaId = 6},

                new PizzaTypeLisaAssignment{PizzaTypeId = 2, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 2, LisaId = 3},
                new PizzaTypeLisaAssignment{PizzaTypeId = 2, LisaId = 6},
                new PizzaTypeLisaAssignment{PizzaTypeId = 2, LisaId = 8},
                new PizzaTypeLisaAssignment{PizzaTypeId = 2, LisaId = 10},
                new PizzaTypeLisaAssignment{PizzaTypeId = 2, LisaId = 12},

                new PizzaTypeLisaAssignment{PizzaTypeId = 3, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 3, LisaId = 3},
                new PizzaTypeLisaAssignment{PizzaTypeId = 3, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 3, LisaId = 9},
                new PizzaTypeLisaAssignment{PizzaTypeId = 3, LisaId = 14},
                new PizzaTypeLisaAssignment{PizzaTypeId = 3, LisaId = 18},

                new PizzaTypeLisaAssignment{PizzaTypeId = 4, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 4, LisaId = 6},
                new PizzaTypeLisaAssignment{PizzaTypeId = 4, LisaId = 18},
                new PizzaTypeLisaAssignment{PizzaTypeId = 4, LisaId = 10},
                new PizzaTypeLisaAssignment{PizzaTypeId = 4, LisaId = 19},
                new PizzaTypeLisaAssignment{PizzaTypeId = 4, LisaId = 16},

                new PizzaTypeLisaAssignment{PizzaTypeId = 5, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 5, LisaId = 14},
                new PizzaTypeLisaAssignment{PizzaTypeId = 5, LisaId = 3},
                new PizzaTypeLisaAssignment{PizzaTypeId = 5, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 5, LisaId = 2},

                new PizzaTypeLisaAssignment{PizzaTypeId = 6, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 6, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 6, LisaId = 5},
                new PizzaTypeLisaAssignment{PizzaTypeId = 6, LisaId = 4},
                new PizzaTypeLisaAssignment{PizzaTypeId = 6, LisaId = 19},
                new PizzaTypeLisaAssignment{PizzaTypeId = 6, LisaId = 17},
                
                new PizzaTypeLisaAssignment{PizzaTypeId = 7, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 7, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 7, LisaId = 5},
                new PizzaTypeLisaAssignment{PizzaTypeId = 7, LisaId = 4},
                new PizzaTypeLisaAssignment{PizzaTypeId = 7, LisaId = 15},
                new PizzaTypeLisaAssignment{PizzaTypeId = 7, LisaId = 17},

                new PizzaTypeLisaAssignment{PizzaTypeId = 8, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 8, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 8, LisaId = 5},
                new PizzaTypeLisaAssignment{PizzaTypeId = 8, LisaId = 4},
                new PizzaTypeLisaAssignment{PizzaTypeId = 8, LisaId = 6},
                new PizzaTypeLisaAssignment{PizzaTypeId = 8, LisaId = 17},

                new PizzaTypeLisaAssignment{PizzaTypeId = 9, LisaId = 1},
                new PizzaTypeLisaAssignment{PizzaTypeId = 9, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 9, LisaId = 5},
                new PizzaTypeLisaAssignment{PizzaTypeId = 9, LisaId = 4},
                new PizzaTypeLisaAssignment{PizzaTypeId = 9, LisaId = 2},
                new PizzaTypeLisaAssignment{PizzaTypeId = 9, LisaId = 17},

                new PizzaTypeLisaAssignment{PizzaTypeId = 10, LisaId = 7},
                new PizzaTypeLisaAssignment{PizzaTypeId = 10, LisaId = 5},
                new PizzaTypeLisaAssignment{PizzaTypeId = 10, LisaId = 4},
                new PizzaTypeLisaAssignment{PizzaTypeId = 10, LisaId = 3}

            };

            context.PizzaTypeLisaAssignments?.AddRange(pizzaTypeLisa);
            context.SaveChanges();
        }
    }
}
