using System.Collections.Generic;
using MonoCross.Navigation;
using CustomerManagement.Shared.Model;
using System.Xml.Serialization;
using System.Net;
using System.IO;

namespace CustomerManagement.Controllers
{
    public class CustomerListController : MXController<List<Customer>>
    {
        public override string Load(Dictionary<string, string> parameters)
        {
            // populate model
            Model = GetCustomerList();

            return ViewPerspective.Default;
        }

        public static List<Customer> GetCustomerList()
        {
            List<Customer> customerList = new List<Customer>();

            // XML Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));

            // web request
            string urlCustomers = string.Format("{0}customers.xml", App.ServiceUri);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlCustomers);
            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), true))
            {
                // XML serializer
                customerList = (List<Customer>)serializer.Deserialize(reader);
            }
            return customerList;
        }
    }
}
