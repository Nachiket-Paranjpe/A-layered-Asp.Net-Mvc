namespace CrossCuttingLayer.ModelEntities
{
    public class CustomerDTO
    {
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }        
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
       public byte[] RowVersion { get; set; }
    }
}
