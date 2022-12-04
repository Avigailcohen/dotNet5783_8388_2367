namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public IEnumerable<OrderItem?> OrderItems { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
