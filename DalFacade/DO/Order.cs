
namespace DO;

public struct Order
{
    public int ID { set; get; }
    /// <summary>
    /// the Id of the order
    /// </summary>
    public string? CustomerName { set; get; }
    /// <summary>
    /// the name of the customer
    /// </summary>
    public string? CustomerEmail { set; get; }
    /// <summary>
    /// the Email of the customer
    /// </summary>
    public string? CustomerAddress { set; get; }
    /// <summary>
    /// the Address of the customer
    /// </summary>
    public DateTime? OrderDate { set; get; }
    /// <summary>
    /// the orderDate
    /// </summary>

    public DateTime? ShipDate { set; get; }
    /// <summary>
    /// time of shipping the order
    /// </summary>
    public DateTime? DeliveryDate { set; get; }
    /// <summary>
    /// time of Delivery
    /// </summary>
    /// <returns></returns>

    public override string ToString() => $@"
ID={ID},
CustomerName: {CustomerName},
CustomerEmail: {CustomerEmail},
CustomerAddress: {CustomerAddress},
OrderDate: {OrderDate},
ShipDate: {ShipDate},
DeliveryDate: {DeliveryDate}
";
}