using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.VmModels
{
    public class PizzaTypeVM
    {
        public PizzaType? PizzaType { get; set; }
        public List<string> Lisas { get; set; } = new List<string>();
        public List<int> LisasInt { get; set; } = new List<int>();
        public string NameLisas{ get; set; } = "";
    }
}
