using Domain.Models;

namespace Domain.ConnectModels
{
    public class POrderOrderAssignment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int POrderId { get; set; }
        public POrder? POrder { get; set; }
    }
}
