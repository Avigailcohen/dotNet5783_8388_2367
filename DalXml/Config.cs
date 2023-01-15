using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
     static internal class Config
    {
        static string s_config = "configuration";
        internal static int GetNextOrderNumber()
        {
            return (int)XmlTools.LoadListFromXMLElement(s_config).Element("nextOrderNumber");
        }
        internal static void SaveNextOrderNumber(int orderNumber)
        {
            XElement root=XmlTools.LoadListFromXMLElement(s_config);
            root.Element("nextOrderNumber").SetValue(orderNumber.ToString());
            XmlTools.SaveListToXMLElement(root, s_config); 
        }
        internal static int GetOrderItemNumber()
        {
            return (int)XmlTools.LoadListFromXMLElement(s_config).Element("nextOrderItemNumber");
        }
        internal static void SaveNextOrderItemNumber(int orderItemNumber)
        {
            XElement root=XmlTools.LoadListFromXMLElement (s_config);
            root.Element("nextOrderItemNumber").SetValue(orderItemNumber.ToString());
            XmlTools.SaveListToXMLElement(root,s_config);
        }
    }

}
