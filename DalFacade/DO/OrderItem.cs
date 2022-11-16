
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    public int ID { set; get; }
    public int OrderId { set; get; }
    public int ProductId { set; get; }
    public float Price { set; get; }
    public int Amount { set; get; }


    public override string ToString() => $@"
ID={ID},
OrderId={OrderId},
ProductId={ProductId},
Price={Price},
Amount={Amount}

";

}