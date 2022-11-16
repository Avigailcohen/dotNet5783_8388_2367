
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    public int ID { set; get; }
    /// <summary>
    /// the Id of the Order
    /// </summary>
    public int OrderId { set; get; }
    /// <summary>
    /// the Id of the OrderItem
    /// </summary>
    public int ProductId { set; get; }
    /// <summary>
    /// the Id of the product
    /// </summary>
    public float Price { set; get; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public int Amount { set; get; }
    /// <summary>
    /// the amount of the OrderItme in the Order
    /// </summary>
    /// <returns>amount</returns>


    public override string ToString() => $@"
ID={ID},
OrderId={OrderId},
ProductId={ProductId},
Price={Price},
Amount={Amount}

";

}