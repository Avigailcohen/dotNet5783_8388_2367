
namespace DO;

public struct Product
{
    public int ID { get; set; }
    public string?Name{get; set;}
    public Enum Category { get; set; }
    public float Price { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
ID={ID},
Name={Name},
Category={Category},
Price={Price},
InStock={InStock},

";
        
    }

