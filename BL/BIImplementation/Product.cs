﻿using BIApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System;

namespace BIImplementation
{
    internal class Product : BIApi.IProduct
    {
        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// get thr details of the product by his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> prodcut </returns>
        /// <exception cref="BO.BlIdDoNotExistException">if the id of the product didnt exist </exception>
        public BO.Product GetProductsById(int id)
        {
            DO.Product product = new DO.Product();

            try
            {
                product = dal!.Product.GetById(id);/// לגבי try
            }
            catch (DO.DalIdDoNotExistException ex)//doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);
            }
            return new BO.Product()///return the product and copy from do to bo 
            {
                ProductID = product.ID,
                Name = product.Name,
                InStock = product.InStock,
                Price = product.Price,
                Category = (BO.Category)product.Category,
                ImageRelativeName = @"\pics\Img" + product.ID + ".jpg"
            };
        }
        /// <summary>
        /// return IEnumerable of the prodcut for the mannager 
        /// funct which gets filter 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BO.BlNullPropertyException"></exception>
        /// <exception cref="BO.BlWrongCategoryException"></exception>
        public IEnumerable<BO.ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null)// for the mannager 
        {
            IEnumerable<BO.ProductForList?> bList = from DO.Product doProduct in dal!.Product.GetAll()
                                                    select new BO.ProductForList///copy from do to bo 
                                                    {
                                                        ID = doProduct.ID,
                                                        ProductName = doProduct.Name,
                                                        Category = (BO.Category)doProduct.Category,
                                                        Price = doProduct.Price,
                                                        ImageRelativeName = @"\pics\Img" + doProduct.ID + ".jpg"
                                                    };

            return filter is null ? bList : bList.Where(filter);//if there is filter return by filtering else return without filter(full list)
        }
        /// <summary>
        /// return producItem for the client by the id 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public BO.ProductItem RequestProductDetailsForC(int ProductId, BO.Cart cart)
        {


            DO.Product? product;

            try
            {
                product = dal!.Product.GetById(ProductId);
            }
            catch (DO.DalIdDoNotExistException ex)//product doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);
            }
            /// return the productItem 
            return new BO.ProductItem()///builiding a productItem
            {
                ID = product?.ID ?? throw new BO.BlNullPropertyException("missing id"),
                ProductItemName = product?.Name ?? throw new BO.BlNullPropertyException("missing name "),
                InStock = product?.InStock > 0 ? true : false,
                Category = (BO.Category)(product?.Category ?? throw new BO.BlWrongCategoryException("worng category")),
                Price = product?.Price ?? throw new BO.BlNullPropertyException("missing price"),
                AmountInCart = cart.OrderItems is null ? 0 : cart.OrderItems.Where(x => x?.ProductID == ProductId).Sum(x => x!.AmountOfItem),
                ImageRelativeName = @"\pics\Img" + product.Value.ID + ".jpg"


            };
        }

        /// <summary>
        /// return the catalog of the product for the client 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<BO.ProductItem?> GetListedProductsForC()//catalog of products for customer 
        {

            return from DO.Product? doProduct in dal!.Product.GetAll()
                   select new BO.ProductItem
                   {
                       ID = doProduct?.ID ?? throw new NullReferenceException("MIssing ID"),
                       ProductItemName = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                       Category = (BO.Category)(doProduct?.Category ?? throw new NullReferenceException("missing category")),
                       Price = doProduct?.Price ?? 0,
                       InStock = doProduct?.InStock > 0 ? true : false,
                       AmountInCart = 0,
                       ImageRelativeName = @"\pics\Img" + doProduct.Value.ID + ".jpg"


                   };
        }

        /// <summary>
        /// add product to the list 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlIdAlreadyExistException"></exception>
        public void AddProduct(BO.Product product)
        {
            if (product.Name!.Length == 0)
                throw new BO.BlInvalidInputException("product name");
            if (product.Price < 0)
                throw new BO.BlInvalidInputException("product price");
            if (product.InStock < 0)
                throw new BO.BlInvalidInputException("product Stock");
            try
            {
                DO.Product pro = new DO.Product
                {
                    ID = product.ProductID,
                    Name = product.Name,
                    InStock = product.InStock,
                    Category = (DO.Category)product.Category,
                    Price = product.Price
                };
                dal?.Product.Add(pro);
            }
            catch (DO.DalIdAlreadyExistException ex)
            {
                throw new BO.BlIdAlreadyExistException("couldnt add product", ex);
            }

        }
        /// <summary>
        /// delete prodcut from the list of the product לעבור על זה לראות אם החריגות טובות
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="BO.BlNullPropertyException"the valure is not match></exception>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public void DeleteProduct(int ID)
        {
            DO.OrderItem? orderItems = dal!.OrderItem.GetAll().FirstOrDefault(item => item?.ID == ID);
            if (orderItems != null)
                throw new BO.BlNullPropertyException("cant delete, exist in orders");//לשאול מה החריגה שצריכה להיות 
            try
            {
                dal.Product.Delete(ID);
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product", ex);

            }
            //IEnumerable<DO.Order?> orders = dal.Order.GetAll();
            //var order = from item in orders
            //            where dal.OrderItem.GetById(item.Value.ID).ProductId == ID
            //            select item;
            //if(orders.Any())
            //{
            //    dal.Product.Delete(ID);
            //}
            //try
            //{
            //    if (((List<DO.OrderItem>)dal.OrderItem.GetAll()).Exists(x => x.ProductId == ID))
            //        throw new Exception();//חריגה שאומרת שאנחנו לא יכולים למחוק את המוצר כי הוא קיים בהזמנות
            //    dal.Product.Delete(ID);
            //}
            //catch
            //{
            //    throw new Exception();// זה אומר שאן לנו בכלל רשימה 
            //}            
        }
        /// <summary>
        /// update the prodcut 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.BlInvalidInputException">invalied input </exception>
        /// <exception cref="BO.BlIdDoNotExistException">the producrt not exist </exception>
        public void UpdateProductData(BO.Product product)
        {
            if (product.Name!.Length == 0)
                throw new BO.BlInvalidInputException("product name");
            if (product.Price < 0)
                throw new BO.BlInvalidInputException("product price ");
            if (product.InStock < 1)
                throw new BO.BlInvalidInputException("product Stock ");
            try
            {
                dal?.Product.Update(new DO.Product()
                {
                    ID = product.ProductID,
                    Name = product.Name,
                    InStock = product.InStock,
                    Category = (DO.Category)product.Category,
                    Price = product.Price
                });
            }
            catch (DO.DalIdDoNotExistException ex)//prduct doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);

            }
            return;
        }
        public IEnumerable<ProductItem?> MostPopular(BO.Cart cart)
        {
            //Grouping all ordered products by product ID
            var productList = from item in dal.OrderItem.GetAll()
                              group item by item?.ID into groupPopular
                              select new { id = groupPopular.Key, Items = groupPopular };

            //Sort the products in descending order according to the quantity ordered
            //Take the first 10
            productList = productList.OrderByDescending(x => x.Items.Count()).Take(10);

            return from item in productList
                   let p = dal.Product.GetById(item?.id ?? throw new BlIdDoNotExistException("product doe not exist"))
                   select new BO.ProductItem
                   {
                       ID = p.ID,
                       ProductItemName = p.Name,
                       Price = p.Price,
                       Category = (BO.Category)p.Category!,
                       ImageRelativeName = @"\pics\Img" + item.id + ".jpg",
                       //AmountInCart = AmountInCart(cart, p.ID),
                       //InStock = checkIfstock(p)
                   };
        }
        private bool checkIfstock(int inStock)
        {
            if (inStock <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// the func return the top 10 expensive products
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BlDoesNotExistException"></exception>
        public IEnumerable<ProductItem?> MostExpensive(BO.Cart cart)
        {
            //Sort the products in descending order by price
            //Take the first 10
            var productList = dal.Product.GetAll().OrderByDescending(x => x?.Price).Take(10);

            return from DO.Product item in productList
                   select new BO.ProductItem
                   {
                       ID = item.ID,
                       ProductItemName = item.Name,
                       Price = item.Price,
                       Category = (BO.Category)item.Category!,
                       ImageRelativeName =  @"\pics\Img" + item.ID + ".jpg",
                       
                   };
        }

        //public IEnumerable<ProductItem?> MostPopulars(BO.Cart cart)
        //{
        //    //Grouping all ordered products by product ID
        //    var productList = from item in dal.OrderItem.GetAll()
        //                      group item by item?.ID into groupPopular
        //                      select new { id = groupPopular.Key, Items = groupPopular };

        //    //Sort the products in descending order according to the quantity ordered
        //    //Take the first 10
        //    productList = productList.OrderByDescending(x => x.Items.Count()).Take(3);

        //    return from item in productList
        //           let p = dal.Product.GetById(item?.id ?? throw new BlIdDoNotExistException("product doe not exist"))
        //           select new BO.ProductItem
        //           {
        //               ID = p.ID,
        //               ProductItemName = p.Name,
        //               Price = p.Price,
        //               Category = (BO.Category)p.Category!,
        //               ImageRelativeName = @"\pics\Img" + p.ID + ".jpg"
        //           };
        //}





    }
}
