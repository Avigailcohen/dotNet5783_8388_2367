using BIApi;



namespace BIImplementation
{
    internal class Product : IProduct
    {
        DalApi.IDal dal = new Dal.DalList();
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
                product = dal.Product.GetById(id);/// לגבי try
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
                Category = (BO.Category)product.Category
            };
        }
        /// <summary>
        /// return IEnumerable of the prodcut for the mannager 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BO.BlNullPropertyException"></exception>
        /// <exception cref="BO.BlWrongCategoryException"></exception>
        public IEnumerable<BO.ProductForList?> GetListedProducts()// for the mannager 
        {
            return from DO.Product? doProduct in dal.Product.GetAll()
                   select new BO.ProductForList///copy from do to bo 
                   {
                       ID = doProduct?.ID ?? throw new BO.BlNullPropertyException("missing id"),
                       ProductName = doProduct?.Name ?? throw new BO.BlNullPropertyException("missing name"),
                       Category = (BO.Category)(doProduct?.Category ?? throw new BO.BlWrongCategoryException("worng category")),
                       Price = doProduct?.Price ?? 0
                   };

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
            if (ProductId < 100000 || ProductId > 999999)
                throw new BO.BlInvalidInputException("product ID");

            DO.Product product;

            try
            {
                product = dal.Product.GetById(ProductId);
            }
            catch (DO.DalIdDoNotExistException ex)//product doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);
            }
            /// return the productItem 
            return new BO.ProductItem()///builiding a productItem
            {
                ID = product.ID,
                ProductItemName = product.Name,
                InStock = product.InStock > 0 ? true : false,
                Category = (BO.Category)product.Category,
                Price = product.Price,
                AmountInCart = cart.OrderItems is null ? 0 : cart.OrderItems.Where(x => x.ProductID == ProductId).Sum(x => x.AmountOfItem)

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
            return from DO.Product? doProduct in dal.Product.GetAll()
                   select new BO.ProductItem
                   {
                       ID = doProduct?.ID ?? throw new NullReferenceException("MIssing ID"),
                       ProductItemName = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                       Category = (BO.Category)(doProduct?.Category ?? throw new NullReferenceException("missing category")),
                       Price = doProduct?.Price ?? 0,
                       InStock = doProduct?.InStock > 0 ? true : false,
                       AmountInCart =0/////////////////////////////////////////////////////check it!!!!


                   };

            throw new NotImplementedException();
        }
        /// <summary>
        /// add product to the list 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlIdAlreadyExistException"></exception>
        public void AddProduct(BO.Product product)
        {
            
            
                if (product.ProductID < 100000 || product.ProductID > 999999)
                    throw new BO.BlInvalidInputException("product ID");
                if(product.Name.Length == 0)
                    throw new BO.BlInvalidInputException("product name");
                if (product.Price < 0)
                    throw new BO.BlInvalidInputException("product price");
                if(product.InStock < 0)
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
                dal.Product.Add(pro);
            }
            catch(DO.DalIdAlreadyExistException ex)
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
            DO.OrderItem? orderItems = dal.OrderItem.GetAll().FirstOrDefault(item => item.Value.ID == ID);
            if (orderItems != null)
                throw new BO.BlNullPropertyException("cant delete, exist in orders");//לשאול מה החריגה שצריכה להיות 
            try
            {
                dal.Product.Delete(ID);
            }
            catch(DO.DalIdDoNotExistException ex)
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
            if (product.ProductID < 100000 || product.ProductID > 999999)
                throw new BO.BlInvalidInputException("product ID");
            if(product.Name.Length == 0)
                throw new BO.BlInvalidInputException("product name");
            if(product.Price < 0)
                throw new BO.BlInvalidInputException("product price ");
            if(product.InStock < 1)
                throw new BO.BlInvalidInputException("product Stock ");
            try
            {
                dal.Product.Update(new DO.Product()
                {
                    ID = product.ProductID,
                    Name = product.Name,
                    InStock = product.InStock,
                    Category = (DO.Category)product.Category,
                    Price = product.Price
                });
            }
            catch(DO.DalIdDoNotExistException ex)//prduct doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);

            }  
            return;
        }
    }

}
