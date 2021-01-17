using Domain.Models;

namespace Domain.ConnectModels
{   
    
    public class PizzaTypeLisaAssignment
    {
        public int Id { get; set; }
        public int PizzaTypeId { get; set; }
        public PizzaType? PizzaType { get; set; }
        public int LisaId { get; set; }
        public Lisa? Lisa { get; set; }
    }
}
