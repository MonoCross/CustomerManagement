﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoCross.Navigation;

using CustomerManagement.Controllers;

namespace CustomerManagement
{
    public class App : MXApplication
    {
#if LOCAL_SERVICE
        public static readonly string ServiceUri = "http://localhost/MXDemo/";
#else
        public static readonly string ServiceUri = "http://ifactrcustomers.azurewebsites.net/";
#endif
        public override void OnAppLoad()
        {
			// Set the application title
            Title = "Customer Management";

            // Add navigation mappings
            NavigationMap.Add("Customers", new CustomerListController());

			CustomerController customerController = new CustomerController();
            NavigationMap.Add("Customers/{CustomerId}", customerController);
            NavigationMap.Add("Customers/{CustomerId}/{Action}", customerController);

            // Set default navigation URI
            NavigateOnLoad = "Customers";
        }
    }
}