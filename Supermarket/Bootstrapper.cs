using Caliburn.Micro;
using Supermarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Supermarket
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        //instead of MainWindow.XAML our launch point is SuperMarketViewModel

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<SupermarketViewModel>();
        }
    }
}
