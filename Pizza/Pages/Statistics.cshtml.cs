using DAL;
using Domain.ConnectModels;
using Domain.Models;
using Domain.VmModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Pizza.Pages
{
    public class StatisticsModel : PageModel
    {
        private readonly PizzaDbContext _context;
        public List<StatVM> StatVMS = new List<StatVM>();
        public StatisticsModel(PizzaDbContext context)
        {
            _context = context;
        }

        public void OnGet(int orderId)
        {
            if (_context.PizzaTypes != null)
            {
              foreach(PizzaType pizzaType in _context.PizzaTypes) 
              {
                    StatVM statVM = new StatVM();
                    if (pizzaType.Name != null) 
                    {
                        statVM.PizzaTypeName = pizzaType.Name;
                        statVM.PizzaTypeId = pizzaType.Id;
                        statVM.OneSmallValue = pizzaType.Value;
                    }
                    StatVMS.Add(statVM);
              } 
                
            }

            if (_context.POrders != null)
            {
                foreach (POrder pOrder in _context.POrders.Where(x=>x.IsPaid)) 
                {
                    StatVM statVM = StatVMS.Single(x=>x.PizzaTypeId == pOrder.PizzaTypeId);
                    StatVMS.Remove(statVM);
                    statVM.Value = GetSizeInInt(pOrder.Size) * statVM.OneSmallValue;
                    statVM.PizzaCount++;
                    if (_context.POrderLisaAssignments != null) 
                    {
                        List<POrderLisaAssignment> pOrderLisaAssignments = _context.POrderLisaAssignments.Where(x => x.POrderId == pOrder.Id)
                                                                                                     .Include(x => x.Lisa).ToList();
                        foreach (POrderLisaAssignment pOrderLisaAssignment in pOrderLisaAssignments) 
                        {
                            if (pOrderLisaAssignment.Lisa != null) 
                            {
                                statVM.Value += pOrderLisaAssignment.Lisa.Value;
                            }
                        }
                    }

                    StatVMS.Add(statVM);
                }
            }

            
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
