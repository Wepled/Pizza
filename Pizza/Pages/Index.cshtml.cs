using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain.ConnectModels;
using Domain.Models;
using Domain.VmModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Pizza.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PizzaDbContext _context;
        private string Category { get; set; } = "";
        private int[] _Meat = new []{1, 2, 8, 9, 17};
        private int _Cheese = 10;

        public int orderID = 0;

        public IndexModel(PizzaDbContext context)
        {
            _context = context;
        }

        public List<PizzaTypeVM> Pizzas = new List<PizzaTypeVM>();

        public void OnGet(string? category,
                            int? orderId)
        {
            Category = category == null ? "" : category;
            if (orderId != null) 
            {
                orderID = orderId.Value;
            }
            List<Lisa> lisas;
            List<PizzaType> pizzaTypes;
            List<PizzaTypeLisaAssignment> pizzaTypeLisas;

            if (_context != null)
            {
                if (_context.Lisas != null)
                {
                    lisas = _context.Lisas.ToList();
                }

                if (_context.PizzaTypes != null && _context.PizzaTypeLisaAssignments != null)
                {
                    pizzaTypeLisas = _context.PizzaTypeLisaAssignments
                        .Include(x=>x.PizzaType)
                        .Include(x=>x.Lisa).
                        ToList();
                    pizzaTypes = _context.PizzaTypes.ToList();

                    foreach (PizzaType pizzaType in pizzaTypes)
                    {
                        var newPizza = new PizzaTypeVM();
                        newPizza.PizzaType = pizzaType;
                        newPizza.Lisas = new List<string>();
                        List<string> Name = new List<string>();
                        foreach (var pizzTypeLisa in pizzaTypeLisas)
                        {
                            if (pizzTypeLisa.Lisa != null && pizzTypeLisa.PizzaTypeId == pizzaType.Id)
                            {
                                newPizza.Lisas.Add(pizzTypeLisa.Lisa.Name);
                                newPizza.LisasInt.Add(pizzTypeLisa.Lisa.Id);
                                Name.Add(pizzTypeLisa.Lisa.Name);
                            }
                        }
                        newPizza.NameLisas = string.Join(", ", Name);
                        Pizzas.Add(newPizza);    
                    }

                }

                if (Category != "")
                {
                    var PizzasToTest = Pizzas;
                    Pizzas = new List<PizzaTypeVM>();
                    if (Category == "Cheese")
                    {
                        foreach (PizzaTypeVM pizza in PizzasToTest)
                        {
                            if (pizza.LisasInt.Contains(_Cheese))
                            {
                                Pizzas.Add(pizza);
                            }
                        }
                    }
                    if (Category == "Vegan")
                    {
                        foreach (PizzaTypeVM pizza in PizzasToTest)
                        {
                            if (CheckForMeat(pizza.LisasInt))
                            {
                                Pizzas.Add(pizza);
                            }
                        
                        }
                    }
                    if (Category == "Meat")
                    {
                        foreach (PizzaTypeVM pizza in PizzasToTest)
                        {
                            if (!CheckForMeat(pizza.LisasInt))
                            {
                                Pizzas.Add(pizza);
                            }

                        }
                    }

                }

            }
        }

        public bool CheckForMeat(List<int> pList)
        {
            foreach (var meat in _Meat)
            {
                if (pList.Contains(meat))
                {
                    return false;
                }
            }

            return true;

        }

        public async Task<IActionResult> OnPostChange()
        {
            await _context.SaveChangesAsync();
            return Redirect("./Index?category=" + Request.Form["mode"] + (orderID != 0 ? "&orderId=" + orderID : ""));
        }

        public string GetClass(string mode)
        {
            if (mode == Category)
            {
                return "active";
            }

            return "";

        }
    }
}
