namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int AmountItems { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
