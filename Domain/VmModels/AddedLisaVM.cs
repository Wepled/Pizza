using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.VmModels
{
   
    public class AddedLisaVM
    {
        public Lisa? Lisa { get; set; }
        public int Count { get; set; }
        public double TotalValue { get; set; } = 0;
    }
}
