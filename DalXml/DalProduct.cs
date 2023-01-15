using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class DalProduct : IProduct
    {
        readonly string s_product = "Products";
        static DO.Product? createProductfromXElement(XElement s)
        {
            return new DO.Product()
            {
                ID = s.ToIntNullable("ID") ?? throw new FormatException("id"),
                Name = (string?)s.Element("Name"),
                Category = s.ToEnumNullable<DO.Category>("Category") ?? 0,
                InStock = s.ToIntNullable("InStock") ?? 0,
                Price = s.ToDoubleNullable("Price") ?? 0

            };
        }
       
        public int Add(Product product)
        {
            XElement? products = XmlTools.LoadListFromXMLElement(s_product);
            XElement? pro=(from pr in products.Elements()
                                where pr.ToIntNullable("ID")== product.ID
                                select pr).FirstOrDefault();
            if(pro!=null)
                throw new DalIdAlreadyExistException(product.ID, "Products");
            XElement proElemnet = new XElement("Products",
                new XElement("ID", product.ID),
                new XElement("Name", product.Name),
                new XElement("Category", product.Category),
                new XElement("InStock", product.InStock),
                new XElement("Price", product.Price));
            products?.Add(proElemnet);
            XmlTools.SaveListToXMLElement(products, s_product);
            return product.ID;






            //List<DO.Product?> listProds = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            //if (listProds.Exists(x => x?.ID == product.ID))
            //    throw new DalIdAlreadyExistException(product.ID, "Product");
            //listProds.Add(product);
            //XmlTools.SaveListToXMLSerializer(listProds, s_product);
            //return product.ID;
        }

        public void Delete(int id)
        {
            XElement? products = XmlTools.LoadListFromXMLElement(s_product);
            XElement? prod = (from pr in products.Elements()
                              where (int?)pr.Element("ID") == id
                              select pr).FirstOrDefault() ?? throw new DalIdDoNotExistException(id, "product not founed");
            prod.Remove();
            XmlTools.SaveListToXMLElement(products, s_product);


            //List<DO.Product?> listProds = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            //int x = listProds.FindIndex(x => x?.ID == id);
            //if (x == -1)
            //    throw new DalIdDoNotExistException(id, "product not founed");
            //listProds.RemoveAt(x);
            //XmlTools.SaveListToXMLSerializer(listProds, s_product);



        }

        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            XElement? products = XmlTools.LoadListFromXMLElement(s_product);
            if (filter!=null)
            {
                return from s in products.Elements()
                       let doprod = createProductfromXElement(s)
                       where filter(doprod)
                       select (DO.Product?)doprod;
            }
            else
            {
                return from s in products.Elements()
                       select createProductfromXElement(s);
            }

            //List<DO.Product?> listProds = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            //if (filter != null)
            //    return listProds.Where(item => filter(item));
            //return listProds.Select(item => item);

        }

        public Product GetById(int id)
        {
            XElement? products = XmlTools.LoadListFromXMLElement(s_product);
            return (from s in products?.Elements()
                    where s.ToIntNullable("ID") == id
                    select (DO.Product?)createProductfromXElement(s)).FirstOrDefault()
                    ?? throw new DalIdDoNotExistException(id, "product not founed");
            //List<DO.Product?> listProds = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            //Product product = new Product();
            //product = listProds.Find(x => x?.ID == id) ?? throw new DalIdDoNotExistException(product.ID, "id");
            //return product;

        }

        public void Update(Product product)
        {
            List<DO.Product?> listProds = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            int index = listProds.FindIndex(x=> x?.ID == product.ID);
            listProds[index] = product;
            XmlTools.SaveListToXMLSerializer(listProds, s_product);
            //List<DO.Product?> listProds = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            //int x = listProds.FindIndex(x => x?.ID == product.ID);
            //if (x == -1)
            //    throw new DalIdDoNotExistException(product.ID, "not exist");
            //listProds[x] = product;
            //XmlTools.SaveListToXMLSerializer(listProds, s_product);
        }
    }
}
