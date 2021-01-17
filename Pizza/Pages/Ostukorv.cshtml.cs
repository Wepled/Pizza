using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain.ConnectModels;
using Domain.Models;
using Domain.VmModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Pizza.Pages
{
    public class OstukorvModel : PageModel
    {

        public List<POrderVM> POrderVms = new List<POrderVM>();
        public int orderID;

        private readonly PizzaDbContext _context;

        public OstukorvModel(PizzaDbContext context)
        {
            _context = context;
        }

        public void OnGet(int orderId)
        {
            orderID = orderId;
            if (_context.POrderOrderAssignments != null)
            {
                foreach (POrderOrderAssignment order in _context.POrderOrderAssignments.Where(x => x.OrderId == orderId))
                {
                    POrderVM pOrderVm = new POrderVM();
                    if (_context.POrders != null)
                    {
                        pOrderVm.POrder = _context.POrders.Single(x => x.Id == order.POrderId);
                        if (_context.PizzaTypes != null)
                        {
                            PizzaType pizzaType = _context.PizzaTypes.Single(x => x.Id == pOrderVm.POrder.PizzaTypeId);
                            pOrderVm.PizzaType = pizzaType;
                            pOrderVm.Value += pizzaType.Value * GetSizeInInt(pOrderVm.POrder.Size);
                            pOrderVm.Name?.Add(pizzaType.Name + " " + GetSizeInStr(pOrderVm.POrder.Size));
                        }

                    }

                    if (_context.POrderLisaAssignments != null)
                    {
                        var pizzaLisad = _context.POrderLisaAssignments
                            .Where(x => x.POrderId == orderId)
                            .Include(x => x.Lisa)
                            .ToList();

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

                        if (AddedLisas.Count != 0) 
                        {
                            pOrderVm.Name?.Add("+");
                        }
                        
                        List<string> striList = new List<string>();

                        foreach (AddedLisaVM pizzaLisa in AddedLisas)
                        {
                            if (pizzaLisa.Lisa != null)
                            {
                                pizzaLisa.TotalValue += pizzaLisa.Count * pizzaLisa.Lisa.Value;
                                pOrderVm.Value += pizzaLisa.Count * pizzaLisa.Lisa.Value;
                                striList.Add(pizzaLisa.Count + " x " + pizzaLisa.Lisa.Name);
                            }
                        }
                        pOrderVm.Name?.Add(string.Join(", ", striList));
                    }
                    POrderVms.Add(pOrderVm);
                }
            }
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            int str = int.Parse(Request.Form["order"]);
            int pOrderCount = 0;
            int unPaid = 0;
            if (_context.POrderOrderAssignments != null)
            {
                List<POrderOrderAssignment> orders = _context.POrderOrderAssignments.Where(x => x.OrderId == str)
                    .Include(x => x.POrder).ToList();
                foreach (POrderOrderAssignment order in orders)
                {

                    if (Request.Form["pay_" + order.POrderId] == "on")
                    {
                        unPaid++;
                        if (order.POrder != null)
                        {
                            order.POrder.IsPaid = true;
                            pOrderCount++;
                        }
                        
                    }

                }
            }
            await _context.SaveChangesAsync();

            
            if (unPaid == pOrderCount && pOrderCount != 0) 
            {
                return Redirect(".");
            }
            return Redirect("./Ostukorv?orderId=" + str);
        }
        public async Task<IActionResult> OnPostDelete(int pOrderID)
        {
            int str = int.Parse(Request.Form["order"]);

            if (_context.POrderOrderAssignments != null && _context.POrders != null && _context.POrderLisaAssignments != null)
            {
                List<POrderOrderAssignment> orders = _context.POrderOrderAssignments.Where(x => x.POrderId == pOrderID).ToList();
                List<POrder> pOrders = _context.POrders.Where(x => x.Id == pOrderID).ToList();
                List<POrderLisaAssignment> pOrderLisaAssignments = _context.POrderLisaAssignments.Where(x=>x.POrderId == pOrderID).ToList();

                _context.POrders.RemoveRange(pOrders);
                _context.POrderOrderAssignments.RemoveRange(orders);
                _context.POrderLisaAssignments.RemoveRange(pOrderLisaAssignments);

            }

            await _context.SaveChangesAsync();
            return Redirect("./Ostukorv?orderId=" + str);
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
