using System.Collections.Generic;

namespace Orc.Interlink
{
    public class Consignment
    {
        public object consignmentNumber { get; set; }
        public object consignmentRef { get; set; }
        public List<object> parcels { get; set; }
        public CollectionDetails collectionDetails { get; set; }
        public DeliveryDetails deliveryDetails { get; set; }
        public string networkCode { get; set; }
        public int numberOfParcels { get; set; }
        public double totalWeight { get; set; }
        public string shippingRef1 { get; set; }
        public string shippingRef2 { get; set; }
        public string shippingRef3 { get; set; }
        public object customsValue { get; set; }
        public string deliveryInstructions { get; set; }
        public string parcelDescription { get; set; }
        public object liabilityValue { get; set; }
        public bool liability { get; set; }
    }
}