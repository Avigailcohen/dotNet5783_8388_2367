namespace BO
{
    public class Order
    {
        public int ID { set; get; }
        public string? CustomerName { set; get; }
        public string? CustomerEmail { set; get; }
        public string? CustomerAddress { set; get; }
        public OrderStatus Status { set; get; }
        public DateTime? OrderDate { set; get; }
        public DateTime? ShipDate { set; get; }
        public DateTime? DeliveryDate { set; get; }
        public double TotalPrice { get; set; }
        public IEnumerable<OrderItem?> OrderItems { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
