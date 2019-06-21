using System.Collections.Generic;

namespace Orc.Interlink
{
    public class ConsignmentDetail
    {
        public string consignmentNumber { get; set; }
        public List<string> parcelNumbers { get; set; }
    }
}