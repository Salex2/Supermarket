using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Supermarket.Views.SupermarketView;

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


       

        private int _productQty;

        public int ProductQty
        {
            get { return _productQty; }
            set
            {
                _productQty = value;
            }
        }


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
            ProductsInCart product = new ProductsInCart
            {
                Product = SelectedProduct,
                QtyInCart = ProductQty
            };
            ShoppingCart.Add(product);


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
