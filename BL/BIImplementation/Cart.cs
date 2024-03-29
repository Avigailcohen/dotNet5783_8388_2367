﻿
using BIApi;
using System.ComponentModel.DataAnnotations;

namespace BIImplementation
{
    internal class Cart : ICart
    {


        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// add product to the cart by id and throw exeption if the id dosent founded
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="ProductID"></param>
        /// <returns>updated cart </returns>
        /// <exception cref="BO.BlNullPropertyException"></exception>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public BO.Cart AddProduct(BO.Cart cart, int ProductID)
        {

            //try to create an new product from dal stage
            DO.Product product = new DO.Product();
            try
            {
                //check if the product exist 
                product = dal!.Product.GetById(ProductID);


                //check if the prodcut exist int the cart-and update the amount of the order item in the cart and the price
                BO.OrderItem? orderItem = cart.OrderItems?.FirstOrDefault(item => item?.ID == ProductID);
                if (orderItem != null)
                {
                    if (product.InStock > 0)
                    {
                        orderItem.AmountOfItem += 1;
                        orderItem.TotalPrice = product.Price;
                    }
                }
                else///if the item is not exist in the cart
                {
                    ///crete a new order item in the cart

                    if (product.InStock > 0)
                    {
                        BO.OrderItem? newOrderItem = new BO.OrderItem()
                        {
                            ID = product.ID,
                            Name = product.Name,
                            Price = product.Price,
                            AmountOfItem = 1,
                            TotalPrice = product.Price,
                            ProductID = product.ID,
                            ImageRelativeName = @"\pics\Img" + product.ID + ".jpg"
                        };
                        ///add 
                        cart.OrderItems = cart.OrderItems!.Append(newOrderItem);
                    }

                    else
                    {
                        throw new BO.BlNullPropertyException(" NOT IN STOCK");//the product not in stock חריגה שהמוצר לא במלאי

                    }
                }
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product in cart", ex);
            }
            //update the price
            cart.Price += product.Price;
            return cart;
        }
        /// <summary>
        /// update the amount od the product in the cart 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="ProductID"></param>
        /// <param name="NewAmount"></param>
        /// <returns> updated cart </returns>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        /// <exception cref="BO.BlNullPropertyException"></exception>

