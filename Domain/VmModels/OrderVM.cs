using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.VmModels
{
    public class POrderVM
    {
        public POrder POrder { get; set; } = new POrder();
        public List<string> Name { get; set; } = new List<string>();
        public double Value { get; set; } = 0;
        public List<AddedLisaVM> AddedLisaVms { get; set; } = new List<AddedLisaVM>();
        public PizzaType PizzaType { get; set; } = new PizzaType();
    }
}
