using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class Database
    {

        private readonly List<Product> Products = new List<Product>
        {

            new Product { ProductName = "Detergent", Price = 20 },
            new Product { ProductName = "Oranges", Price = 42 },
            new Product { ProductName = "Juice", Price = 10 },
            new Product { ProductName = "Meat", Price = 19 },
            new Product { ProductName = "Beer", Price = 25 },
        };
       

        
        public List<Product> GetProducts()
        {
            return Products;
        }
    }
}
