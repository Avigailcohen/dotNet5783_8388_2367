namespace BIApi;

public interface ICart
{

    /// <summary>
    /// function which add prodcut to the cart 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ProductID"></param>
    /// <returns></returns>
    public BO.Cart AddProduct(BO.Cart cart, int ProductID);
    /// <summary>
    /// function which can add\delete prodcuts from the cary by getting the new amount.
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ProductID"></param>
    /// <param name="NewAmount"></param>
    /// <returns></returns>
    BO.Cart UpdateAmount(BO.Cart cart, int ProductID, int NewAmount);
    /// <summary>
    /// function which create a new order and add the order to list of the orders
    /// </summary>
    /// <param name="cart"></param>
     void OrderConfirmation(BO.Cart cart);




    //public int AddProduct(BO.Product);
    //פה נכניס את שאר ההצהרות על המתודות 

}
