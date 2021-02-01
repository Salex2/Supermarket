using Caliburn.Micro;
using DataAccess;
using Supermarket.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                NotifyOfPropertyChange(() => CanAddToCart); // when you select a product check if canAddToCart has been modified
            }
        }


        private string _zipcode="";

        public string ZipCode
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;

                //When ZIPCODE value changes notify CanPay 
                NotifyOfPropertyChange(() => CanPay);
            }
        }

        
        Regex validZipCode = new Regex("^[0-9]{5}$");
        public bool CanPay
        {
            get
            {

                bool output = false;

                if (ShoppingCart.Count() >= 1 && validZipCode.IsMatch(ZipCode))
                {
                    output = true;
                }

                return output;
            }   
        }

        //THE CART ; Here I initialize the new List of added products in the Cart
        public BindingList<ProductsInCart> ShoppingCart { get; set; } = new BindingList<ProductsInCart>();

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

        //Takes the itesm from the products LIST and the qty; don`t take it away from the products List
        //Basically i create a new object to add in cart ; recreate the Product ListBox
        public void AddToCart()
        {
            var product = new ProductsInCart
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
            return SupermarketServices.CalculateTotal(ShoppingCart);
         
        }


        private void SaveCustomerOrder()
        {
            SupermarketServices.SaveCustomerOrder(ZipCode, Total);
        }

        private void SaveStatistics()
        {

            SupermarketServices.SaveStatistics(ZipCode, Total);
           
        }
       

        //pay method
        public void Pay()
        {
            SaveCustomerOrder();

            //if is checked save also in statistcs file
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


        private bool _output = true;
        public bool CanStatistics
        {
            get
            {
                return _output;
            }
        }

        
        //open the statistics txt from shared drive
        public  void Statistics()
        {
            _output = false;

            SupermarketServices.Statistics();
            
            NotifyOfPropertyChange(() => CanStatistics);
        }



    }
}
