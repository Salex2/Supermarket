using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Supermarket.Views.SupermarketView;

namespace Supermarket.ViewModels
{
    //Conductor<object>
    //Screen allows for more control over opening and closing; 
    //EXP: If you want to close a form without saving you can get an Event to ask you if you are sure about exiting without saving
    public class SupermarketViewModel : Screen
    {
        

        public SupermarketViewModel()
        {

           
           Database db = new Database();
           Products = new BindingList<Product>(db.GetProducts());

        }
        
        public BindingList<Product> Products { get; set; }


        //used for the selection of items in the products ListBox
        //In the ListBox i write SelectedItem="SelectedProduct"
        //When i select a product in the ListBox it will put it in my SelectedProduct property;it does it by default
        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart); // when you select a product check if canAddToCart has been modified
            }
        }





        //THE CART ; Here I initialize the new List of added products in the Cart
        private BindingList<ProductsInCart> _cart = new BindingList<ProductsInCart>();

        public BindingList<ProductsInCart> ShoppingCart
        {
            get { return _cart; }
            set
            {
                _cart = value;

            }
        }



        public int ProductQty { get; set; }
        


        //Check if a product is selected; 
        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //make sure qty is bigger than 0;
                if (SelectedProduct != null && ProductQty > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        private int _zipcode;

        public int ZipCode
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;

                //When ZIPCODE value changes notify CanPay 
                NotifyOfPropertyChange(() => CanPay);
            }
        }
      

        public bool CanPay
        {
            get
            {

                bool output = false;

                if (ShoppingCart.Count() >= 1 && ZipCode > 180)
                {
                    output = true;
                }

                return output;
            }
            


        }
        //Takes the itesm from the products LIST and the qty; don`t take it away from the products List
        //Basically i create a new object to add in cart ; recreate the Product ListBox
        public void AddToCart()
        {
            ProductsInCart product = new ProductsInCart
            {
                Product = SelectedProduct,
                QtyInCart = ProductQty
            };
            ShoppingCart.Add(product);

            //notify on property change when we add something to cart
            NotifyOfPropertyChange(() => Total);

            //Notifiy CanPay if we added something in the cart
            NotifyOfPropertyChange(() => CanPay);

        }

        

        public string Total
        {
            get
            {
                var total = CalculateTotal();
                
                return total.ToString() + "Ron";
            }
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (var item in ShoppingCart)
            {
                total += item.Product.Price * item.QtyInCart;
            }

            return total;
        }


        private void SaveCustomerOrder()
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

        private void SaveStatistics()
        {

            using (StreamWriter file = new StreamWriter(@"d:\Supermarket\Statistics\Statistics.txt", true))
            {
                file.WriteLine("Client Zipcode: " + ZipCode);
                file.WriteLine("Total amount: " + Total);
                file.WriteLine("Date: " + DateTime.Now);
                file.WriteLine("-------------------");
                
            }
        }
       

        //pay method
        public void Pay()
        {
            SaveCustomerOrder();

            if (SaveToStatistics)
            {
                SaveStatistics();
            }
            

            ClearCart();
            
        }

        
        
        public void ClearCart()
        {
            ShoppingCart.Clear();

            //when we clear the cart ; clear the price
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanPay);
        }

        public bool SaveToStatistics { get; set; }


        //open the statistics txt from shared drive
        public void Statistics()
        {
            var fileToOpen = @"\\DESKTOP-NAGLN77\Statistics\Statistics.txt";
           
             Process.Start(fileToOpen);
        }

        //TO DO : MAKE A REGEX TO TEST THE ZIPCODE





    }
}
