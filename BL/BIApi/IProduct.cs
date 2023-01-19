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
        /// get product list for manager 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null);

        /// <summary>
        /// catalog for client
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductItem?> GetListedProductsForC();

        /// <summary>
        /// get product bt ID for client
        /// </summary>
        /// <returns></returns>
        BO.ProductItem? RequestProductDetailsForC(int ProductId, BO.Cart cart);

        /// <summary>
        /// get  product by ID for manager
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>

        /// <summary>
        /// add neww product to DB
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

        IEnumerable<ProductItem?> MostExpensive(BO.Cart cart);

    }
}
