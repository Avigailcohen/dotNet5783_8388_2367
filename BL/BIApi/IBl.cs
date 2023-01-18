namespace BIApi
{
    public interface IBl
    {
        public IOrder Order { get; }
        public IProduct Product { get; }
        public ICart Cart { get; }
        public IUser User { get; }

    }
}
