using System.Collections.Generic;
using MonoCross.Navigation;
using CustomerManagement.Shared.Model;

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
#if LOCAL_DATA
            customerList = CustomerManagement.Data.XmlDataStore.GetCustomers();
#else
            // XML Serializer
            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));

            // web request
            string urlCustomers = "http://localhost/MXDemo/customers.xml";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlCustomers);
            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), true))
            {
                // XML serializer
                customerList = (List<Customer>)serializer.Deserialize(reader);
            }
#endif
            return customerList;
        }
    }
}
