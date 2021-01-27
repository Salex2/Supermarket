using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
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
        //Bindable collection- handles things like alerting us when something has changed on the list. Update on the VIEW
        public BindableCollection<Product> Products { get; set; }
      
        public SupermarketViewModel()
        {
            Database db = new Database();

            Products = new BindableCollection<Product>(db.GetProducts());


        }

    }
}
