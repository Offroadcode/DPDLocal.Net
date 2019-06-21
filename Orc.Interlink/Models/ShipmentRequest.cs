using System;
using System.Collections.Generic;

namespace Orc.Interlink
{
    public class ShipmentRequest
    {
        public object job_id { get; set; }
        public bool collectionOnDelivery { get; set; }
        public object invoice { get; set; }
        public DateTime collectionDate { get; set; }
        public bool consolidate { get; set; }
        public List<Consignment> consignment { get; set; }
    }
}