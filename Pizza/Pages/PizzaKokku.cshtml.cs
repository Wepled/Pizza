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

namespace Pizza.Pages
{
    public class PizzaKokkuModel : PageModel
    {
        public POrderVM POrderVm = new POrderVM();

        private readonly PizzaDbContext _context;

        public int orderID = 0;

        public PizzaKokkuModel(PizzaDbContext context)
        {
            _context = context;
        }

        public void OnGet(int pOrderId, int? orderId)
        {

            if (orderId != null) 
            {
                orderID = orderId.Value; 
            }

            if (_context.POrders != null)
            {
                POrderVm.POrder = _context.POrders.Single(x => x.Id == pOrderId);
                if (_context.PizzaTypes != null)
                {
                    PizzaType pizzaType = _context.PizzaTypes.Single(x => x.Id == POrderVm.POrder.PizzaTypeId);
                    POrderVm.PizzaType = pizzaType;
                    POrderVm.Value += pizzaType.Value * GetSizeInInt(POrderVm.POrder.Size);
                    POrderVm.Name.Add(pizzaType.Name + " " + GetSizeInStr(POrderVm.POrder.Size));
                }
            }

            if (_context.POrderLisaAssignments != null)
            {
                var pizzaLisad = _context.POrderLisaAssignments.Where(x => x.POrderId == pOrderId)
                    .Include(x=>x.Lisa).ToList();

                List<AddedLisaVM> AddedLisas = new List<AddedLisaVM>();
                foreach (POrderLisaAssignment pizzaLisa in pizzaLisad)
                {

                    if (AddedLisas.Where(x => x.Lisa?.Id == pizzaLisa.LisaId).Any() != true)
                    {
                        var newAddLisa = new AddedLisaVM();
                        newAddLisa.Lisa = pizzaLisa.Lisa;
                        newAddLisa.Count++;
                        AddedLisas.Add(newAddLisa);
                    }
                    else
                    {
                        AddedLisas.Single(x => x.Lisa == pizzaLisa.Lisa).Count++;
                    }

                }

                foreach (AddedLisaVM pizzaLisa in AddedLisas)
                {
                    if (pizzaLisa.Lisa != null)
                    {
                        pizzaLisa.TotalValue += pizzaLisa.Count * pizzaLisa.Lisa.Value;
                        POrderVm.Value += pizzaLisa.Count * pizzaLisa.Lisa.Value;
                    }
                }

                POrderVm.AddedLisaVms = AddedLisas;
            }
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            int OrderToCreateID = int.Parse(Request.Form["orderid"]);
            if (OrderToCreateID == 0 ) 
            {
                _context.Orders?.Add(new Order());
                _context.SaveChanges();
                if (_context.Orders != null) 
                {
                    OrderToCreateID = _context.Orders.Count();
                }
                
            }
            

            if (_context.Orders != null)
            {
                POrderOrderAssignment newPOrderOrderAssignment = new POrderOrderAssignment()
                {
                    OrderId = OrderToCreateID,
                    POrderId = int.Parse(Request.Form["id"])
                };
                _context.POrderOrderAssignments?.Add(newPOrderOrderAssignment);
                await _context.SaveChangesAsync();
                return Redirect("./Ostukorv?orderId=" + _context.Orders.Count());
            }

            await _context.SaveChangesAsync();
            return Redirect("./Ostukorv?orderId=" + 1);
        }

        public string GetSizeInStr(PizzaSize size)
        {
            if (size == PizzaSize.Small)
            {
                return "Small";
            }

            if (size == PizzaSize.Medium)
            {
                return "Medium";
            }

            return "Big";
        }
        public double GetSizeInInt(PizzaSize size)
        {
            if (size == PizzaSize.Small)
            {
                return 1;
            }

            if (size == PizzaSize.Medium)
            {
                return 1.5;
            }

            return 3;
        }
    }
}