        public BO.Cart UpdateAmount(BO.Cart cart, int ProductID, int NewAmount)
        {
            
            ///cheking by the do if the id exist else throw execption 
            DO.Product product = new DO.Product();
            
            try
            {
                product = dal!.Product.GetById(ProductID);
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product in cart", ex);
            }
            ///if the product in stock

            if (product.InStock < NewAmount)
                throw new BO.BlNullPropertyException(" not in stock");
            ///find the id which answeres the critiorion 

            BO.OrderItem? orderItem = cart.OrderItems.FirstOrDefault(x => x?.ID == ProductID);
            


            int x = cart.OrderItems.ToList().FindIndex(x => x?.ID == ProductID);
            if (orderItem == null)
                throw new BO.BlNullPropertyException("object doenst found");//the object is not exist

            /// if the amount is 0 delete the product 
            if (NewAmount == 0)//the given new amount
            {

                cart.OrderItems = cart.OrderItems.Where(item => item?.ProductID != ProductID);//deletes this item from cart
                cart.Price += product.Price * (NewAmount - orderItem.AmountOfItem);
                //update proce(the amount differnce will be a negative number-will reduce the price)
                cart.Price -= orderItem.TotalPrice;
               // ((List<BO.OrderItem?>)cart.OrderItems).Remove(orderItem);
               
                



            }
            
            else
            {
                double oldTotalPrice = orderItem.TotalPrice;
                orderItem.AmountOfItem = NewAmount;
                orderItem.TotalPrice = NewAmount * orderItem.Price;
                cart.Price += orderItem.TotalPrice - oldTotalPrice;
            }
          ///return the update cart 
            return cart;

        }
        /// <summary>
        /// create a cart to buy.
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="BO.BlInvalidInputException">if the input is invaild </exception>
        /// <exception cref="BO.BlIdDoNotExistException">if the order is exist </exception>
        public void OrderConfirmation(BO.Cart cart)
        {
            ///check the details 
            if (cart.CustomerName =="")
                throw new BO.BlIncorrectDateException("customer name");
            if(!new EmailAddressAttribute().IsValid(cart.CustomerEmail)) 
                throw new BO.BlIncorrectDateException("customer Email");
            if (cart.CustomerAddress ==null )
                throw new BO.BlIncorrectDateException("customer Address");
            if (cart.OrderItems.All(items=>Check(items.ProductID,items.AmountOfItem) == true)==false)//no items in cart
                throw new BO.BlNullPropertyException("no items in cart");
            if (cart.OrderItems.Count() == 0)
                throw new BO.BlNullPropertyException("no items in cart");
            ////all details are correct
            BO.Order order = new BO.Order()
            {
                CustomerName = cart.CustomerName,
                CustomerAddress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                Status = BO.OrderStatus.Ordered,
                ShipDate = null,
                DeliveryDate = null,
                TotalPrice = cart.Price,

            };
            //cart.CustomerAddress = null;
            //cart.CustomerEmail = null;
            //cart.CustomerName = null;
            order.OrderItems = cart.OrderItems.Select(item => item);
            DO.Order order1 = new DO.Order()
            {
                CustomerName = order.CustomerName,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                DeliveryDate = order.DeliveryDate,
                ShipDate = order.ShipDate,
                OrderDate = order.OrderDate,
            };
            order.ID=dal.Order.Add(order1);
            IEnumerable<DO.OrderItem> orderItems;
            orderItems = from BO.OrderItem items in order.OrderItems
                         select new DO.OrderItem()
                         {
                             OrderId = order.ID,
                             ProductId = items.ProductID,
                             Price = items.Price,
                             Amount = items.AmountOfItem
                         };
            orderItems.Select(x => dal.OrderItem.Add(x)).ToList();
            order.OrderItems.ToList().ForEach(item => UpdateAmount(item.ID, item.AmountOfItem));
           
            
            

            //DO.Order order = new DO.Order()
            //{
            //    CustomerName = cart.CustomerName,
            //    CustomerEmail = cart.CustomerEmail,
            //    CustomerAddress = cart.CustomerAddress,
            //    OrderDate = DateTime.Now,
            //    DeliveryDate = null,
            //    ShipDate = null,
            //};

            //try
            //{
            //    int orderID = dal!.Order.Add(order);

            //    IEnumerable<int> orderItemsID = from item in cart.OrderItems
            //                                    select dal.OrderItem.Add(
            //                                        new DO.OrderItem
            //                                        {
            //                                            OrderId = orderID,
            //                                            Price = item.Price,
            //                                            ProductId = item.ProductID,
            //                                            Amount = item.AmountOfItem,
            //                                            ID = item.ID
            //                                        });

            //    IEnumerable<DO.Product> productUpdate = from item in cart.OrderItems
            //                                            select new DO.Product
            //                                            {
            //                                                ID = item.ProductID,
            //                                                Name = item.Name,
            //                                                Price = item.Price,
            //                                                Category = dal.Product.GetById(item.ProductID).Category,
            //                                                InStock = dal.Product.GetById(item.ProductID).InStock - item.AmountOfItem
            //                                            };


            //    productUpdate.ToList().ForEach(x => dal.Product.Update(x));
            //}
            //catch(DO.DalIdDoNotExistException ex)
            //{
            //   throw  new BO.BlIdDoNotExistException("dont exist",ex) ;
            //}
        }
        bool Check(int productID, int Amount)
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetById(productID);
            }
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product in cart",ex);
            }
            if (product.InStock >= Amount)
                return true;
            return false;
        }
        void UpdateAmount(int productId, int amount)
        {
            DO.Product prod;
            try
            {
                prod = dal.Product.GetById(productId);//return product by id
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product in cart", ex);

            }
            prod.InStock -= amount;
            dal.Product.Update(prod);
        }
    }

}


