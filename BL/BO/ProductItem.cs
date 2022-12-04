namespace BO
{
    public class ProductItem
    {
        public int ID { get; set; }
        public string? ProductItemName { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public bool InStock { get; set; }
        public int AmountInCart { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
