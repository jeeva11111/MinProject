using System.ComponentModel.DataAnnotations;

namespace MinProject.Models.Customer
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string? ContactName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    public class CustomerModel
    {

        public List<Customer>? Customers { get; set; }


        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}
