using System;
using System.Collections.Generic;
using System.Text;

namespace RegistruCentras.Domains
{
    public class RequestForInfo
    {
        public Guid ID { get; set; }
        public Guid RequestorId { get; set; }
        public AppUser Requestor { get; set; }
        public string Question { get; set; }
        public ResponseInfo ResponseInfo { get; set; }

    }
}
