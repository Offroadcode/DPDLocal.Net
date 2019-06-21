using System.Collections.Generic;

namespace Orc.Interlink
{
    public class InsertShipmentResponse
    {
        public string shipmentId { get; set; }
        public bool consolidated { get; set; }
        public List<ConsignmentDetail> consignmentDetail { get; set; }
    }
}