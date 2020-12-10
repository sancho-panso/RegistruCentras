using System;

namespace RegistruCentras.Domains
{
    public class ResponseInfo
    {
        public Guid ID { get; set; }
        public Guid ResponserId { get; set; }
        public AppUser Responder { get; set; }
        public string Answer { get; set; }
        public Guid RequestForInfoID { get; set; }
        public RequestForInfo Request { get; set; }
    }
}