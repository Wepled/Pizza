namespace Domain.Models
{
    public enum PizzaSize
    {
        Small,
        Medium,
        Big
    }

    public class POrder
    {
        public int Id { get; set; }
        public int PizzaTypeId { get; set; }
        public PizzaSize Size { get; set; }
        public bool IsPaid { get; set; } = false;
        public int OrderId { get; set; }
    }
}
