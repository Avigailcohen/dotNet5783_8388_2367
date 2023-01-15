
namespace DO;

public struct Product
{
    public int ID { get; set; }
    /// <summary>
    /// the id of product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// the category of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// the amount of the stock
    /// </summary>
    /// <returns>how much in stock from the category</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }




}

