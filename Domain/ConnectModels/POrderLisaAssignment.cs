using Domain.Models;

namespace Domain.ConnectModels
{
    public class POrderLisaAssignment
    {
        public int Id { get; set; }
        public int POrderId { get; set; }
        public POrder? POrder { get; set; }
        public int LisaId { get; set; }
        public Lisa? Lisa { get; set; }
    }
}
