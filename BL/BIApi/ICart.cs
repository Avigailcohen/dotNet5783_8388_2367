namespace BIApi;

public interface ICart
{


    public BO.Cart AddProduct(BO.Cart cart, int ProductID);
    BO.Cart UpdateAmount(BO.Cart cart, int ProductID, int NewAmount);
    void OrderConfirmation(BO.Cart cart);




    //public int AddProduct(BO.Product);
    //פה נכניס את שאר ההצהרות על המתודות 

}
