using System.Linq;
using System.Reflection;

namespace BO;
public static class Tools
{
    public static string ToStringProperty<T>(this T t, string str = "")
    {
        foreach(PropertyInfo item in t.GetType().GetProperties())
        {
            if (item.GetValue(t, null) is IEnumerable<object>)
            {
                IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj: t, null);
                string s = String.Join("  ", list);
                str += "\n" + item.Name + ": " + s;

            }
            else
                str += "\n" + item.Name
                    + ": " + item.GetValue(t, null);
               

                
           
        }
        //string str = "";
        //foreach (PropertyInfo item in t.GetType().GetProperties())
        //{
        //    if (item.GetValue(t, null) is IEnumerable<object>)
        //    {
        //        IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj: t, null);
        //        string s = String.Join("  ", list);
        //        str += " " + item.Name + ": " + s;

        //    }
        //    else
        //        // str += " " + item.Name
        //        //+ ": " + item.GetValue(t, null);
        //       str = "";
        //    foreach (PropertyInfo item in typeof(T).GetProperties())
        //    {
        //        str += "\n" + item.Name + ": " + item.GetValue(t, null);
        //    }
        return str;





    }
    //return str;
    //string str = "";
    //foreach (PropertyInfo item in typeof(T).GetProperties())
    //{
    //    str += "\n" + item.Name + ": " + item.GetValue(t, null);
    //}
    //return str;

}



