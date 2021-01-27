using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess
{
    public class Database
    {

        private readonly List<Product> Products = new List<Product>
        {

            new Product { ProductName = "Chips", Price = 40 },
            new Product { ProductName = "Oranges", Price = 7 },
            new Product { ProductName = "Juice", Price = 10 },
            new Product { ProductName = "Meat", Price = 19 },
            new Product { ProductName = "Beer", Price = 6 },
            new Product { ProductName = "Avocado", Price = 14 },
        };
       

        
        public List<Product> GetProducts()
        {
            return Products;
        }
    }
}
