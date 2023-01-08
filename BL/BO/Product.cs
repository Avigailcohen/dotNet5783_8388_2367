namespace BO
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int InStock { get; set; }
        public string? ImageRelativeName { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
