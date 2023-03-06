namespace TrucksTakov.Models.Order
{
    public class OrderIndexVM
    {
        public int Id { get; set; } 
        public string OrderDate { get; set; }
        public string UserId { get; set; }
        public string User { get; set; }
        public int TruckId { get; set; }
        public string Model { get; set; }
        public string Truck { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
