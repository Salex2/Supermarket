using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Services
{
    public class SupermarketServices 
    {

        public static decimal CalculateTotal( BindingList<ProductsInCart> ShoppingCart)
        {
            decimal total = 0;

            foreach (var item in ShoppingCart)
            {
                total += item.Product.Price * item.QtyInCart;
            }

            return total;
        }

        public static void SaveCustomerOrder(string ZipCode,string Total)
        {
            string name = ZipCode.ToString();
            string path = System.IO.Path.Combine(@"d:\Supermarket\Orders", name + ".txt");
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Client Zipcode: " + ZipCode);
                writer.WriteLine("Total amount: " + Total);
                writer.WriteLine("Date: " + DateTime.Now);
            }
        }

        public static void SaveStatistics(string ZipCode, string Total)
        {
            using (StreamWriter file = new StreamWriter(@"d:\Supermarket\Statistics\Statistics.txt", true))
            {
                file.WriteLine("Client Zipcode: " + ZipCode);
                file.WriteLine("Total amount: " + Total);
                file.WriteLine("Date: " + DateTime.Now);
                file.WriteLine("-------------------");

            }
        }

        public static void Statistics()
        {
            var fileToOpen = @"\\DESKTOP-NAGLN77\Statistics\Statistics.txt";

            Process.Start(fileToOpen);
        }
    }
}
