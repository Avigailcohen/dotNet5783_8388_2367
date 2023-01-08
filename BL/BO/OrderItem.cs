namespace BO
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int AmountOfItem { get; set; }
        public double TotalPrice { get; set; }
        public string? ImageRelativeName { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
