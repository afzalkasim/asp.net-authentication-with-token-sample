using System;
using System.Collections.Generic;
using System.Text;

namespace Glamz.Business.Model
{
    public class BusinessDto
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string WebPage { get; set; }
        public string FacebookLink { get; set; }
        public string MobileNumber { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string LandlineNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
