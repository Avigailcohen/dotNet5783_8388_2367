﻿using DO;
namespace Dal;

public class DalProduct
{
    /// <summary>
    /// function which add product to list of product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>the ID of the product</returns>
    /// <exception cref="Exception"></exception>
    public int Add ( Product product)
    {
        if (DataSource.ProductList.Exists(x => x?.ID == product.ID))
            throw new Exception("The product is exist");
        DataSource.ProductList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// function which gets the ID of the product, search him by the ID, and return the product
    /// </summary>
    /// <param name="id"></param>
    /// <returns>product</returns>
    /// <exception cref="Exception"></exception>
    
    public Product GetById (int id)
    {
        Product product = new Product();
        product = DataSource.ProductList.Find(x => x?.ID == id) ?? throw new Exception("not founded");
        return product;
    }
    /// <summary>
    /// function which get product , and search him by his ID, and if he is exist, we remove him and add other.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>
    public void update(Product product)
    {
        int x = DataSource.ProductList.FindIndex(x => x?.ID == product.ID);
        if (x == -1)
            throw new Exception("not exist");
        DataSource.ProductList.Insert(x+1 ,product);         
    }
    /// <summary>
    /// function which get ID of product and delte him. if he doesnt exist we throw exepction
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete (int id)
    {
        int x = DataSource.ProductList.FindIndex(x => x?.ID == id);
        if (x == -1)
            throw new Exception("product not founed");
        DataSource.ProductList.RemoveAt(x);

    }
    /// <summary>
    /// copy the detalis of one list to another.
    /// </summary>
    /// <returns> the product list</returns>
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> ProductList1 = new List<Product?>();
        for (int i = 0; i < DataSource.ProductList.Count; i++)
        { 
           ProductList1.Add(DataSource.ProductList[i]);
        }
        return ProductList1;
    }

}