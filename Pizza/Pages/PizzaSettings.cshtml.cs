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


    public class PizzaSettingsModel : PageModel
    {
        private readonly PizzaDbContext _context;

        public PizzaSettingsModel(PizzaDbContext context)
        {
            _context = context;
        }

        public List<Lisa> Lisas = new List<Lisa>();
        public List<AddedLisaVM> AddedLisas = new List<AddedLisaVM>();
        public PizzaType PizzaType = new PizzaType();
        public List<PizzaTypeLisaAssignment> PizzaTypeLisas = new List<PizzaTypeLisaAssignment>();
        public List<POrderLisaAssignment> POrderLisaAssignments = new List<POrderLisaAssignment>();
        public POrder pizzaOrder = new POrder();

        public int orderID = 0;

        public void OnGet(int? pizzaId, int? pOrderId, int? addLisa, int? orderId)
        {
            if (orderId != null) 
            {
                orderID = orderId.Value;
            }

            if (_context.Lisas != null)
            {
                Lisas = _context.Lisas.ToList();
            }

            if (pOrderId != null && _context.POrders != null)
            {
                pizzaId = _context.POrders.Single(x => x.Id == pOrderId).PizzaTypeId;
            }

            if (_context.PizzaTypes != null && _context.PizzaTypeLisaAssignments != null && _context.POrderLisaAssignments != null)
            {
                PizzaTypeLisas = _context.PizzaTypeLisaAssignments
                    .Where(x=>x.PizzaTypeId == pizzaId)
                    .Include(x => x.PizzaType)
                    .Include(x => x.Lisa).ToList();

                if (pOrderId != null)
                {
                    POrderLisaAssignments = _context.POrderLisaAssignments
                        .Where(x => x.POrderId == pOrderId)
                        .Include(x => x.POrder)
                        .Include(x => x.Lisa).ToList();
                }

                if (pizzaId != null)
                {
                    PizzaType = _context.PizzaTypes.Single(x => x.Id == pizzaId);
                    
                    if (_context.POrders?.Any(x => x.Id == pOrderId) == true)
                    {
                        pizzaOrder = _context.POrders.Single(x => x.Id == pOrderId);
                    }
                    else
                    {
                        pizzaOrder.PizzaTypeId = PizzaType.Id;
                        pizzaOrder.Size = PizzaSize.Medium;
                    }

                    foreach (PizzaTypeLisaAssignment pizzaLisa in PizzaTypeLisas)
                    {

                        if (AddedLisas.Any(x => x.Lisa?.Id == pizzaLisa.LisaId) == false)
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

                    if (_context.POrderLisaAssignments != null && pOrderId != null)
                    {
                        var pizzaLisad = _context.POrderLisaAssignments.Where(x => x.POrderId == pOrderId).ToList();
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
                    }
                }

                if (pizzaOrder.Id == 0)
                {
                    _context.POrders?.Add(pizzaOrder);
                }

                _context.SaveChanges();
            }
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            var Lisa = int.Parse(Request.Form["id"]);
            var orderid = int.Parse(Request.Form["orderid"]);
            int Count = 1;
            if (_context.POrders != null)
            {
                Count = _context.POrders.ToList().Last().Id;
            }
            POrderLisaAssignment pOrderLisaToAdd = new POrderLisaAssignment
            {
                LisaId = Lisa,
                POrderId = Count
            };

            _context.POrderLisaAssignments?.Add(pOrderLisaToAdd);
            await _context.SaveChangesAsync();

            return Redirect("./PizzaSettings?pOrderId=" + Count + (orderid != 0 ? "&orderId=" + orderid : ""));
        }

        public async Task<IActionResult> OnPostDropSm(string Size)
        {
            var Lisa = Request.Form["small"];
            var orderid = int.Parse(Request.Form["orderid"]);

            int Count = 1;
            if (_context.POrders != null)
            {
                Count = _context.POrders.ToList().Last().Id;
                POrder order = _context.POrders.Single(x => x.Id == Count);
                order.Size = PizzaSize.Small;
            }

            await _context.SaveChangesAsync();

            return Redirect("./PizzaSettings?pOrderId=" + Count + (orderid != 0 ? "&orderId=" + orderid : ""));
        }
        public async Task<IActionResult> OnPostDropMm(string Size)
        {
            var Lisa = Request.Form["medium"];
            var orderid = int.Parse(Request.Form["orderid"]);

            int Count = 1;
            if (_context.POrders != null)
            {
                Count = _context.POrders.ToList().Last().Id;
                POrder order = _context.POrders.Single(x => x.Id == Count);
                order.Size = PizzaSize.Medium;
            }

            await _context.SaveChangesAsync();

            return Redirect("./PizzaSettings?pOrderId=" + Count + (orderid != 0 ? "&orderId=" + orderid : ""));
        }

        public async Task<IActionResult> OnPostDropBg(string Size)
        {
            var Lisa = Request.Form["big"];
            var orderid = int.Parse(Request.Form["orderid"]);

            int Count = 1;
            if (_context.POrders != null)
            {
                Count = _context.POrders.ToList().Last().Id;
                POrder order = _context.POrders.Single(x => x.Id == Count);
                order.Size = PizzaSize.Big;
            }

            await _context.SaveChangesAsync();

            return Redirect("./PizzaSettings?pOrderId=" + Count + (orderid != 0 ? "&orderId=" + orderid : ""));
        }

        public string getClass(string size)
        {
            if (size == "S")
            {
                return pizzaOrder.Size == PizzaSize.Small ? "btn-success" : "btn-outline-success";
            }
            if (size == "M")
            {
                return pizzaOrder.Size == PizzaSize.Medium ? "btn-success" : "btn-outline-success";
            }
            return pizzaOrder.Size == PizzaSize.Big ? "btn-success" : "btn-outline-success";
        }
    }
}
