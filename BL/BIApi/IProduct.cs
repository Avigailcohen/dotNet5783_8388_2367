using BO;

namespace BIApi
{
    public interface IProduct
    {

        /// <summary>
        /// return product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BO.Product GetProductsById(int id);

        /// <summary>
        /// return list of products for manager 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null);

        /// <summary>
        /// catalog for client
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductItem?> GetListedProductsForC();

        /// <summary>
        /// get product by ID for client
        /// </summary>
        /// <returns></returns>
        BO.ProductItem? RequestProductDetailsForC(int ProductId, BO.Cart cart);

        /// <summary>
        /// get  product by ID for manager
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>

        /// <summary>
        /// add new product to DB
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(BO.Product product);

        /// <summary>
        /// delete product from DB
        /// </summary>
        /// <param name="ID"></param>
        void DeleteProduct(int ID);

        /// <summary>
        /// update product propertis in DB
        /// </summary>
        /// <param name="product"></param>
        void UpdateProductData(BO.Product product);
        //IEnumerable<BO.ProductItem?> getByGrouping();
        /// <summary>
        /// func which grouping the most expensive products
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        IEnumerable<ProductItem?> MostExpensive(BO.Cart cart);
        //IEnumerable<ProductItem?> MostPopulars(BO.Cart cart);

    }
}
