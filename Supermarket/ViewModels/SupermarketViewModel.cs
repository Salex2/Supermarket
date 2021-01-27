using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket.ViewModels
{
    
    //Screen allows for more control over opening and closing; 
    //EXP: If you want to close a form without saving you can get an Event to ask you if you are sure about exiting without saving
    public class SupermarketViewModel : Conductor<object>
    {

       

        public SupermarketViewModel()
        {
           Database db = new Database();
           var productList = db.GetProducts();
           Products = new BindingList<Product>(productList);

        }

        private BindingList<Product> _products;

        public BindingList<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
            }
        }

        //THE CART
        private BindingList<Product> _cart;

        public BindingList<Product> ShoppingCart
        {
            get { return _cart; }
            set
            {
                _cart = value;

            }
        }


        //Quantity
        private int _productqty;

        public int Qty
        {
            get { return _productqty; }
            set
            {
                _productqty = value;
                NotifyOfPropertyChange(() => Qty);
            }

        }


        //Make sure something is selected and qty is not 0
        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                return output;
            }
        }

        public void AddToCart()
        {

        }

        //The total as string 

        public string Total
        {
            get
            {
                //TODO - Replace with calculation
                return "0Ron";
            }
        }


        //check if is something in the cart so user can pay
        public bool CanPay
        {
            get
            {
                bool output = false;
                return output;
            }
        }

        //pay method
        public void Pay()
        {

        }

        
        //empty the cart
        public void ClearCart()
        {

        }

      



    }
}
