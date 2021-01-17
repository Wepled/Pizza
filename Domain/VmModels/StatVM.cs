using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.VmModels
{
    public class StatVM
    {
        public string PizzaTypeName { get; set; } = "";
        public int PizzaTypeId { get; set; } = 0;
        public int PizzaCount { get; set; } = 0;
        public double OneSmallValue { get; set; } = 0;
        public double Value { get; set; } = 0;

    }
}
