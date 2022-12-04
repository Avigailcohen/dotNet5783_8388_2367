using DalApi;
using DO;
namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// function which add product to list of product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>the ID of the product</returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product product)
    {
        if (DataSource.productList.Exists(x => x?.ID == product.ID))
            throw new DalIdAlreadyExistException(product.ID, "Product");
        DataSource.productList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// function which gets the ID of the product, search him by the ID, and return the product
    /// </summary>
    /// <param name="id"></param>
    /// <returns>product</returns>
    /// <exception cref="Exception"></exception>

    public Product GetById(int id)
    {
        Product product = new Product();
        product = DataSource.productList.Find(x => x?.ID == id) ?? throw new DalIdDoNotExistException(product.ID,"id");
        return product;
    }
    /// <summary>
    /// function which get product , and search him by his ID, and if he is exist, we remove him and add other.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product product)
    {
        int x = DataSource.productList.FindIndex(x => x?.ID == product.ID);
        if (x == -1)
            throw new DalIdDoNotExistException(product.ID,"not exist");
        DataSource.productList[x] = product;
    }
    /// <summary>
    /// function which get ID of product and delte him. if he doesnt exist we throw exepction
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int x = DataSource.productList.FindIndex(x => x?.ID == id);
        if (x == -1)
            throw new DalIdDoNotExistException(id,"product not founed");
        DataSource.productList.RemoveAt(x);

    }
    /// <summary>
    /// copy the detalis of one list to another.
    /// </summary>
    /// <returns> the product list</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.productList.Where(item => filter(item));
        return DataSource.productList.Select(item => item);
    }


}
