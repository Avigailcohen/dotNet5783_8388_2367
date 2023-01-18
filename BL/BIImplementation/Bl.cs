using BIApi;
using BO;

namespace BIImplementation;
sealed public class Bl : IBl
{
    internal Bl() { }

    public IOrder Order { get; } = new Order();
    public IProduct Product { get; } = new Product();
    public ICart Cart { get; } = new Cart();
    public IUser User { get; } = new User();



}
