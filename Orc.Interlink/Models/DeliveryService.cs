namespace Orc.Interlink
{
    public class DeliveryService
    {
        public Network network { get; set; }
        public bool isLiabilityAllowed { get; set; }
        public bool invoiceRequired { get; set; }
        public Product product { get; set; }
        public Service service { get; set; }
    }
}